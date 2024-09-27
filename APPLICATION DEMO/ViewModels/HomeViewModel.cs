using APPLICATION_DEMO.DAL.Models;

namespace APPLICATION_DEMO.ViewModels
{
    public class HomeViewModel
    {
        
            public List<string> CarouselImages { get; set; }
            public List<Food> PreferredFood { get; set; } // Assuming you already have this
        

        //public IEnumerable<Food> PreferredFood { get; set;}
    }
}
