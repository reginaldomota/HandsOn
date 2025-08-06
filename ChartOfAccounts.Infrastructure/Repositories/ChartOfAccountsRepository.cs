using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Exceptions;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ChartOfAccounts.Infrastructure.Repositories;

public class ChartOfAccountsRepository : IChartOfAccountsRepository
{
    private readonly AppDbContext _context;
    private readonly IRequestContext _requestContext;

    public ChartOfAccountsRepository(AppDbContext context, IRequestContext requestContext)
    {
        _context = context;
        _requestContext = requestContext;
    }

    public async Task<(List<ChartOfAccount> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        try
        {
            IQueryable<ChartOfAccount> query = _context.ChartOfAccounts.OrderBy(x => x.CodeNormalized);

            int totalCount = await query.CountAsync();

            List<ChartOfAccount> items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Serviço temporariamente indisponível. Tente novamente mais tarde.", ex);
        }
    }

    public async Task<ChartOfAccount?> GetByCodeAsync(string code)
    {
        try
        {
            return await _context.Set<ChartOfAccount>()
                .FirstOrDefaultAsync(x => x.Code == code);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Serviço temporariamente indisponível. Tente novamente mais tarde.", ex);
        }
    }

    public async Task<ChartOfAccount?> GetByIdempotencyKeyAsync(Guid idempotencyKey)
    {
        try
        {
            return await _context.Set<ChartOfAccount>()
                .FirstOrDefaultAsync(x => x.IdempotencyKey == idempotencyKey);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Serviço temporariamente indisponível. Tente novamente mais tarde.", ex);
        }
    }

    public async Task<bool?> IsPostableAsync(string code)
    {
        try
        {
            bool? isPostable = await _context.ChartOfAccounts
                .Where(c => c.Code == code)
                .Select(c => (bool?)c.IsPostable)
                .FirstOrDefaultAsync();

            return isPostable;
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Serviço temporariamente indisponível. Tente novamente mais tarde.", ex);
        }
    }

    public async Task CreateAsync(ChartOfAccount account)
    {
        try
        {
            _context.ChartOfAccounts.Add(account);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            throw new DataIntegrityViolationException("Violação de Integridade de dados", ex);
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Serviço temporariamente indisponível. Tente novamente mais tarde.", ex);
        }
    }

    public async Task DeleteAsync(string code)
    {
        try
        {
            ChartOfAccount? entity = await GetByCodeAsync(code);
            if (entity != null)
            {
                _context.ChartOfAccounts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Serviço temporariamente indisponível. Tente novamente mais tarde.", ex);
        }
    }

    public async Task<List<string>> GetChildrenCodesAsync(string parentCode)
    {
        try
        {
            return await _context
                .Set<ChartOfAccount>()
                .Where(c => c.Code.StartsWith(parentCode + "."))
                .Select(c => c.Code)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ServiceUnavailableException("Serviço temporariamente indisponível. Tente novamente mais tarde.", ex);
        }
    }

}
