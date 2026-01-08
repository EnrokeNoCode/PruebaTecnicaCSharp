using POSBasic.Persistence.DTO;
using POSBasic.Persistence.Interface;


namespace POSBasic.Persistence
{
    public class ServiceBusAuth : IServiceBusAuth
    {
        public OracleServiceBusResult Authenticate()
        {
            return new OracleServiceBusResult
            {
                Ok = true,
                Usuario = "example_api",
                Password = "Example_123",
                Tns = "localhost:1521/XEPDB1"
            };
        }
    }

}
