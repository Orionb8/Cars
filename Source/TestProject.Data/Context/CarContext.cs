using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestProject.Interfaces;
using TestProject.Models;

namespace TestProject.Data.Context {
    public class CarContext : DbContext {
        public DbSet<CarModel> Cars { get; set; }
        public DbSet<ColorModel> Colors { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public CarContext(DbContextOptions<CarContext> options)
          : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            #region Colors
            modelBuilder.Entity<ColorModel>().HasData(
                new ColorModel {
                    Id = 1,
                    Name = "Black",
                    CreatedAtTime = System.DateTime.Now,
                    IsDeleted = false
                },
                new ColorModel {
                    Id = 2,
                    Name = "Gray",
                    CreatedAtTime = System.DateTime.Now,
                    IsDeleted = false
                },
                new ColorModel {
                    Id = 3,
                    Name = "Blue",
                    CreatedAtTime = System.DateTime.Now,
                    IsDeleted = false
                },
                new ColorModel {
                    Id = 4,
                    Name = "DarkBlue",
                    CreatedAtTime = System.DateTime.Now,
                    IsDeleted = false
                },
                new ColorModel {
                    Id = 5,
                    Name = "White",
                    CreatedAtTime = System.DateTime.Now,
                    IsDeleted = false
                });
            #endregion
            #region Cars
            modelBuilder.Entity<CarModel>().HasData(
                new CarModel {
                    Id = 1,
                    Name = "Audi A4",
                    BrandName = "Audi",
                    ColorId = 4,
                    CreatedAtTime = System.DateTime.Now,
                    IsDeleted = false
                },
                new CarModel {
                    Id = 2,
                    Name = "S-class",
                    BrandName = "Mercedes-Benz",
                    ColorId = 5,
                    CreatedAtTime = System.DateTime.Now,
                    IsDeleted = false
                });
            #endregion
            #region Users
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel() {
                    Id = 1,
                    UserName = "admin",
                    EmailAddress = "admin@mail.com",
                    Password = "12345",
                    Role = "admin"
                });
            #endregion
            base.OnModelCreating(modelBuilder);
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach(var entityType in entityTypes) {
                if(typeof(IRecoverable).IsAssignableFrom(entityType.ClrType)) {
                    var parameterExpr = Expression.Parameter(entityType.ClrType, "x");
                    var propertyExpr = Expression.Property(parameterExpr, nameof(IRecoverable.IsDeleted));
                    var isFalseExpr = Expression.Equal(propertyExpr, Expression.Constant(false));
                    var delegateType = Expression.GetDelegateType(entityType.ClrType, typeof(bool));
                    var lambda = Expression.Lambda(delegateType, isFalseExpr, parameterExpr);

                    modelBuilder.Entity(entityType.Name).HasQueryFilter(lambda);
                }
            }
        }
    }
}
