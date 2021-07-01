using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ShoppingCartVM
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<ProductSize> ProductSizes { get; set; }
        [Display(Name = "Количина")]
        public int Quantity { get; set; }
        public SelectList ListSizes { get; set; }
        [Display(Name = "Величина")]
        public int SizeId { get; set; }
    }
}
