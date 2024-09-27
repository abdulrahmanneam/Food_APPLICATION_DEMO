using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using APPLICATION_DEMO.DAL.Repositories;
using APPLICATION_DEMO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace APPLICATION_DEMO.Controllers
{
    public class FoodController1 : Controller
    {

        private readonly categoryRepository _categoryRepository;
        private readonly FoodDBContext foodDB;
        private readonly foodRrpository _foodRrpository;
        private readonly IWebHostEnvironment hostEnvironment;

        public FoodController1(categoryRepository categoryRepository, FoodDBContext foodDB, foodRrpository foodRrpository, IWebHostEnvironment hostEnvironment)
        {
            _categoryRepository = categoryRepository;
            this.foodDB = foodDB;
            _foodRrpository = foodRrpository;
            this.hostEnvironment = hostEnvironment;
        }
        [Authorize]
        public IActionResult Add()
        {
            var Products = _foodRrpository.Foods.OrderByDescending(p => p.FoodID).ToList();
            return View(Products);
        }
 
        public IActionResult GetByID(int id)
        {
            var GetBy = _foodRrpository.Foods.FirstOrDefault(p => p.FoodID == id);
            return View("FoodDetails", GetBy);

        }
 
        public IActionResult Delete(int id)
        {
            var product = foodDB.Foods.Find(id);
            if (product == null)
            {
                return RedirectToAction("Add", "Food");
            }
            string imgdelete = hostEnvironment.WebRootPath + "/Images/" + product.imageUrl;
            string imgdelete1 = hostEnvironment.WebRootPath + "/Images/" + product.ImageTUrl;
            System.IO.File.Delete(imgdelete);
            System.IO.File.Delete(imgdelete1);

            foodDB.Foods.Remove(product);
            foodDB.SaveChanges(true);
            return RedirectToAction("Add", "Food");


        }

        [HttpGet]
     
        public IActionResult Update(int id, IFormFile? imageFile, IFormFile? imageTFile)
        {
            var product = foodDB.Foods.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }

            FoodListViewModel foodList = new FoodListViewModel()
            {
                FoodID = product.FoodID,
                Name = product.Name,
                Price = product.Price,
                SDescription = product.SDescription,
                LDescription = product.LDescription,
                inStook = product.inStook,
                IsPreferredFood = product.IsPreferredFood,
                imageUrl = imageFile,
                ImageTUrl= imageTFile,

                CategoryID = product.CategoryID,
            };

            ViewBag.ImageFileName = product.imageUrl;
            ViewBag.ImageTUrl = product.ImageTUrl;
            ViewBag.Categories = foodDB.Categories.ToList();

            return View(foodList);
        }

        [HttpPost]
       
        public async Task<IActionResult> Update(FoodListViewModel foodList ,IFormFile? imageFile, IFormFile? imageTFile)
        {

            //if (!ModelState.IsValid)
            //{
            ViewBag.Categories = foodDB.Categories.ToList();
            //return View(foodList);
            //}

            var product = await foodDB.Foods.FindAsync(foodList.FoodID );
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            product.Name = foodList.Name;
            product.Price = foodList.Price;
            product.LDescription = foodList.LDescription;
            product.SDescription = foodList.SDescription;
            product.inStook = foodList.inStook;
            product.IsPreferredFood = foodList.IsPreferredFood;
            product.CategoryID = foodList.CategoryID;

            // التعامل مع الصورة العادية
            if (imageFile != null && imageFile.Length > 0)
            {
                product.imageUrl = await ProcessImageAsync(imageFile, product.imageUrl);
            }

            // التعامل مع الصورة المصغرة
            if (imageTFile != null && imageTFile.Length > 0)
            {
                product.ImageTUrl = await ProcessImageAsync(imageTFile, product.ImageTUrl);
            }

            await foodDB.SaveChangesAsync();
            return RedirectToAction("Add", "Food");
        }

        private async Task<string> ProcessImageAsync(IFormFile imageFile, string oldImageUrl)
        {
            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", oldImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;

            
            //// استرجاع المنتج المراد تعديله من قاعدة البيانات بناءً على FoodID
            //var existingProduct = foodDB.Foods.Find(food.FoodID);
            //if (existingProduct == null)
            //{
            //    return NotFound();
            //}

            //ViewBag.Categories = foodDB.Categories.ToList();

            //// التحقق من وجود الصورة الأولى
            //if (food.imageUrl == null || food.imageUrl.Length == 0 && string.IsNullOrEmpty(existingProduct.imageUrl))
            //{
            //    ModelState.AddModelError("imageUrl", "الصورة الأولى مطلوبة");
            //}

            //// التحقق من وجود الصورة الثانية
            //if (food.ImageTUrl == null || food.ImageTUrl.Length == 0 && string.IsNullOrEmpty(existingProduct.ImageTUrl))
            //{
            //    ModelState.AddModelError("ImageTUrl", "الصورة الثانية مطلوبة");
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View(food);
            //}

            //// حفظ الصورة الأولى (imageUrl) إذا تم تحميل صورة جديدة
            //if (food.imageUrl != null)
            //{
            //    string newFile1 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(food.imageUrl.FileName);
            //    string imageFullPath1 = Path.Combine(hostEnvironment.WebRootPath, "Images", newFile1);

            //    using (var stream1 = new FileStream(imageFullPath1, FileMode.Create))
            //    {
            //        await food.imageUrl.CopyToAsync(stream1);
            //    }

            //    // حذف الصورة القديمة إذا كانت موجودة
            //    if (!string.IsNullOrEmpty(existingProduct.imageUrl))
            //    {
            //        string oldImagePath1 = Path.Combine(hostEnvironment.WebRootPath, "Images", existingProduct.imageUrl);
            //        if (System.IO.File.Exists(oldImagePath1))
            //        {
            //            System.IO.File.Delete(oldImagePath1);
            //        }
            //    }

            //    existingProduct.imageUrl = newFile1; // تعيين المسار الجديد للصورة الأولى
            //}

            //// حفظ الصورة الثانية (ImageTUrl) إذا تم تحميل صورة جديدة
            //if (food.ImageTUrl != null)
            //{
            //    string newFile2 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(food.ImageTUrl.FileName);
            //    string imageFullPath2 = Path.Combine(hostEnvironment.WebRootPath, "Images", newFile2);

            //    using (var stream2 = new FileStream(imageFullPath2, FileMode.Create))
            //    {
            //        await food.ImageTUrl.CopyToAsync(stream2);
            //    }

            //    // حذف الصورة القديمة إذا كانت موجودة
            //    if (!string.IsNullOrEmpty(existingProduct.ImageTUrl))
            //    {
            //        string oldImagePath2 = Path.Combine(hostEnvironment.WebRootPath, "Images", existingProduct.ImageTUrl);
            //        if (System.IO.File.Exists(oldImagePath2))
            //        {
            //            System.IO.File.Delete(oldImagePath2);
            //        }
            //    }

            //    existingProduct.ImageTUrl = newFile2; // تعيين المسار الجديد للصورة الثانية
            //}

            //// تحديث باقي خصائص المنتج
            //existingProduct.Name = food.Name;
            //existingProduct.Price = food.Price;
            //existingProduct.SDescription = food.SDescription;
            //existingProduct.LDescription = food.LDescription;
            //existingProduct.inStook = food.inStook;
            //existingProduct.IsPreferredFood = food.IsPreferredFood;
            //existingProduct.CategoryID = food.CategoryID;

            //// حفظ التعديلات في قاعدة البيانات
            //await foodDB.SaveChangesAsync();

            //// إعادة التوجيه إلى الصفحة الرئيسية
            //return RedirectToAction("Index", "Home");
        }



      

        [HttpGet]
     
        public IActionResult Create()
        {   
            // تمرير الفئات إلى الـ ViewBag
            ViewBag.Categories = foodDB.Categories.ToList();
            return View();
        }

        
        [HttpPost]
      
        public IActionResult Create(FoodListViewModel food)
        {
       
                ViewBag.Categories = foodDB.Categories.ToList();
                
          

            // التحقق من وجود الصورة الأولى
            if (food.imageUrl == null || food.imageUrl.Length == 0)
            {
                ModelState.AddModelError("imageUrl", "الصورة الأولى مطلوبة");
            }

            // التحقق من وجود الصورة الثانية
            if (food.ImageTUrl == null || food.ImageTUrl.Length == 0)
            {
                ModelState.AddModelError("ImageTUrl", "الصورة الثانية مطلوبة");
            }

            // حفظ الصورة الأولى (imageUrl)
            string imageFile1 = null;
            if (food.imageUrl != null)
            {
                string newFile1 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(food.imageUrl.FileName);
                string imageFullPath1 = Path.Combine(hostEnvironment.WebRootPath, "Images", newFile1);

                using (var stream1 = new FileStream(imageFullPath1, FileMode.Create))
                {
                    food.imageUrl.CopyTo(stream1);
                }

                imageFile1 = newFile1; // حفظ المسار للصورة الأولى
            }

            // حفظ الصورة الثانية (ImageTUrl)
            string imageFile2 = null;
            if (food.ImageTUrl != null)
            {
                string newFile2 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(food.ImageTUrl.FileName);
                string imageFullPath2 = Path.Combine(hostEnvironment.WebRootPath, "Images", newFile2);

                using (var stream2 = new FileStream(imageFullPath2, FileMode.Create))
                {
                    food.ImageTUrl.CopyTo(stream2);
                }

                imageFile2 = newFile2; // حفظ المسار للصورة الثانية
            }

            // إنشاء الكائن وإضافة الصور
            Food product = new Food()
            {
                Name = food.Name,
                Price = food.Price,
                SDescription = food.SDescription,
                LDescription = food.LDescription,
                inStook = food.inStook,
                imageUrl = imageFile1, // تعيين الصورة الأولى
                ImageTUrl = imageFile2, // تعيين الصورة الثانية
                IsPreferredFood = food.IsPreferredFood,
                CategoryID = food.CategoryID,
            };

            // حفظ الكائن في قاعدة البيانات
            _foodRrpository.Add(product);
            foodDB.SaveChanges();

            // إعادة التوجيه إلى الصفحة الرئيسية
            return RedirectToAction("Add", "Food");
        }

        public ViewResult List(string category)
            {
                IEnumerable<Food> drinks;
                string currentCategory;

                if (string.IsNullOrEmpty(category))
                {
                    // استرجاع جميع الأطعمة من قاعدة البيانات
                    drinks = _foodRrpository.Foods.OrderBy(p => p.FoodID);
                    currentCategory = "All Foods";
                }
                else
                {
                    // استرجاع الفئة المطابقة من قاعدة البيانات
                    var categoryName = category.ToLower();
                    drinks = categoryName switch
                    {
                        "breakfast" => _foodRrpository.Foods.Where(p => p.Category.Name.Equals("Breakfast")).OrderBy(p => p.Name),
                        "launch" => _foodRrpository.Foods.Where(p => p.Category.Name.Equals("Launch")).OrderBy(p => p.Name),
                        "dinner" => _foodRrpository.Foods.Where(p => p.Category.Name.Equals("Dinner")).OrderBy(p => p.Name),
                        "dessert" => _foodRrpository.Foods.Where(p => p.Category.Name.Equals("Dessert")).OrderBy(p => p.Name),
                        "drinks" => _foodRrpository.Foods.Where(p => p.Category.Name.Equals("Drinks")).OrderBy(p => p.Name),
                        _ => Enumerable.Empty<Food>() // قيمة افتراضية في حال عدم وجود فئة مطابقة
                    };
                    currentCategory = category;
                }

                return View(new FoodListViewModel
                {
                    foods = drinks,
                    CurrwntCategory = currentCategory
                });
            }

        }
    }

