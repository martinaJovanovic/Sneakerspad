using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class ProductSizeVM
    {
        public int ProductId { get; set; }
        [Display(Name = "Величини")]
        public IEnumerable<int> SelectedSizes { get; set; }
        public IEnumerable<SelectListItem> SizeList { get; set; }
    }
}
