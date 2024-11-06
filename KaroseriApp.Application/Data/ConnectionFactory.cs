using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KaroseriApp.Application.Data;

public class ConnectionFactory(IConfiguration configuration)
{
    public IDbConnection Create() => new SqlConnection(configuration.GetConnectionString("SqlExpress"));
}