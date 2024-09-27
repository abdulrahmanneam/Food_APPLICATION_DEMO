using APPLICATION_DEMO.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APPLICATION_DEMO.DAL
{
    public class DbInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Seed()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<FoodDBContext>();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(Categories.Values);
                    context.SaveChanges();
                }

                if (!context.Foods.Any())
                {
                    context.AddRange(
                        new Food
                        {
                            Name = "koshry",
                            Price = 20.23m,
                            SDescription = "يا مزاج الكشري وحلاوة الكشري",
                            LDescription = "من أفضل الأكلات المشهورة في التقاليد المصرية",
                            Category = Categories["Breakfast"],
                            imageUrl = "f3.jpeg",
                            inStook = true,
                            IsPreferredFood = true,
                            ImageTUrl = "alhelw.jpeg"
                        },
                        new Food
                        {
                            Name = "brg",
                            Price = 14.95M,
                            SDescription = "choose what you want",
                            LDescription = "its something you like to eat",
                            Category = Categories["Launch"],
                            imageUrl = "f5.jpg",
                            inStook = true,
                            IsPreferredFood = true,
                            ImageTUrl = "f2app.jpeg"
                        },
                        new Food
                        {
                            Name = "fISH",
                            Price = 17.95M,
                            SDescription = "choose what you want",
                            LDescription = "its something you like to eat",
                            Category = Categories["Dinner"],
                            imageUrl = "f5.jpg",
                            inStook = true,
                            IsPreferredFood = true,
                            ImageTUrl = "f2app.jpeg"
                        },
                        new Food
                        {
                            Name = "smock",
                            Price = 30.95M,
                            SDescription = "choose what you want",
                            LDescription = "its something you like to eat",
                            Category = Categories["Dessert"],
                            imageUrl = "f5.jpg",
                            inStook = true,
                            IsPreferredFood = true,
                            ImageTUrl = "f2app.jpeg"
                        }
                    );

                    context.SaveChanges();
                }
            }
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var categoriesList = new Category[]
                    {
                        new Category { Name = "Breakfast", Description="all BreakFast Food" },
                        new Category { Name = "Launch", Description="all Launch Food" },
                        new Category { Name = "Dinner", Description="all Dinner Food" },
                        new Category { Name = "Dessert", Description="all Dessert Food" },
                        new Category { Name = "Drinks", Description="all Drinks" },
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (var category in categoriesList)
                    {
                        categories.Add(category.Name, category);
                    }
                }

                return categories;
            }
        }
    }
}
