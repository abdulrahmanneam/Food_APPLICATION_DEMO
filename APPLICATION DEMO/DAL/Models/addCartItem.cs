using System.ComponentModel.DataAnnotations;

namespace APPLICATION_DEMO.DAL.Models
{
    public class addCartItem
    {
        [Key]
        public   int CrtId { get; set; }
        public Food food { get; set;}     

        public int amount { get; set; }
        public string addCartId { get; set; }  

    }
}
