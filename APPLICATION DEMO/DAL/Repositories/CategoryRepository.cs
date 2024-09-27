using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using System.Collections.Generic;
using System.Linq;

namespace APPLICATION_DEMO.DAL.Repositories
{
    public class CategoryRepository : categoryRepository
    {
        private readonly FoodDBContext _foodDBContext;

        public CategoryRepository(FoodDBContext foodDBContext)
        {
            _foodDBContext = foodDBContext;
        }

        // تنفيذ الخاصية Categories لعرض قائمة الفئات
        public override IEnumerable<Category> Categories => _foodDBContext.Categories.ToList();
    }
}
