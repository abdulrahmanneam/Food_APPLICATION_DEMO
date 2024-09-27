using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APPLICATION_DEMO.DAL.Models
{
    public class FoodDBContext : IdentityDbContext<IdentityUser>
    {
        public  FoodDBContext(DbContextOptions<FoodDBContext> options) : base(options )
        {
            
        }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<addCartItem> addCartItems { get; set; }

        public DbSet<Order> orders { get; set; }    
        public DbSet<OrderDetail> ordersDetail { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>()
                .Property(f => f.Price)
                .HasPrecision(18, 2); // 18 هو العدد الكلي للأرقام، 2 هو عدد الأرقام بعد الفاصلة العشرية
                                      // تحديد الدقة للخاصية OrderTotal في الكيان Order
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTotal)
                .HasColumnType("decimal(18,2)");       // 18 رقم إجمالي، 2 منها بعد الفاصلة العشرية

     
            
            // تحديد الدقة للخاصية Price في الكيان OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)"); // 18 رقم إجمالي، 2 منها بعد الفاصلة العشرية

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Food>()
        //        .Property(f => f.Price)
        //        .HasColumnType("decimal(18,2)"); // تعيين النوع والتحديد بشكل صريح

        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
