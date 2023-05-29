using P013EStore.Core.Entities;
using System.Net;
using System.Net.Mail;

namespace P013EStore.MVCUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact, string konu = "Siteden mesaj geldi")
        {
            SmtpClient smtpClient = new("mail.siteadresi.com", 587); // 1. parametre mail sunucusu, 2. parametre mail portu
            smtpClient.Credentials = new NetworkCredential("email kullanıcı adı" , "email şifre");
            smtpClient.EnableSsl = false; // email sunucusu ssl ile çalışıyorsa true ver
            MailMessage message = new();
            message.From = new MailAddress("info@siteadi.com"); // mesajın gönderildiği adres
            message.To.Add("info@siteadi.com"); // mesajın gönderileceği mail adresi
            message.To.Add("test@siteadi.com"); // 1 den fazla yere mail gönderebiliriz
            message.Subject = konu;
            message.Body = $"Mail Bilgileri : <hr /> Ad Soyad : {contact.Name} {contact.Surname} <hr /> Email : {contact.Email} <hr /> Telefon : {contact.Phone}  <hr /> Mesaj : {contact.Message}  <hr /> Mesaj Tarihi : {contact.CreateDate} "; // gönderilecek mesajın içeriği
            message.IsBodyHtml = true; // Gönderimde html kodu kullandıysak bu ayarı aktif etmeliyiz
            // smtpClient.Send(message); mesajı senkron olarak gönderir
            await smtpClient.SendMailAsync(message); // mesajı asenkron olarak mail attık.
            smtpClient.Dispose(); // nesneyi bellekten at
        }
    }
}
