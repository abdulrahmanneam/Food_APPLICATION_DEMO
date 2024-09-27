using APPLICATION_DEMO.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace APPLICATION_DEMO.ViewModels
{
    public class FoodListViewModel
    {

        public int FoodID { get; set; }
        public IEnumerable<Food> foods {  get; set; }
        public string CurrwntCategory { get; set; }

        public string Name { get; set; }
        public string SDescription { get; set; }
        public string LDescription { get; set; }
        public decimal Price { get; set; }
        public IFormFile? imageUrl { get; set; }
        public IFormFile? ImageTUrl { get; set; }
        public bool IsPreferredFood { get; set; }
        public bool inStook { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
