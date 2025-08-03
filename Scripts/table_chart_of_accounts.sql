CREATE TABLE "ChartOfAccounts" (
    "Id" SERIAL PRIMARY KEY,
    "Code" VARCHAR(255) NOT NULL UNIQUE,
    "Name" VARCHAR(255) NOT NULL,
    "Description" TEXT,
    "Type" VARCHAR(50) NOT NULL,
    "IsPostable" BOOLEAN NOT NULL,
    "ParentCode" VARCHAR(255),
    "Level" INTEGER GENERATED ALWAYS AS (
        array_length(string_to_array("Code", '.'), 1)
    ) STORED,
    "CreatedAt" TIMESTAMP DEFAULT now(),
    "UpdatedAt" TIMESTAMP DEFAULT now()
);

CREATE INDEX idx_chartofaccounts_parentcode ON "ChartOfAccounts"("ParentCode");
CREATE INDEX idx_chartofaccounts_type ON "ChartOfAccounts"("Type");