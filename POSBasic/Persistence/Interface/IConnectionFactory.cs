
using Oracle.ManagedDataAccess.Client;

namespace POSBasic.Persistence.Interface
{
    public interface IConnectionFactory
    {
        OracleConnection GetConnection();
    }
}
