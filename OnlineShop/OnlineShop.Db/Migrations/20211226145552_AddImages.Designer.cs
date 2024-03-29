﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Db;

namespace OnlineShop.Db.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211226145552_AddImages")]
    partial class AddImages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComparisonProduct", b =>
                {
                    b.Property<Guid>("ComparisonsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsToCompareId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ComparisonsId", "ProductsToCompareId");

                    b.HasIndex("ProductsToCompareId");

                    b.ToTable("ComparisonProduct");
                });

            modelBuilder.Entity("FavouriteProduct", b =>
                {
                    b.Property<Guid>("FavouriteProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FavouritesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FavouriteProductsId", "FavouritesId");

                    b.HasIndex("FavouritesId");

                    b.ToTable("FavouriteProduct");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comparison", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comparisons");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Favourite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Image");

                    b.HasData(
                        new
                        {
                            Id = new Guid("475bffd1-7f4a-4dd1-a327-5f3dd48321b8"),
                            Path = "/Content/Hotdog.jpg",
                            ProductId = new Guid("5db89dfd-0615-453b-b9af-b39835d33c90")
                        },
                        new
                        {
                            Id = new Guid("29e429d0-a84b-42e7-ad8b-75f7e538fcb3"),
                            Path = "/Content/cheeseCake.jpg",
                            ProductId = new Guid("7d7e9765-9be5-4c3d-8ca6-868dfa887543")
                        },
                        new
                        {
                            Id = new Guid("94b23910-deef-4cff-a426-ce687d235d58"),
                            Path = "/Content/cinnamon.jpg",
                            ProductId = new Guid("4baf5f10-89c2-45ef-9e03-1981647b311b")
                        },
                        new
                        {
                            Id = new Guid("d548613f-e19f-4cc3-992b-6851d696d617"),
                            Path = "/Content/summerSalad.jpg",
                            ProductId = new Guid("9d87c586-08bc-4af8-8604-2a1f44931a6e")
                        },
                        new
                        {
                            Id = new Guid("9502fa21-fba5-4fe0-8624-d4f6f0812c0c"),
                            Path = "/Content/greekSalad.jpg",
                            ProductId = new Guid("9eb081b4-7e20-4e90-b5ab-f94d75e82ac9")
                        },
                        new
                        {
                            Id = new Guid("226a38af-16f7-436e-8575-aa034e81c540"),
                            Path = "/Content/Borzsch.png",
                            ProductId = new Guid("b2a5d4ad-73b8-4b1a-9164-9d37758765bd")
                        },
                        new
                        {
                            Id = new Guid("52c5681a-d79b-4cb9-951c-168b19678d90"),
                            Path = "/Content/CabbagePie.png",
                            ProductId = new Guid("b7efb40a-4090-46f1-896a-b84fd7b4fa85")
                        },
                        new
                        {
                            Id = new Guid("ad0bd905-18a1-4731-856e-42895e85676e"),
                            Path = "/Content/ChickenWOK.png",
                            ProductId = new Guid("11fac53d-bee3-4f63-b7e0-7a39d61336e9")
                        },
                        new
                        {
                            Id = new Guid("f48560a1-715e-4d4e-be98-4c57f0ff7979"),
                            Path = "/Content/Cutlet.png",
                            ProductId = new Guid("5d031949-6efb-4084-b2ce-7c01622ec7d7")
                        },
                        new
                        {
                            Id = new Guid("5d05cfdd-dae1-4837-a190-0e4e3d6a640e"),
                            Path = "/Content/Kharcho.png",
                            ProductId = new Guid("db5c6a11-7b3a-463e-add5-01b628cb30e6")
                        },
                        new
                        {
                            Id = new Guid("c35416cf-ff64-4c98-bead-bfe824486f20"),
                            Path = "/Content/MashedPotato.png",
                            ProductId = new Guid("9bb66902-d72c-422e-b18a-80d1ce411c12")
                        },
                        new
                        {
                            Id = new Guid("9516e3dd-67a6-42cd-a13a-a198417700dd"),
                            Path = "/Content/MorseBerry.png",
                            ProductId = new Guid("061dde30-b3f1-45ef-b00b-d70e35d7d9ac")
                        },
                        new
                        {
                            Id = new Guid("d24a2471-3c2f-4d2b-ac24-3f71d072299d"),
                            Path = "/Content/MorseBuckthorn.png",
                            ProductId = new Guid("c589b5b2-ceda-439a-beed-66fc9ef9b52c")
                        },
                        new
                        {
                            Id = new Guid("dbc848b0-e4b3-42df-9fd7-c54c6c4765df"),
                            Path = "/Content/Pancake.png",
                            ProductId = new Guid("b46a8ceb-650c-46fb-a9d5-1d081ce4e335")
                        },
                        new
                        {
                            Id = new Guid("39278df9-2c10-49d4-ab41-fe93ca6a10f9"),
                            Path = "/Content/Rice.png",
                            ProductId = new Guid("dcef4e05-4a34-4a26-99b0-592bf38174cb")
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.OrderWithContacts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserDeliveryInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("UserDeliveryInfoId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5db89dfd-0615-453b-b9af-b39835d33c90"),
                            Category = 5,
                            Cost = 69m,
                            Description = "Сочная сосиска в хрустящей булке",
                            Name = "Сосиска в тесте"
                        },
                        new
                        {
                            Id = new Guid("7d7e9765-9be5-4c3d-8ca6-868dfa887543"),
                            Category = 0,
                            Cost = 174m,
                            Description = "Нежный творожный поцелуй со вкусом вишни",
                            Name = "Творожная запеканка"
                        },
                        new
                        {
                            Id = new Guid("4baf5f10-89c2-45ef-9e03-1981647b311b"),
                            Category = 5,
                            Cost = 92m,
                            Description = "Идеальный десерт для завершения любой трапезы",
                            Name = "Булочка с корицей"
                        },
                        new
                        {
                            Id = new Guid("9d87c586-08bc-4af8-8604-2a1f44931a6e"),
                            Category = 1,
                            Cost = 64m,
                            Description = "Помидоры, огурцы, листья салата. Просто и вкусно",
                            Name = "Овощной салат"
                        },
                        new
                        {
                            Id = new Guid("9eb081b4-7e20-4e90-b5ab-f94d75e82ac9"),
                            Category = 1,
                            Cost = 94m,
                            Description = "Вечная классика. Свежие овощи идеально сочетаются с соленым сыром фета",
                            Name = "Греческий салат"
                        },
                        new
                        {
                            Id = new Guid("b2a5d4ad-73b8-4b1a-9164-9d37758765bd"),
                            Category = 2,
                            Cost = 124m,
                            Description = "Густой борщ со сметаной",
                            Name = "Борщ"
                        },
                        new
                        {
                            Id = new Guid("b7efb40a-4090-46f1-896a-b84fd7b4fa85"),
                            Category = 5,
                            Cost = 69m,
                            Description = "Хрустящее тесто и мнооого начинки",
                            Name = "Пирожок с капустой"
                        },
                        new
                        {
                            Id = new Guid("11fac53d-bee3-4f63-b7e0-7a39d61336e9"),
                            Category = 3,
                            Cost = 170m,
                            Description = "Много, вкусно, сытно и ещё раз вкусно",
                            Name = "WOK с курицей и овощами"
                        },
                        new
                        {
                            Id = new Guid("5d031949-6efb-4084-b2ce-7c01622ec7d7"),
                            Category = 3,
                            Cost = 120m,
                            Description = "Свинина, говядина, яйцо, булка, специи - всё на своём месте",
                            Name = "Домашняя котлета"
                        },
                        new
                        {
                            Id = new Guid("db5c6a11-7b3a-463e-add5-01b628cb30e6"),
                            Category = 2,
                            Cost = 124m,
                            Description = "Ароматная встреча бульона, курицы, риса, морковки и специй",
                            Name = "Харчо"
                        },
                        new
                        {
                            Id = new Guid("9bb66902-d72c-422e-b18a-80d1ce411c12"),
                            Category = 4,
                            Cost = 84m,
                            Description = "Вкусное сливочное картофельное пюре. Отсутствие комочков гаранитруем!",
                            Name = "Картофельное пюре"
                        },
                        new
                        {
                            Id = new Guid("061dde30-b3f1-45ef-b00b-d70e35d7d9ac"),
                            Category = 6,
                            Cost = 50m,
                            Description = "Прекрасно освежает и утоляет жажду. Порция 0,2л",
                            Name = "Ягодный морс"
                        },
                        new
                        {
                            Id = new Guid("c589b5b2-ceda-439a-beed-66fc9ef9b52c"),
                            Category = 6,
                            Cost = 50m,
                            Description = "Насыщенный и немного терпкий вкус. всеё как полагается облепихе. Порция 0,2л",
                            Name = "Облепиховый морс"
                        },
                        new
                        {
                            Id = new Guid("b46a8ceb-650c-46fb-a9d5-1d081ce4e335"),
                            Category = 0,
                            Cost = 95m,
                            Description = "Фирменные толстые нежные блинчики с топпингом на выбор",
                            Name = "Панкейки с топпингом"
                        },
                        new
                        {
                            Id = new Guid("dcef4e05-4a34-4a26-99b0-592bf38174cb"),
                            Category = 4,
                            Cost = 54m,
                            Description = "Простой питательный гарнир. Подхлдит к любому горячему блюду",
                            Name = "Рис рассыпчатый"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.UserDeliveryInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlatNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserDeliveryInfos");
                });

            modelBuilder.Entity("ComparisonProduct", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Comparison", null)
                        .WithMany()
                        .HasForeignKey("ComparisonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsToCompareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FavouriteProduct", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("FavouriteProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.Favourite", null)
                        .WithMany()
                        .HasForeignKey("FavouritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Cart", null)
                        .WithMany("Items")
                        .HasForeignKey("CartId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("CartsItems")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.OrderWithContacts", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId");

                    b.HasOne("OnlineShop.Db.Models.UserDeliveryInfo", "UserDeliveryInfo")
                        .WithMany()
                        .HasForeignKey("UserDeliveryInfoId");

                    b.Navigation("Cart");

                    b.Navigation("UserDeliveryInfo");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Navigation("CartsItems");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
