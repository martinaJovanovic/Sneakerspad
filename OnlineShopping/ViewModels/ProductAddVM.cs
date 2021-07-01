using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ProductAddVM
    {
        [Display(Name = "Продукт")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Display(Name = "Слика")]
        public string PictureUrl { get; set; }
        public int Size { get; set; }
        [Display(Name = "Боја")]
        public string Color { get; set; }
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        public IFormFile Picture { get; set; }
    }
}
