using Oracle.ManagedDataAccess.Client;

namespace POSBasic.Database
{
    public static class OracleConnectionFactory
    {
        private static readonly string _connectionString =
            "User Id=example_api;Password=Example_123;Data Source=localhost:1521/XEPDB1";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}