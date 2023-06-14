using System.ComponentModel.DataAnnotations;

namespace P013EStore.Core.Entities
{
    public class Setting : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Site Başlık")]
        public string? Title { get; set; }
        [Display(Name = "Site Açıklama"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Mail Sunucusu")]
        public string? MailServer { get; set; }
        public int Port { get; set; }
        [Display(Name = "Mail Kullanıcı Adı")]
        public string? UserName { get; set;}
        [Display(Name = "Mail Şifresi")]
        public string? Password { get; set; }
        public string? Favicon { get; set; }
        [Display(Name = "Site Logo")]
        public string? Logo { get; set; }
        [Display(Name = "Firma Adresi"), DataType(DataType.MultilineText)]
        public string? Address { get; set; }
        [Display(Name = "Firma Harita Kodu"), DataType(DataType.MultilineText)]
        public string? MapCode { get; set; }
    }
}
