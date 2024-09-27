using APPLICATION_DEMO.DAL.Models;
using System.Collections.Generic;

namespace APPLICATION_DEMO.DAL.Models.interfaces
{
    public abstract class categoryRepository
    {
        // خاصية مجردة لعرض قائمة الفئات
        public abstract IEnumerable<Category> Categories { get; }
    }
}
