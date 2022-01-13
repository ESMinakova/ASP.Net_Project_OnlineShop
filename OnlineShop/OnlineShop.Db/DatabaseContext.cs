using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        //каждый DbSet - доступ к таблицам
        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Favourite> Favourites { get; set; }

        public DbSet<Comparison> Comparisons { get; set; }

        public DbSet<OrderWithContacts> Orders { get; set; }

        public DbSet<UserDeliveryInfo> UserDeliveryInfos { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            var product1Id = Guid.Parse("5db89dfd-0615-453b-b9af-b39835d33c90");
            var product2Id = Guid.Parse("7d7e9765-9be5-4c3d-8ca6-868dfa887543");
            var product3Id = Guid.Parse("4baf5f10-89c2-45ef-9e03-1981647b311b");
            var product4Id = Guid.Parse("9d87c586-08bc-4af8-8604-2a1f44931a6e");
            var product5Id = Guid.Parse("9eb081b4-7e20-4e90-b5ab-f94d75e82ac9");
            var product6Id = Guid.Parse("b2a5d4ad-73b8-4b1a-9164-9d37758765bd");
            var product7Id = Guid.Parse("b7efb40a-4090-46f1-896a-b84fd7b4fa85");
            var product8Id = Guid.Parse("11fac53d-bee3-4f63-b7e0-7a39d61336e9");
            var product9Id = Guid.Parse("5d031949-6efb-4084-b2ce-7c01622ec7d7");
            var product10Id = Guid.Parse("db5c6a11-7b3a-463e-add5-01b628cb30e6");
            var product11Id = Guid.Parse("9bb66902-d72c-422e-b18a-80d1ce411c12");
            var product12Id = Guid.Parse("061dde30-b3f1-45ef-b00b-d70e35d7d9ac");
            var product13Id = Guid.Parse("c589b5b2-ceda-439a-beed-66fc9ef9b52c");
            var product14Id = Guid.Parse("b46a8ceb-650c-46fb-a9d5-1d081ce4e335");
            var product15Id = Guid.Parse("dcef4e05-4a34-4a26-99b0-592bf38174cb");

            var image1 = new Image
            {
                Id = Guid.Parse("475bffd1-7f4a-4dd1-a327-5f3dd48321b8"),
                Path = "/Content/Hotdog.jpg",
                ProductId = product1Id
            };

            var image2 = new Image
            {
                Id = Guid.Parse("29e429d0-a84b-42e7-ad8b-75f7e538fcb3"),
                Path = "/Content/cheeseCake.jpg",
                ProductId = product2Id
            };

            var image3 = new Image
            {
                Id = Guid.Parse("94b23910-deef-4cff-a426-ce687d235d58"),
                Path = "/Content/cinnamon.jpg",
                ProductId = product3Id
            }; 
            
            var image4 = new Image
            {
                Id = Guid.Parse("d548613f-e19f-4cc3-992b-6851d696d617"),
                Path = "/Content/summerSalad.jpg",
                ProductId = product4Id
            }; 
            
            var image5 = new Image
            {
                Id = Guid.Parse("9502fa21-fba5-4fe0-8624-d4f6f0812c0c"),
                Path = "/Content/greekSalad.jpg",
                ProductId = product5Id
            }; 
            
            var image6 = new Image
            {
                Id = Guid.Parse("226a38af-16f7-436e-8575-aa034e81c540"),
                Path = "/Content/Borzsch.png",
                ProductId = product6Id
            }; 
            
            var image7 = new Image
            {
                Id = Guid.Parse("52c5681a-d79b-4cb9-951c-168b19678d90"),
                Path = "/Content/CabbagePie.png",
                ProductId = product7Id
            }; 
            
            var image8 = new Image
            {
                Id = Guid.Parse("ad0bd905-18a1-4731-856e-42895e85676e"),
                Path = "/Content/ChickenWOK.png",
                ProductId = product8Id
            }; 
            
            var image9 = new Image
            {
                Id = Guid.Parse("f48560a1-715e-4d4e-be98-4c57f0ff7979"),
                Path = "/Content/Cutlet.png",
                ProductId = product9Id
            }; 
            
            var image10 = new Image
            {
                Id = Guid.Parse("5d05cfdd-dae1-4837-a190-0e4e3d6a640e"),
                Path = "/Content/Kharcho.png",
                ProductId = product10Id
            };
            
            var image11 = new Image
            {
                Id = Guid.Parse("c35416cf-ff64-4c98-bead-bfe824486f20"),
                Path = "/Content/MashedPotato.png",
                ProductId = product11Id
            }; 
            
            var image12 = new Image
            {
                Id = Guid.Parse("9516e3dd-67a6-42cd-a13a-a198417700dd"),
                Path = "/Content/MorseBerry.png",
                ProductId = product12Id
            }; 
            
            var image13 = new Image
            {
                Id = Guid.Parse("d24a2471-3c2f-4d2b-ac24-3f71d072299d"),
                Path = "/Content/MorseBuckthorn.png",
                ProductId = product13Id
            }; 
            
            var image14 = new Image
            {
                Id = Guid.Parse("dbc848b0-e4b3-42df-9fd7-c54c6c4765df"),
                Path = "/Content/Pancake.png",
                ProductId = product14Id
            }; 
            
            var image15 = new Image
            {
                Id = Guid.Parse("39278df9-2c10-49d4-ab41-fe93ca6a10f9"),
                Path = "/Content/Rice.png",
                ProductId = product15Id
            };

            modelBuilder.Entity<Image>().HasData(image1, image2, image3, image4, image5, image6, image7,
                image8, image9, image10, image11, image12, image13, image14, image15);


            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product {Id = product1Id, Name = "Сосиска в тесте", Cost = 69, Description = "Сочная сосиска в хрустящей булке", Category = Category.Bakery },
                new Product {Id = product2Id, Name = "Творожная запеканка", Cost = 174, Description = "Нежный творожный поцелуй со вкусом вишни", Category = Category.Breakfast },
                new Product {Id = product3Id, Name = "Булочка с корицей", Cost = 92, Description = "Идеальный десерт для завершения любой трапезы", Category = Category.Bakery },
                new Product {Id = product4Id, Name = "Овощной салат", Cost = 64, Description = "Помидоры, огурцы, листья салата. Просто и вкусно", Category = Category.Salad },
                new Product {Id = product5Id, Name = "Греческий салат", Cost = 94, Description = "Вечная классика. Свежие овощи идеально сочетаются с соленым сыром фета", Category = Category.Salad },
                new Product {Id = product6Id, Name = "Борщ", Cost = 124, Description = "Густой борщ со сметаной", Category = Category.Soup },
                new Product {Id = product7Id, Name = "Пирожок с капустой", Cost = 69, Description = "Хрустящее тесто и мнооого начинки", Category = Category.Bakery },
                new Product {Id = product8Id, Name = "WOK с курицей и овощами", Cost = 170, Description = "Много, вкусно, сытно и ещё раз вкусно", Category = Category.MainDish },
                new Product {Id = product9Id, Name = "Домашняя котлета", Cost = 120, Description = "Свинина, говядина, яйцо, булка, специи - всё на своём месте", Category = Category.MainDish },
                new Product {Id = product10Id, Name = "Харчо", Cost = 124, Description = "Ароматная встреча бульона, курицы, риса, морковки и специй", Category = Category.Soup },
                new Product {Id = product11Id, Name = "Картофельное пюре", Cost = 84, Description = "Вкусное сливочное картофельное пюре. Отсутствие комочков гаранитруем!", Category = Category.SideDish },
                new Product {Id = product12Id, Name = "Ягодный морс", Cost = 50, Description = "Прекрасно освежает и утоляет жажду. Порция 0,2л", Category = Category.Drink },
                new Product {Id = product13Id, Name = "Облепиховый морс", Cost = 50, Description = "Насыщенный и немного терпкий вкус. всеё как полагается облепихе. Порция 0,2л", Category = Category.Drink },
                new Product {Id = product14Id, Name = "Панкейки с топпингом", Cost = 95, Description = "Фирменные толстые нежные блинчики с топпингом на выбор", Category = Category.Breakfast },
                new Product {Id = product15Id, Name = "Рис рассыпчатый", Cost = 54, Description = "Простой питательный гарнир. Подхлдит к любому горячему блюду", Category = Category.SideDish }
            });
        }
    }
}
