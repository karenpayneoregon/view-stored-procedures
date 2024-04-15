# About

Provides a console project which scans a database for stored procedures and writes the stored procedures if any to a text file under the application path, StoredProcedures with the `database name_StoredProcedures.sql`.

## Setup

Before running, set the connection string in appsettings.json

```json
{
  "ConnectionsConfiguration": {
    "ActiveEnvironment": "Development",
    "Development": "Data Source=.\\SQLEXPRESS;Initial Catalog=NorthWind2024;Integrated Security=True;Encrypt=False"
  }
}
```

## Screenshot

![x](assets/screenshot.png)

## Code

- Written in Microsoft VS2022
- Dapper for data access

## Enhancements

Could be made into a global `dotnet tool` with [little effort](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-global-tool).