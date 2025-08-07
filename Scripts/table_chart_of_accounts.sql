DROP TABLE IF EXISTS "ChartOfAccounts";

CREATE TABLE "ChartOfAccounts" (
    "Id" SERIAL PRIMARY KEY,
    "IdempotencyKey" UUID NOT NULL,
    "Code" VARCHAR(1024) NOT NULL,
    "Name" VARCHAR(255) NOT NULL,
    "CodeNormalized" VARCHAR(1024) NOT NULL,
    "Type" VARCHAR(50) NOT NULL,
    "IsPostable" BOOLEAN NOT NULL,
    "ParentCode" VARCHAR(1024),
    "RequestId" VARCHAR(32) NOT NULL,
    "TenantId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT now(),
    "UpdatedAt" TIMESTAMP DEFAULT now()
);

CREATE INDEX idx_chartofaccounts_code           ON "ChartOfAccounts"("Code");
CREATE INDEX idx_chartofaccounts_name           ON "ChartOfAccounts"("Name");
CREATE INDEX idx_chartofaccounts_parentcode     ON "ChartOfAccounts"("ParentCode");
CREATE INDEX idx_chartofaccounts_codenormalized ON "ChartOfAccounts"("CodeNormalized");
CREATE INDEX idx_chartofaccounts_ispostable     ON "ChartOfAccounts"("IsPostable");
CREATE INDEX idx_chartofaccounts_tenantid       ON "ChartOfAccounts"("TenantId");

CREATE UNIQUE INDEX ux_chartofaccounts_tenantid_code ON "ChartOfAccounts" ("TenantId", "Code");
CREATE UNIQUE INDEX ux_chartofaccounts_tenantid_idempotencykey ON "ChartOfAccounts" ("TenantId", "IdempotencyKey");