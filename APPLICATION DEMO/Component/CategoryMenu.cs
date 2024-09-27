using APPLICATION_DEMO.DAL.Models.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APPLICATION_DEMO.Component
{
    public class CategoryMenu : ViewComponent
    {
       private readonly categoryRepository _repository;
        public CategoryMenu(categoryRepository categoryRepository)
        {
            _repository = categoryRepository;   
          
        }
        public IViewComponentResult Invoke()
        {
            var categories = _repository.Categories.OrderBy(c => c.Name).ToList();

            return View(categories);    
        }
    }
}
