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

        public ApiIntegrationTestFixture()
        {
            TempDirectory = Path.Join(Path.GetTempPath(), "MinimalisticFileServer");
            RemoveTestFiles(); // for save measure, the test process could have been killed

            Environment.SetEnvironmentVariable($"{EnvVariablePrefix}{EnvironmentVariables.Path}", TempDirectory);
            Server = SetUpTestServer();
        }

        public TestServer Server { get; }
        private string TempDirectory { get; }

        public void Dispose()
        {
            Server.Dispose();
            Environment.SetEnvironmentVariable($"{EnvVariablePrefix}{EnvironmentVariables.Path}", null);
            Directory.Delete(TempDirectory, true);
        }

        public void ArrangeTestFiles()
        {
            foreach (var file in Directory.GetFiles("Assets/TestFiles"))
                File.Copy(file, Path.Combine(TempDirectory, Path.GetFileName(file)));
        }

        public void RemoveTestFiles()
        {
            if (Directory.Exists(TempDirectory)) Directory.Delete(TempDirectory, true);

            Directory.CreateDirectory(TempDirectory);
        }

        public bool Exists(string filename)
        {
            return File.Exists(Path.Combine(TempDirectory, filename));
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
    }
}