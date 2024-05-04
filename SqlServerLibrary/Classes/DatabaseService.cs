using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SqlServerLibrary.Classes;

public class DatabaseService
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());

    public async Task<List<string>> DatabaseNames() 
        => ( await _cn.QueryAsync<string>(SqlStatements.GetDatabaseNames)).AsList();

    /// <summary>
    /// Get names of databases on selected server excluding system databases
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> DatabaseNamesFiltered() =>
        (await _cn.QueryAsync<string>(
            """
            SELECT name
            FROM sys.databases
            WHERE name NOT IN ( 'master', 'tempdb', 'model', 'msdb' )
            """))
        .AsList();
}