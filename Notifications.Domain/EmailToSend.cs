namespace Notifications.Domain
{
    public class EmailToSend
    {
        public string EmailDestinatario { get; set; } = String.Empty;
        public string asunto { get; set; } = String.Empty;
        public string Mensaje { get; set; } = String.Empty;
        public bool Html { get; set; }
        public string HtmlBody { get; set; } = String.Empty;


    }
}