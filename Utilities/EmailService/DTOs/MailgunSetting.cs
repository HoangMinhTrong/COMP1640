namespace Utilities.EmailService.DTOs
{
    public class MailgunSetting
    {
        public string APIKey { get; set; }
        public string APIUrl { get; set; }
        public string Domain { get; set; }
        public string From { get; set; }
        public bool Enable { get; set; }
    }
}
