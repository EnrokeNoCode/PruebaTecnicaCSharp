using Oracle.ManagedDataAccess.Client;
using POSBasic.Persistence.Interface;

namespace POSBasic.Persistence
{
    public class OracleConnectionFactory : IConnectionFactory
    {
        private readonly IServiceBusAuth _serviceBus;

        public OracleConnectionFactory(IServiceBusAuth serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public OracleConnection GetConnection()
        {
            var result = _serviceBus.Authenticate();

            if (!result.Ok)
                throw new Exception($"ServiceBus error: {result.Error}");

            var connectionString =
                $"User Id={result.Usuario};Password={result.Password};Data Source={result.Tns}";

            return new OracleConnection(connectionString);
        }
    }
}
