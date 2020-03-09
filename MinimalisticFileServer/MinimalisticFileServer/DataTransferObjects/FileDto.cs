namespace MinimalisticFileServer.DataTransferObjects
{
    public class FileDto
    {
        public string Url { get; set; }

        public FileDto(string url)
        {
            Url = url;
        }
    }
}