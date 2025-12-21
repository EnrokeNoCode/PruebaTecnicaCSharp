using Microsoft.Extensions.DependencyInjection;
using POSBasic.Services.Interfaces;
using POSBasic.Services;
using POSBasic.Forms;
using POSBasic.Persistence.Interface;
using POSBasic.Persistence;
namespace POSBasic
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddSingleton<IConnectionFactory>(new OracleConnectionFactory("User Id=example_api;Password=Example_123;Data Source=localhost:1521/XEPDB1"));

            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddSingleton<IBuscadorService, BuscadorService>();


            services.AddTransient<FrmInicio>();
            services.AddTransient<FrmVentas>();
            services.AddTransient<FrmProductos>();
            services.AddTransient<FrmClientes>();

            using var serviceProvider = services.BuildServiceProvider();

            var form = serviceProvider.GetRequiredService<FrmInicio>();
            Application.Run(form);
        }
    }
}