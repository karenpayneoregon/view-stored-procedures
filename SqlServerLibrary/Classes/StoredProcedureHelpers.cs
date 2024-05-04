using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using SqlServerLibrary.Extensions;

namespace SqlServerLibrary.Classes;
public class StoredProcedureHelpers
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());

    public string FileName()
    {
        SqlConnectionStringBuilder builder = new(ConnectionString());
        return Path.Combine("StoredProcedures", $"{builder.DataSource.Clean()}__{builder.InitialCatalog}_StoredProcedures.sql");
    }

    public string InitialCatalog()
    {
        SqlConnectionStringBuilder builder = new(ConnectionString());
        
        return $"{builder.DataSource}\\{builder.InitialCatalog}";
    }

    /// <summary>
    /// Read names of stored procedures from database in connection string
    /// </summary>
    /// <returns>List of Stored procedures if any</returns>
    public async Task<List<string>> GetStoredProcedureName(string excludes)
    {
        var statement =
            $"""
            SELECT name AS "ProcedureName"
            FROM sys.sysobjects
            WHERE type = 'P'
              AND LEFT(name, 3) NOT IN ( {excludes} )
            ORDER BY name;
            """;
        
        return (await _cn.QueryAsync<string>(statement)).AsList();

    }

    public async Task<(bool success, List<string> list)> GetStoredProcedureNameSafe(string dbName, string excludes)
    {
        var test = ConnectionReader.Get(dbName);
        await using var cn = new SqlConnection(ConnectionReader.Get(dbName));

        var statement =
            $"""
             SELECT name AS "ProcedureName"
             FROM sys.sysobjects
             WHERE type = 'P'
               AND LEFT(name, 3) NOT IN ( {excludes} )
             ORDER BY name;
             """;

        try
        {
            return (true, (await cn.QueryAsync<string>(statement)).AsList());
        }
        catch (Exception ex)
        {
            return (false, null)!;
        }

    }

    /// <summary>
    /// Get the definition of a stored procedure
    /// </summary>
    /// <param name="procedureName">Name of stored procedure</param>
    /// <returns>Definition of stored procedure</returns>
    public async Task<string?> GetStoredProcedureDefinitionAsync(string db, string procedureName)
    {
        var statement =
            """
            SELECT    c.text
            FROM      sys.syscomments c
            INNER JOIN sys.sysobjects o
               ON o.id = c.id
            WHERE      o.type = 'P'
              AND      o.name = @ProcedureName;
            """;


        SqlConnectionStringBuilder builder = new(ConnectionString());
        builder.InitialCatalog = db;
        _cn.ConnectionString = builder.ConnectionString;
        return (await  _cn.QueryAsync<string>(statement, new { ProcedureName = procedureName}))
            .FirstOrDefault();
    }

    public string? GetStoredProcedureDefinition(string db, string procedureName)
    {
        var statement =
            """
            SELECT    c.text
            FROM      sys.syscomments c
            INNER JOIN sys.sysobjects o
               ON o.id = c.id
            WHERE      o.type = 'P'
              AND      o.name = @ProcedureName;
            """;


        SqlConnectionStringBuilder builder = new(ConnectionString());
        builder.InitialCatalog = db;
        _cn.ConnectionString = builder.ConnectionString;
        return _cn.Query<string>(statement, new { ProcedureName = procedureName })
            .FirstOrDefault();
    }

    public string? GetStoredProcedureDefinition1(string procedureName)
    {
        var statement =
            """
            DECLARE @Lines TABLE (Line NVARCHAR(MAX)) ;
            DECLARE @FullText NVARCHAR(MAX) = '' ;
            
            INSERT @Lines EXEC sp_helptext @ProcedureName ;
            SELECT @FullText = @FullText + Line FROM @Lines ; 
            
            SELECT @FullText ;
            """;

        return _cn.Query<string>(statement, new { ProcedureName = procedureName })
            .FirstOrDefault();
    }

}
