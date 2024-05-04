using ConsoleConfigurationLibrary.Classes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SqlServerLibrary.Classes;
internal class ConnectionReader
{

    public static string Get(string dbName)
    {
        var _configuration = Configuration.JsonRoot();

        SqlConnectionStringBuilder builder = new()
        {
            DataSource = _configuration.GetValue<string>("Server:Name"),
            InitialCatalog = dbName,
            IntegratedSecurity = true,
            Encrypt = SqlConnectionEncryptOption.Optional
        };

        return builder.ConnectionString;
    }
}
