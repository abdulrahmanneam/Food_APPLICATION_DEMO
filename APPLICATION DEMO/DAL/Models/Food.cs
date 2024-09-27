using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APPLICATION_DEMO.DAL.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        public string Name { get; set; }
        public string SDescription { get; set; }
        public string LDescription { get; set; }
        public decimal Price { get; set; }
        [MaxLength(100)]
        public string imageUrl { get; set; }
        public string ImageTUrl { get; set; }
        public bool IsPreferredFood { get; set; }
        public bool inStook { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}