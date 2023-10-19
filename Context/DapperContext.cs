using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace test4.Context
{
    public class DapperContext
    {
       private readonly IConfiguration _configuration;
       private readonly string _connectionString;
       public DapperContext(IConfiguration configuration)
       {
           _configuration = configuration;
           _connectionString = _configuration.GetConnectionString("OracleDBConnection");
       }
       public IDbConnection CreateConnection()
           => new OracleConnection(_connectionString); 
       }
}