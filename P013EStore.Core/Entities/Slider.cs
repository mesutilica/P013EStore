using System.ComponentModel.DataAnnotations;

namespace P013EStore.Core.Entities
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public string? Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        public string? Link { get; set; }
    }
}
