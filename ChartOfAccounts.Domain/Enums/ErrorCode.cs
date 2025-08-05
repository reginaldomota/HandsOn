namespace ChartOfAccounts.Domain.Enums;

public enum ErrorCode
{
    // Genéricos
    Unknown = 0,
    ServiceUnavailable = 1,
    InternalServerError = 2,

    // Autenticação
    Unauthorized = 100,
    Forbidden = 101,
    InvalidCredentials = 102,
    TokenExpired = 103,

    // Validação
    ValidationError = 200,
    RequiredFieldMissing = 201,
    InvalidFormat = 202,

    // Dados
    NotFound = 300,
    AlreadyExists = 301,
    Conflict = 302,

    // Banco de dados
    DatabaseConnectionFailed = 400,
    QueryExecutionFailed = 401,

    // Integrações externas
    ExternalServiceUnavailable = 500,
    Timeout = 501
}
