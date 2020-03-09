using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalisticFileServer;
using MinimalisticFileServer.Controllers;
using MinimalisticFileServer.DataTransferObjects;
using MinimalisticFileServerTest.Fixtures;
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
                {EnvironmentVariables.Path, DirectoryFixture.TempDirectory}
            });

            FilesController = new FilesController(configurationDummy)
            {
                ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()}
            };
        }

        [Fact]
        public async Task TestGet_all_files()
        {
            // Act
            var response = await FilesController.Get(String.Empty);
            
            // Assert
            Assert.IsType<OkObjectResult>(response);
            var fileDtos = ((IEnumerable<FileDto>)((OkObjectResult) response).Value).ToArray();
            Assert.Equal(4, fileDtos.Count());
            Assert.Contains(fileDtos, f => f.Url.Contains("File_1_äöüÄÖÜ.pdf"));
            Assert.Contains(fileDtos, f => f.Url.Contains("File_2.pdf"));
            Assert.Contains(fileDtos, f => f.Url.Contains("File_3.txt"));
            Assert.Contains(fileDtos, f => f.Url.Contains("File_4.docx"));
        }

        [Fact]
        public async Task TestGet_single_file()
        {
            // Act
            var response = await FilesController.Get("File_2.pdf");
            
            // Assert
            Assert.IsType<FileStreamResult>(response); }
    }
}