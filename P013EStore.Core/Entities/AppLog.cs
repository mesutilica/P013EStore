using System.ComponentModel.DataAnnotations;

namespace P013EStore.Core.Entities
{
    public class AppLog : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Oluşma Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
