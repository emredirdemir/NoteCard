using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.Models
{
    public class CategoryViewModel
    {
        public List<Category> categories { get; set; }
        public Category category { get; set; }
        public int SelectedCategory { get; set; }
    }
}
