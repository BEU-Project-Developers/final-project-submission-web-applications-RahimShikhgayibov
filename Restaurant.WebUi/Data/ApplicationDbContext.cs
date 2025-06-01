using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebUi.Models;


namespace Restaurant.WebUi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<AboutInfo> AboutInfos { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Dish>()
    .Property(d => d.Price)
    .HasColumnType("decimal(10,2)");



            builder.Entity<DishIngredient>()
         .HasKey(di => new { di.DishId, di.IngredientId });

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);



















        }








    }
}
