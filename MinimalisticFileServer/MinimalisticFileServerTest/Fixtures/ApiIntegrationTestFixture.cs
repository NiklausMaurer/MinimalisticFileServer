using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MinimalisticFileServer;

namespace MinimalisticFileServerTest.Fixtures
{
    public class ApiIntegrationTestFixture : IDisposable
    {
        private const string EnvVariablePrefix = "MINIMALISTICFILESERVERTEST_";
        
        public TestServer Server { get; }
        private string TempDirectory { get; }

        public ApiIntegrationTestFixture()
        {
            TempDirectory = SetUpTempDirectory();
            Environment.SetEnvironmentVariable($"{EnvVariablePrefix}{EnvironmentVariables.Path}", TempDirectory);
            Server = SetUpTestServer();
        }

        public void Dispose()
        {
            Server.Dispose();
            Environment.SetEnvironmentVariable($"{EnvVariablePrefix}{EnvironmentVariables.Path}", null);
            Directory.Delete(TempDirectory, true);
        }

        private TestServer SetUpTestServer()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddEnvironmentVariables(EnvVariablePrefix);

            return new TestServer(new WebHostBuilder()
                .UseEnvironment("AutomatedTests")
                .UseConfiguration(configurationBuilder.Build())
                .UseStartup<Startup>());
        }

        private string SetUpTempDirectory()
        {
            var tempDirectory = Path.Join(Path.GetTempPath(), "MinimalisticFileServer");
            
            if (Directory.Exists(tempDirectory)) Directory.Delete(tempDirectory, true);

            Directory.CreateDirectory(tempDirectory);

            foreach (var file in Directory.GetFiles("Assets/TestFiles"))
            {
                File.Copy(file, Path.Combine(tempDirectory, Path.GetFileName(file)));
            }
            
            return tempDirectory;
        }
    }
}