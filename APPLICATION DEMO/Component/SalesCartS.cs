
using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace APPLICATION_DEMO.Component
{
    public class SalesCartS : ViewComponent
    {
        private readonly SalesCart _salesCart;

        public SalesCartS(SalesCart salesCart)
        {
            _salesCart = salesCart;
        }

        public IViewComponentResult Invoke()
        {
            // الحصول على خدمة SalesCart من خدمة الطلب
            var serviceProvider = HttpContext.RequestServices;
            var cart = SalesCart.GetCart(serviceProvider);

            // إذا كانت SalesCart موجودة في السلة، استخدمها
            if (cart != null)
            {
                _salesCart.AddCartItems = cart.AddCartItems;
            }
            else
            {
                // إذا كانت البيانات غير موجودة، تعيين قائمة فارغة
                _salesCart.AddCartItems = new List<addCartItem>();
            }

            // إعداد نموذج العرض (ViewModel)
            var salesCartViewModel = new SalesCartViewModel
            {
                SalesCart = _salesCart,
                salescarttotal = _salesCart.GetTotal() // تأكد من أن GetTotal() تعيد القيمة الصحيحة
            };

            // تحديد مسار العرض بشكل صريح
            return View("/Views/Shared/Components/SalesCartS/Default.cshtml", salesCartViewModel);
        }
    }
}


//using APPLICATION_DEMO.DAL.Models;
//using APPLICATION_DEMO.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;

//namespace APPLICATION_DEMO.Component
//{
//    public class SalesCartS : ViewComponent
//    {
//        private readonly  SalesCart _salesCart;
//        public SalesCartS(SalesCart salesCart)
//        {
//            _salesCart = salesCart;
//        }   
//        public IViewComponentResult Invoke()
//        {
//            var serviceProvider = HttpContext.RequestServices;
//            var cart = SalesCart.GetCart(serviceProvider);
//            _salesCart.AddCartItems = cart.AddCartItems;

//            var items = cart.AddCartItems;

//            _salesCart.AddCartItems = items;

//            var item =new List<addCartItem>() { new addCartItem() ,new addCartItem() };
//            var salescartViewModle = new SalesCartViewModel
//            {
//                SalesCart = _salesCart,
//                salescarttotal = _salesCart.GetTotal()
//            };
//            return View(salescartViewModle);
//        }
//    }
//}

//using APPLICATION_DEMO.DAL.Models;
//using APPLICATION_DEMO.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;

//namespace APPLICATION_DEMO.Component
//{
//    public class SalesCartS : ViewComponent
//    {
//        private readonly SalesCart _salesCart;

//        public SalesCartS(SalesCart salesCart)
//        {
//            _salesCart = salesCart;
//        }

//        public IViewComponentResult Invoke()
//        {
//            الحصول على خدمة SalesCart من الخدمة
//            var serviceProvider = HttpContext.RequestServices;
//            var cart = SalesCart.GetCart(serviceProvider);

//            إذا كانت SalesCart موجودة في السلة، استخدمها
//            if (cart != null)
//            {
//                _salesCart.AddCartItems = cart.AddCartItems;
//            }
//            else
//            {
//                إذا كانت البيانات غير موجودة، يمكنك تعيين قائمة فارغة أو بيانات وهمية
//                _salesCart.AddCartItems = new List<addCartItem>();
//                var pizza = new Food { Name = "Pizza" };
//                Create a Food object for Pizza
//               var burger = new Food { Name = "Burger" }; // Create a Food object for Burger

//                أضف بعض العناصر الوهمية إلى عربة التسوق

//               _salesCart.AddCartItems = new List<addCartItem>
//                           {
//                                new addCartItem { CrtId = 1, food = pizza, amount = 2, addCartId = "sa" },
//                                new addCartItem { CrtId = 2, food =burger , amount = 1, addCartId = "ah" }
//                           };

//            }

//            استخدام البيانات الفعلية بدلاً من إنشاء بيانات وهمية جديدة
//           var salesCartViewModel = new SalesCartViewModel
//           {
//               SalesCart = _salesCart,
//               salescarttotal = _salesCart.GetTotal() // تأكد من أن GetTotal() تعيد القيمة الصحيحة
//           };

//            return View(salesCartViewModel);
//        }
//    }
//}
