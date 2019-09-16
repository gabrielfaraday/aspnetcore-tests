using System;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MyProject.Data.Context;

namespace MyProject.Api.IntegrationTests.Configuration
{
    public class BaseTestFixture : IDisposable
    {
        public readonly TestServer Server;
        public readonly HttpClient Client;
        public readonly MainContext TestMainContext;

        public BaseTestFixture()
        {
            Server = new TestServer(
                WebHost.CreateDefaultBuilder()
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>());

            TestMainContext = Server.Host.Services.GetService(typeof(MainContext)) as MainContext;

            Client = Server.CreateClient();

            SetupDatabase();
        }

        private void SetupDatabase()
        {
            try
            {
                //Prepare database
            }
            catch
            {
                //TODO: Add a better logging
                // Does nothing
            }
        }

        public void Dispose()
        {
            TestMainContext.Dispose();
            Client.Dispose();
            Server.Dispose();
        }
    }
}