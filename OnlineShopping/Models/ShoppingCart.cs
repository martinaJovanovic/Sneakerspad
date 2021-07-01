using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ProductSizeId { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ProductSize ProductSize { get; set; }
        [Display(Name = "Количина")]
        [Range(1, 100)]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public Product Product { get; set; }
        public Size Size { get; set; }
    }
}
