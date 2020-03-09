using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MinimalisticFileServer.DataTransferObjects;
using MinimalisticFileServerTest.Fixtures;
using Newtonsoft.Json;
using Xunit;

namespace MinimalisticFileServerTest
{
    public class FilesApiTest : IDisposable, IClassFixture<ApiIntegrationTestFixture>
    {
        private ApiIntegrationTestFixture ApiIntegrationTestFixture { get; }
        private HttpClient Client { get; }
        
        public FilesApiTest(ApiIntegrationTestFixture apiIntegrationTestFixture)
        {
            ApiIntegrationTestFixture = apiIntegrationTestFixture;
            Client = apiIntegrationTestFixture.Server.CreateClient();
        }

        public void Dispose()
        {
            ApiIntegrationTestFixture.RemoveTestFiles();
        }

        [Fact]
        public async Task TestGetAllFiles()
        {
            // Arrange
            ApiIntegrationTestFixture.ArrangeTestFiles();
            var request = new HttpRequestMessage(HttpMethod.Get, "/files");
            
            // Act
            var response = await Client.SendAsync(request);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string content = await response.Content.ReadAsStringAsync();

            FileDto[] files = (JsonConvert.DeserializeObject<IEnumerable<FileDto>>(content)).ToArray();
            Assert.Equal(4, files.Length);
            Assert.Contains(files, file => file.Url.Equals("http://localhost/files/File_1_äöüÄÖÜ.pdf"));
            Assert.Contains(files, file => file.Url.Equals("http://localhost/files/File_2.pdf"));
            Assert.Contains(files, file => file.Url.Equals("http://localhost/files/File_3.txt"));
            Assert.Contains(files, file => file.Url.Equals("http://localhost/files/File_4.docx"));
        }

        [Fact]
        public async Task TestGetExistingSingleFile()
        {
            // Arrange
            ApiIntegrationTestFixture.ArrangeTestFiles();
            var request = new HttpRequestMessage(HttpMethod.Get, "/files/File_3.txt");
            
            // Act
            var response = await Client.SendAsync(request);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2018, response.Content.Headers.ContentLength);
            Assert.Equal("application/octet-stream", response.Content.Headers.ContentType.MediaType);
            string content = await response.Content.ReadAsStringAsync();
            Assert.Contains("File 3 File 3 File 3", content);
        }

        [Fact]
        public async Task TestGetNonExistentSingleFile()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/files/nothing.txt");
            
            // Act
            var response = await Client.SendAsync(request);
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task TestDeleteExistingFile()
        {
            // Arrange
            ApiIntegrationTestFixture.ArrangeTestFiles();
            var request = new HttpRequestMessage(HttpMethod.Delete, "/files/File_2.pdf");
            
            // Act
            var response = await Client.SendAsync(request);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(ApiIntegrationTestFixture.Exists("File2.pdf"));
        }
        
        [Fact]
        public async Task TestDeleteNotExistingFile()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Delete, "/files/nothing.pdf");
            
            // Act
            var response = await Client.SendAsync(request);
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}