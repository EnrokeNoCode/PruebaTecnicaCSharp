
using POSBasic.Persistence.DTO;

namespace POSBasic.Persistence.Interface
{
    public interface IServiceBusAuth
    {
        OracleServiceBusResult Authenticate();
    }
}
