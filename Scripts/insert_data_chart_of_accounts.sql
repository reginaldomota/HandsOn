INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1', 
    'Receitas', 
    'Receita', 
    FALSE, 
    NULL, 
    now(), 
    now(), 
    '001',
    '93178a97-98e3-47c5-be0e-f30062e95037',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.1', 
    'Taxa condominial', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.001',
    'a4f07b3d-a901-4e58-92ed-c4a171a7d777',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.2', 
    'Reserva de dependência', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.002',
    '777d4d43-cf9f-41cd-ba27-284793a84b59',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.3', 
    'Multas', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.003',
    '14f5216b-7d46-47f7-9645-4f7f7303338c',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.4', 
    'Juros', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.004',
    '55433901-58cd-474a-8fe5-e56908dc6741',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.5', 
    'Multa condominial', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.005',
    'b6f78bea-5372-49cb-96a2-41d76bb7787a',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.6', 
    'Água', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.006',
    '6ec6a97e-0b54-416c-9d9a-0059bfaaa1a8',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.7', 
    'Gás', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.007',
    '0464c317-b3aa-40db-b108-dfd0e45d2283',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.8', 
    'Luz e energia', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.008',
    '76ce86c2-a19b-4f65-9fd5-a4355a729a92',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.9', 
    'Fundo de reserva', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.009',
    '2bf292f3-ec48-4e01-91e6-162b9a060304',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.10', 
    'Fundo de obras', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.010',
    '0201dd8d-52ee-4ad2-8e15-95e5ae00922c',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.11', 
    'Correção monetária', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.011',
    '26538782-1825-48dd-8d7e-2c4a559bb651',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.12', 
    'Transferência entre contas', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.012',
    'a51e08a8-e152-44e0-8781-ed43c8681199',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.13', 
    'Pagamento duplicado', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.013',
    'bb351b55-d003-4e22-8dc7-a4b60053cf6a',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.14', 
    'Cobrança', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.014',
    'c8a16f6b-bd9f-43a7-9db1-8429fd974be4',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.15', 
    'Crédito', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.015',
    '78790161-b91f-4ad9-9b64-89620250bb53',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.16', 
    'Água mineral', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.016',
    '9b517f86-b1be-4dfe-9458-2c2427bede15',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.17', 
    'Estorno taxa de resgate', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.017',
    'fcd8da30-1afc-4f2b-8472-89552edf3ad0',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.18', 
    'Acordo', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.018',
    '3d79e852-148e-4d1b-99e7-a14c7ca3498e',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '1.19', 
    'Honorários', 
    'Receita', 
    TRUE, 
    '1', 
    now(), 
    now(), 
    '001.019',
    'a9670358-6bb4-4bd1-89a1-5c31d3c6afdb',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2', 
    'Despesas', 
    'Despesa', 
    FALSE, 
    NULL, 
    now(), 
    now(), 
    '002',
    'fb688a29-1017-4af9-87fd-3ec54560a045',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1', 
    'Com pessoal', 
    'Despesa', 
    FALSE, 
    '2', 
    now(), 
    now(), 
    '002.001',
    'c0add0b0-2c58-4329-87c9-482e183a444c',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.1', 
    'Salário', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.001',
    'e5313685-f6a1-4884-89fa-d399abd05359',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.2', 
    'Adiantamento salarial', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.002',
    '43e922c0-497a-4e71-8985-167707aa1b97',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.3', 
    'Hora extra', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.003',
    '0a53a1ac-4ae4-448c-94a5-0884a415de4b',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.4', 
    'Férias', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.004',
    'e5fca271-d755-4cfa-b531-9f9bdf55befd',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.5', 
    '13º salário', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.005',
    'b00ff5c2-4b87-413d-a315-78de6c7cb443',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.6', 
    'Adiantamento 13º salário', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.006',
    '3ddd4bcc-6ab5-4c89-9599-7015dc524287',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.7', 
    'Adicional de função', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.007',
    '6dc0c545-d490-46d1-bb0c-c13da7de2e8d',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.8', 
    'Aviso prévio', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.008',
    'd67d3e7f-b581-4717-bf48-7f7cd6dc7cec',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.9', 
    'INSS', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.009',
    '40851c34-a616-4f33-83d4-11d5b572d092',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.10', 
    'FGTS', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.010',
    'cbd0d57e-6b97-4527-9095-5fa8b777aa9c',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.11', 
    'PIS', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.011',
    '6fd05e07-aa79-4c8f-a837-36c4aebdcf01',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.12', 
    'Vale refeição', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.012',
    'c833268c-c175-492c-97fb-093e7c6e3ecb',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.13', 
    'Vale transporte', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.013',
    '669a2424-604b-4fb3-aff9-c76b004a1d18',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.14', 
    'Cesta básica', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.014',
    '96ff8b9a-4039-49d5-9911-27c213c2edda',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.1.15', 
    'Acordo trabalhista', 
    'Despesa', 
    TRUE, 
    '2.1', 
    now(), 
    now(), 
    '002.001.015',
    '23b4b65b-6756-404e-b22c-8934b59191e3',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2', 
    'Mensais', 
    'Despesa', 
    FALSE, 
    '2', 
    now(), 
    now(), 
    '002.002',
    '1d2de2fc-e06f-4c10-b2e4-d9fdab3fe4e7',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2.1', 
    'Energia elétrica', 
    'Despesa', 
    TRUE, 
    '2.2', 
    now(), 
    now(), 
    '002.002.001',
    '7528c41e-1757-4d29-93e2-f80d635a727d',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2.2', 
    'Água e esgoto', 
    'Despesa', 
    TRUE, 
    '2.2', 
    now(), 
    now(), 
    '002.002.002',
    '82561b38-ae19-48d4-85ab-c9db62f9ac68',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2.3', 
    'Taxa de administração', 
    'Despesa', 
    TRUE, 
    '2.2', 
    now(), 
    now(), 
    '002.002.003',
    '10b05ca8-fd54-4f38-9389-5cd2d4544f33',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2.4', 
    'Gás', 
    'Despesa', 
    TRUE, 
    '2.2', 
    now(), 
    now(), 
    '002.002.004',
    '0bd9359e-735e-4e73-b268-1e037e55f1cf',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2.5', 
    'Seguro obrigatório', 
    'Despesa', 
    TRUE, 
    '2.2', 
    now(), 
    now(), 
    '002.002.005',
    '7f187ebe-2396-4eaf-80b6-4716479b4ca1',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2.6', 
    'Telefone', 
    'Despesa', 
    TRUE, 
    '2.2', 
    now(), 
    now(), 
    '002.002.006',
    '068c0c12-34b6-494b-8422-366ac20effd7',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.2.7', 
    'Softwares e aplicativos', 
    'Despesa', 
    TRUE, 
    '2.2', 
    now(), 
    now(), 
    '002.002.007',
    '65a7e7de-f627-4b69-8ce1-1012e13fc2e2',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.3', 
    'Manutenção', 
    'Despesa', 
    FALSE, 
    '2', 
    now(), 
    now(), 
    '002.003',
    'c94bcf6c-c818-425c-a4f5-54ba88d6463f',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.3.1', 
    'Elevador', 
    'Despesa', 
    TRUE, 
    '2.3', 
    now(), 
    now(), 
    '002.003.001',
    'ca79f182-c63c-4a56-bc8f-0bdcbedf1bec',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.3.2', 
    'Limpeza e conservação', 
    'Despesa', 
    TRUE, 
    '2.3', 
    now(), 
    now(), 
    '002.003.002',
    'a997b6d4-f12c-48ee-9918-f0a5eabb9d29',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.3.3', 
    'Jardinagem', 
    'Despesa', 
    TRUE, 
    '2.3', 
    now(), 
    now(), 
    '002.003.003',
    'f3d5266a-fdd1-4c31-83c1-715d278b5748',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4', 
    'Diversas', 
    'Despesa', 
    FALSE, 
    '2', 
    now(), 
    now(), 
    '002.004',
    'a024e125-87aa-42d8-9abe-26888115ee23',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4.1', 
    'Honorários de advogado', 
    'Despesa', 
    TRUE, 
    '2.4', 
    now(), 
    now(), 
    '002.004.001',
    '850300df-4f9d-4a26-8a8e-932245474a34',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4.2', 
    'Xerox', 
    'Despesa', 
    TRUE, 
    '2.4', 
    now(), 
    now(), 
    '002.004.002',
    '3b2979b7-cf2f-403a-a364-d0b86c4b5fb4',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4.3', 
    'Correios', 
    'Despesa', 
    TRUE, 
    '2.4', 
    now(), 
    now(), 
    '002.004.003',
    'dc65788d-029c-47ed-9e73-80816341f8a1',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4.4', 
    'Despesas judiciais', 
    'Despesa', 
    TRUE, 
    '2.4', 
    now(), 
    now(), 
    '002.004.004',
    '26f7c184-db61-4dc8-af99-3cd7b92f638f',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4.5', 
    'Multas', 
    'Despesa', 
    TRUE, 
    '2.4', 
    now(), 
    now(), 
    '002.004.005',
    '4f3fc416-58f0-4af0-80bb-aa6bead93111',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4.6', 
    'Juros', 
    'Despesa', 
    TRUE, 
    '2.4', 
    now(), 
    now(), 
    '002.004.006',
    '2b329919-924d-4901-9e4b-ee2d1f88ca44',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '2.4.7', 
    'Transferência entre contas', 
    'Despesa', 
    TRUE, 
    '2.4', 
    now(), 
    now(), 
    '002.004.007',
    '7296d787-af14-4c23-ba43-8ad30d944a4e',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '3', 
    'Despesas bancárias', 
    'Despesa', 
    FALSE, 
    NULL, 
    now(), 
    now(), 
    '003',
    '0ff76cf0-c92a-43f0-80f1-612c93384755',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '3.1', 
    'Registro de boletos', 
    'Despesa', 
    TRUE, 
    '3', 
    now(), 
    now(), 
    '003.001',
    '4d06ac3f-e271-4afc-99c7-dacbb6dc0028',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '3.2', 
    'Processamento de boletos', 
    'Despesa', 
    TRUE, 
    '3', 
    now(), 
    now(), 
    '003.002',
    '68a213df-8d58-4930-a548-c60421e42cd6',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '3.3', 
    'Registro e processamento de boletos', 
    'Despesa', 
    TRUE, 
    '3', 
    now(), 
    now(), 
    '003.003',
    '023dc224-2033-44e6-8f8c-081bd0ce99ee',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '3.4', 
    'Resgates', 
    'Despesa', 
    TRUE, 
    '3', 
    now(), 
    now(), 
    '003.004',
    'f12271ef-2fec-4e89-8372-77d26c841671',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '4', 
    'Outras receitas', 
    'Receita', 
    FALSE, 
    NULL, 
    now(), 
    now(), 
    '004',
    'c2af9c45-7e9a-4568-9f35-b414dc465ea2',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '4.1', 
    'Rendimento de poupança', 
    'Receita', 
    TRUE, 
    '4', 
    now(), 
    now(), 
    '004.001',
    'c8263bc9-0029-4fd0-acb8-ba768bc5c78b',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);

INSERT INTO "ChartOfAccounts" ("Code", "Name", "Type", "IsPostable", "ParentCode", "CreatedAt", "UpdatedAt", "CodeNormalized"
, "IdempotencyKey", "RequestId", "TenantId") 
VALUES (
    '4.2', 
    'Rendimento de investimentos', 
    'Receita', 
    TRUE, 
    '4', 
    now(), 
    now(), 
    '004.002',
    'f913b34f-4af9-432d-9677-aa1c85c575fe',
    'RQI-20250805T000000Z-00000000',
    '6b9a29df-482c-4b3e-9c38-032f3a39f45f'
);