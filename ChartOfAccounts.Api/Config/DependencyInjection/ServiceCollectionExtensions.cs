using ChartOfAccounts.Application.Errors;
using ChartOfAccounts.Application.Errors.Converters;
using ChartOfAccounts.Application.Errors.Converters.Interfaces;
using ChartOfAccounts.Application.Errors.Interfaces;
using ChartOfAccounts.Application.Factories;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Exceptions.Interfaces;
using ChartOfAccounts.Application.Handlers.ChartOfAccounts.Interfaces;
using ChartOfAccounts.Application.Interfaces;
using ChartOfAccounts.Application.Services;
using ChartOfAccounts.CrossCutting.Context;
using ChartOfAccounts.CrossCutting.Context.Interfaces;
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

        services.AddScoped<IDeleteValidationHandler, ChildrenValidationHandler>();
        services.AddScoped<IDeleteValidationChainBuilder, DeleteValidationChainBuilder>();

        services.AddScoped<MaxLevelValidationHandler>();
        services.AddScoped<ParentCodeValidationHandler>();
        services.AddScoped<ParentExistenceValidationHandler>();
        services.AddScoped<ParentPostableValidationHandler>();
        services.AddScoped<ParentTypeValidationHandler>();
        services.AddScoped<ICreateValidationChainBuilder, CreateValidationChainBuilder>();

        // Factories
        services.AddScoped<IChartOfAccountFactory, ChartOfAccountFactory>();

        // Registro dos handlers de exceção
        services.AddScoped<IdempotencyMatchHandler>();
        services.AddScoped<CodeExistsHandler>();
        services.AddScoped<IdempotencyConflictHandler>();
        services.AddScoped<DefaultExceptionHandler>();
        services.AddScoped<IExceptionHandlerChainBuilder, ExceptionHandlerChainBuilder>();

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
        services.AddScoped<ITenantDbContextPoolProvider, TenantDbContextPoolProvider>();
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

