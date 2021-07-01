using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Продукт")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Display(Name = "Слика")]
        public string PictureUrl { get; set; }
        [Display(Name = "Боја")]
        public string Color { get; set; }
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        [Display(Name = "Попуст")]
        public decimal CenaPopust { get; set; }
        public ICollection<ProductSize> Sizes { get; set; }
    }
}
