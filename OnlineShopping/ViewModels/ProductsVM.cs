using Microsoft.AspNetCore.Http;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ProductsVM
    {
        public IFormFile Picture { get; set; }
        public Product Product { get; set; }
    }
}
