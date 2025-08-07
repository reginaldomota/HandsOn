using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.CrossCutting.Resources;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using ChartOfAccounts.Infrastructure.Extensions;
using ChartOfAccounts.Infrastructure.Factories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ChartOfAccounts.Infrastructure.Repositories;

public class ChartOfAccountsRepository : IChartOfAccountsRepository
{
    private readonly AppDbContext _context;

    public ChartOfAccountsRepository(ITenantDbContextFactory contextFactory)
    {
        _context = contextFactory.CreateDbContext();
    }

    public async Task<(List<ChartOfAccount> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? find = null, bool? isPostable = null)
    {
        try
        {
            IQueryable<ChartOfAccount> query = _context.ChartOfAccounts;

            if (!string.IsNullOrWhiteSpace(find))
            {
                string findLower = find.ToLowerInvariant();
                query = query.Where(a =>
                    a.Code.ToLower().Contains(findLower) ||
                    a.Name.ToLower().Contains(findLower));
            }

            if (isPostable.HasValue)
            {
                query = query.Where(a => a.IsPostable == isPostable.Value);
            }

            IQueryable<ChartOfAccount> tenantQuery = query.ForCurrentTenant();

            int totalCount = await tenantQuery.CountAsync();

            List<ChartOfAccount> items = await tenantQuery
                .OrderBy(a => a.Code)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

    public async Task<ChartOfAccount?> GetByCodeAsync(string code)
    {
        try
        {
            return await _context.Set<ChartOfAccount>()
                .ForCurrentTenant()
                .FirstOrDefaultAsync(x => x.Code == code);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

    public async Task<ChartOfAccount?> GetByIdempotencyKeyAsync(Guid idempotencyKey)
    {
        try
        {
            return await _context.Set<ChartOfAccount>()
                .ForCurrentTenant()
                .FirstOrDefaultAsync(x => x.IdempotencyKey == idempotencyKey);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

    public async Task<bool?> IsPostableAsync(string code)
    {
        try
        {
            bool? isPostable = await _context.ChartOfAccounts
                .ForCurrentTenant()
                .Where(c => c.Code == code)
                .Select(c => (bool?)c.IsPostable)
                .FirstOrDefaultAsync();

            return isPostable;
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

    public async Task CreateAsync(ChartOfAccount account)
    {
        Validator.ValidateObject(account, new ValidationContext(account), validateAllProperties: true);

        try
        {
            _context.ChartOfAccounts.Add(account);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            throw new DataIntegrityViolationException(ErrorMessages.Error_DataIntegrityViolation, ex);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

    public async Task DeleteAsync(string code)
    {
        try
        {
            ChartOfAccount? entity = await GetByCodeAsync(code);

            if (entity == null)
                throw new ErrorHttpRequestException(
                    string.Format(ValidationMessages.Validation_ChartOfAccounts_CodeNotFound, code),
                    (int)HttpStatusCode.NotFound,
                    Domain.Enums.ErrorCode.NotFound);

            _context.ChartOfAccounts.Remove(entity);
            await _context.SaveChangesAsync();
            
        }
        catch (ErrorHttpRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

    public async Task<List<string?>> GetChildrenCodesAsync(string parentCode)
    {
        try
        {
            return await _context
                .Set<ChartOfAccount>()
                .ForCurrentTenant()
                .Where(c => c.Code.StartsWith(parentCode + "."))
                .Select(c => c.Code)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

    public async Task<bool> HasChildrenAsync(string code)
    {
        try
        {
            return await _context.Set<ChartOfAccount>()
                .ForCurrentTenant()
                .AnyAsync(c => c.Code.StartsWith(code + "."));
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException(ErrorMessages.Error_ServiceUnavailable, ex);
        }
    }

}
