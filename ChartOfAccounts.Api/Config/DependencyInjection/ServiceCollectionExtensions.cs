using ChartOfAccounts.Application.Errors;
using ChartOfAccounts.Application.Errors.Converters;
using ChartOfAccounts.Application.Errors.Converters.Interfaces;
using ChartOfAccounts.Application.Errors.Interfaces;
using ChartOfAccounts.Application.Factories;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Application.Services;
using ChartOfAccounts.CrossCutting.Context;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
using ChartOfAccounts.CrossCutting.Tenancy.Interfaces;
using ChartOfAccounts.Domain.Interfaces;
using ChartOfAccounts.Infrastructure.Factories;
using ChartOfAccounts.Infrastructure.Factories.Interfaces;
using ChartOfAccounts.Infrastructure.Repositories;
using ChartOfAccounts.Infrastructure.Tenancy;

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
        services.AddSingleton<IErrorResponseConverter, ErrorHttpRequestConverter>();
        services.AddSingleton<IErrorResponseConverter, DefaultExceptionConverter>();

        // Factories
        services.AddScoped<IChartOfAccountFactory, ChartOfAccountFactory>();

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
        services.AddScoped<ITenantConnectionProvider, TenantConnectionProvider>();
        services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();

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
