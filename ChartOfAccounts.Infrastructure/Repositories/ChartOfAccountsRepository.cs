using ChartOfAccounts.Domain.Entities;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChartOfAccounts.Infrastructure.Repositories;

public class ChartOfAccountsRepository : IChartOfAccountsRepository
{
    private readonly AppDbContext _context;

    public ChartOfAccountsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ChartOfAccount>> GetAllAsync()
    {
        return await _context.ChartOfAccounts.ToListAsync();
    }

    public async Task<(List<ChartOfAccount> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        IQueryable<ChartOfAccount> query = _context.ChartOfAccounts.OrderBy(x => x.Code);

        int totalCount = await query.CountAsync();

        List<ChartOfAccount> items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }


    public async Task<ChartOfAccount?> GetByCodeAsync(string code)
    {
        return await _context.Set<ChartOfAccount>()
            .FirstOrDefaultAsync(x => x.Code == code);
    }


    public async Task AddAsync(ChartOfAccount account)
    {
        _context.ChartOfAccounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var entity = await GetByCodeAsync(code);
        if (entity != null)
        {
            _context.ChartOfAccounts.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<string>> GetChildrenCodesAsync(string parentCode)
    {
        return await _context
            .Set<ChartOfAccount>()
            .Where(c => c.Code.StartsWith(parentCode + "."))
            .Select(c => c.Code)
            .ToListAsync();
    }

}
