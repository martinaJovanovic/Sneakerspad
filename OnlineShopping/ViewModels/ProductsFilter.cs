using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ProductsFilter
    {
        public string Color { get; set; }
        public string Brand { get; set; }
        public decimal CenaOd { get; set; }
        public decimal CenaDo { get; set; }
        public string Sex { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public SelectList Colors { get; set; }
        public SelectList Brands { get; set; }
        public SelectList CeniOd { get; set; }
        public SelectList CeniDo { get; set; }
    }
}
