namespace Hw57.Services;

public class EmailService
{
    public void SendEmail(string email, string subject, string text)
    {
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

        string server = "smtp.mail.ru"; 
        int port = 25; 
        bool enableSsl = false; 
        string from = "aktan2004win@mail.ru"; 
        string password = "iWbEEF9Fqi1AiS2VLUMx"; 
        string to = email; 

        var message = new MimeKit.MimeMessage();
        message.From.Add(new MimeKit.MailboxAddress("qwe", from)); 
        message.To.Add(new MimeKit.MailboxAddress("qwe", to));
        message.Subject = subject;
        message.Body = new MimeKit.TextPart("plain")
        {
            Text = text
        };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect(server, port, enableSsl);
            client.Authenticate(from, password);
            client.Send(message);
            client.Disconnect(true);
        }

    }
    
    
}