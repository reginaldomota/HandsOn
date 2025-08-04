using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Application.Services;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using ChartOfAccounts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChartOfAccounts.CrossCutting.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IChartOfAccountsService, ChartOfAccountsService>();
        services.AddScoped<IAccountCodeSuggestionService, AccountCodeSuggestionService>();
        return services;
    }

    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Caso você tenha serviços de domínio com regras específicas, adicione aqui.
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Registra o DbContext com PostgreSQL
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Repositórios
        services.AddScoped<IChartOfAccountsRepository, ChartOfAccountsRepository>();

        return services;
    }

    public static IServiceCollection RegisterAllDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplicationServices()
            .AddDomainServices()
            .AddInfrastructureServices(configuration);

        return services;
    }
}
