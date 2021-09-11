using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Test.MyProject.BL
{
    internal static class ServiceProviderTest
    {
        private static readonly IServiceProvider ServiceProviderInner;

        public static IServiceProvider GetServiceProvider() => ServiceProviderInner;

        static ServiceProviderTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IConfiguration>(provider => configuration);

            serviceCollection.RegisterInfrastructure();
            serviceCollection.RegisterServices();
            serviceCollection.RegisterAutoMapper();

            ServiceProviderInner = serviceCollection.BuildServiceProvider();

            var contextFactory = ServiceProviderInner.GetService<IContextFactory>();
            Assert.IsTrue(contextFactory != null);
            var context = contextFactory.GetDatabaseContext();
            Assert.IsTrue(context != null);
        }
    }
}
