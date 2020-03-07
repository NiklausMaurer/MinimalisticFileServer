using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalisticFileServer;
using MinimalisticFileServer.Controllers;
using MinimalisticFileServerTest.Fixtures;
using MinimalisticFileServerTest.TestDoubles;
using Xunit;

namespace MinimalisticFileServerTest
{
    public class FilesControllerTest : IClassFixture<DirectoryFixture>
    {
        private FilesController FilesController { get; }
        private DirectoryFixture DirectoryFixture { get; }


        public FilesControllerTest(DirectoryFixture directoryFixture)
        {
            DirectoryFixture = directoryFixture;
            
            var configurationDummy = new ConfigurationDummy(new Dictionary<string, string>()
            {
                {EnvironmentVariables.PATH, DirectoryFixture.TempDirectory}
            });
            
            FilesController = new FilesController(new LoggerDummy<FilesController>(), configurationDummy);
            FilesController.ControllerContext = new ControllerContext();
            FilesController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact]
        public void TestGet()
        {
            // Act
            var response = this.FilesController.Get().ToArray();
            
            // Assert
            Assert.Equal(4, response.Count());
            Assert.Contains(response, file => file.Url.Contains("File_1_äöüÄÖÜ.pdf"));
            Assert.Contains(response, file => file.Url.Contains("File_2.pdf"));
            Assert.Contains(response, file => file.Url.Contains("File_3.txt"));
            Assert.Contains(response, file => file.Url.Contains("File_4.docx"));
        }
    }
}