namespace Cake.ApiReference.Uploader
{
    public class ApiFileInfo
    {
        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        public string FileMimeType { get; set; }
    }
}