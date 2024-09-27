namespace APPLICATION_DEMO.DAL.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
