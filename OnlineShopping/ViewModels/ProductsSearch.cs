using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ProductsSearch
    {
        public String Search { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
