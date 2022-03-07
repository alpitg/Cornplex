## Azure

## secrets
    
```json
    {
      "KeyVault": {
        "Endpoint": "https://az-free-dev.vault.azure.net/",
        "TenantId": "866a40b9-d4a2-40d2-9e94-ca555a1bfd49-01",
        "ClientId": "20527699-8c12-406d-b7c7-f2faa7dcad52-01",
        "ClientSecret": "6a_7Q~aRYQoe4Sr~vua__S8T4gxNW1Hla_BrF_01"
      }
    }
```

> For local testing
    1. Store the above in your local secret.json file
    2. Configure Azure key vault access in Program.cs file
    3. The try to access the Azure value as `configuration["PostgreSqlDbConnection"]`