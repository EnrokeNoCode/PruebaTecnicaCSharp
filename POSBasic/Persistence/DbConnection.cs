using Oracle.ManagedDataAccess.Client;
using POSBasic.Persistence.Interface;

namespace POSBasic.Persistence
{
    public class OracleConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public OracleConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}