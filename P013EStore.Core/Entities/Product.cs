﻿using System.ComponentModel.DataAnnotations;

namespace P013EStore.Core.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Ürün Kodu")]
        public string? ProductCode { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Anasayfa")]
        public bool IsHome { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }
    }
}
