namespace MinimalisticFileServer.DataTransferObjects
{
    public class FileDto
    {
        public FileDto(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}