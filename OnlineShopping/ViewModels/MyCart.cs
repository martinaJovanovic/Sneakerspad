using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class MyCart
    {
        public IEnumerable<ProductSize> ProductSizes { get; set; }
        [Display(Name = "Продукт")]
        public IEnumerable<Product> Products { get; set; }
        [Display(Name = "Величина")]
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    }
}
