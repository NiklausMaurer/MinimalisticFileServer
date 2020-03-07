using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MinimalisticFileServer;
using MinimalisticFileServer.Controllers;
using MinimalisticFileServerTest.TestDoubles;
using Newtonsoft.Json.Schema;
using Xunit;
using Xunit.Abstractions;

namespace MinimalisticFileServerTest
{
    public class FilesControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private FilesController FilesController { get; }

        public FilesControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var configurationDummy = new ConfigurationDummy(new Dictionary<string, string>()
            {
                {EnvironmentVariables.PATH, "Assets/TestFiles"}
            });
            
            FilesController = new FilesController(new LoggerDummy<FilesController>(), configurationDummy);
        }

        [Fact]
        public void TestGet()
        {
            // Act
            var response = this.FilesController.Get();
            
            // Assert
            _testOutputHelper.WriteLine(JsonSerializer.Serialize(response));
            Assert.Equal(4, response.Count());
        }
    }
}