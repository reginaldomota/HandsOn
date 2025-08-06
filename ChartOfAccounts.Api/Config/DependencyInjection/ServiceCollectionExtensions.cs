using ChartOfAccounts.Application.Errors;
using ChartOfAccounts.Application.Errors.Converters;
using ChartOfAccounts.Application.Errors.Converters.Interfaces;
using ChartOfAccounts.Application.Errors.Interfaces;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Application.Services;
using ChartOfAccounts.CrossCutting.Context;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Infrastructure.Data;
using ChartOfAccounts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChartOfAccounts.Api.Config.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IChartOfAccountsService, ChartOfAccountsService>();
        services.AddScoped<IAccountCodeSuggestionService, AccountCodeSuggestionService>();
        services.AddSingleton<IErrorResponseFactory, ErrorResponseFactory>();

        // Registra os conversores de erro
        services.AddSingleton<IErrorResponseConverter, BusinessRuleValidationExceptionConverter>();
        services.AddSingleton<IErrorResponseConverter, ServiceUnavailableExceptionConverter>();
        services.AddSingleton<IErrorResponseConverter, DefaultExceptionConverter>();

        return services;
    }

    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddCrossCuttingServices(this IServiceCollection services)
    {
        services.AddScoped<IRequestContext, RequestContext>();
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
            .AddCrossCuttingServices()
            .AddInfrastructureServices(configuration);

        return services;
    }
}
