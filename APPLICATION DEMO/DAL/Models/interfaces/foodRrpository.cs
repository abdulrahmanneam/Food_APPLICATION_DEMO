using APPLICATION_DEMO.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace APPLICATION_DEMO.DAL.Models.interfaces
{

    public abstract class foodRrpository
    {
        private readonly FoodDBContext fooddb;

        protected foodRrpository( FoodDBContext fooddb)
        {
            this.fooddb = fooddb;
        }
        public abstract IEnumerable<Food> Foods { get; }
        public abstract IEnumerable<Food> PreferredFood { get; }
        public abstract Food GetFoodByID(int FoodID);
        public void Add(Food food)
        {
             fooddb.Foods.Add(food); // _context هو كائن DbContext
   
        }

    }
    //public class foodRrpository
    //{
    //    public IEnumerable<Food> Foods { get; set; }

    //    public IEnumerable<Food> PreferredDrink { get; set; }

    //    //public abstract Food GetFoodByID(int FoodID); // its same way
    //    public Food GetFoodByID(int FoodID)
    //    {

    //        return Foods.FirstOrDefault(f => f.FoodID == FoodID);
    //    }

    //}
}
