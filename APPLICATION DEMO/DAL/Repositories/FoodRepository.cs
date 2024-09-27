using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using Microsoft.EntityFrameworkCore;

namespace APPLICATION_DEMO.DAL.Repositories
{
    public class FoodRepository : foodRrpository
    {
        private readonly FoodDBContext _dbContext;
        public FoodRepository(FoodDBContext dbContext) : base(dbContext) 
        {

            _dbContext = dbContext;

        }
        public override IEnumerable<Food> Foods => _dbContext.Foods.Include(c=> c.Category);

        //public override IEnumerable<Food> PreferredFood => _dbContext.Foods.Where(f=>f.IsPreferredFood).Include(c=>c.Category);

        public override IEnumerable<Food> PreferredFood =>_dbContext.Foods.Where(f => f.IsPreferredFood).Include(f => f.Category).ToList();

        public override Food GetFoodByID(int foodID) => _dbContext.Foods.FirstOrDefault(p => p.FoodID == foodID);
    }
    public interface IFoodRepository
    {
        IEnumerable<Category> GetAllCategories();
        // Other methods...
    }

}
