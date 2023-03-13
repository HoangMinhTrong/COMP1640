namespace COMP1640.ViewModels.Attachment.Responses
{
    public class FileDownLoadResponse
    {
        public FileDownLoadResponse(byte[] data, string type, string name)
        {
            Data = data;
            Type = type;
            Name = name;
        }

        public byte[] Data { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
