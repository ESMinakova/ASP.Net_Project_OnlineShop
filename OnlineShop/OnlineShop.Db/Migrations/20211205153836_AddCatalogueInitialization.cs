using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class AddCatalogueInitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Cost", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { new Guid("5db89dfd-0615-453b-b9af-b39835d33c90"), 5, 69m, "Сочная сосиска в хрустящей булке", "/Content/Hotdog.jpg", "Сосиска в тесте" },
                    { new Guid("7d7e9765-9be5-4c3d-8ca6-868dfa887543"), 0, 174m, "Нежный творожный поцелуй со вкусом вишни", "/Content/cheeseCake.jpg", "Творожная запеканка" },
                    { new Guid("4baf5f10-89c2-45ef-9e03-1981647b311b"), 5, 92m, "Идеальный десерт для завершения любой трапезы", "/Content/cinnamon.jpg", "Булочка с корицей" },
                    { new Guid("9d87c586-08bc-4af8-8604-2a1f44931a6e"), 1, 64m, "Помидоры, огурцы, листья салата. Просто и вкусно", "/Content/summerSalad.jpg", "Овощной салат" },
                    { new Guid("9eb081b4-7e20-4e90-b5ab-f94d75e82ac9"), 1, 94m, "Вечная классика. Свежие овощи идеально сочетаются с соленым сыром фета", "/Content/greekSalad.jpg", "Греческий салат" },
                    { new Guid("b2a5d4ad-73b8-4b1a-9164-9d37758765bd"), 2, 124m, "Густой борщ со сметаной", "/Content/Borzsch.png", "Борщ" },
                    { new Guid("b7efb40a-4090-46f1-896a-b84fd7b4fa85"), 5, 69m, "Хрустящее тесто и мнооого начинки", "/Content/CabbagePie.png", "Пирожок с капустой" },
                    { new Guid("11fac53d-bee3-4f63-b7e0-7a39d61336e9"), 3, 170m, "Много, вкусно, сытно и ещё раз вкусно", "/Content/ChickenWOK.png", "WOK с курицей и овощами" },
                    { new Guid("5d031949-6efb-4084-b2ce-7c01622ec7d7"), 3, 120m, "Свинина, говядина, яйцо, булка, специи - всё на своём месте", "/Content/Cutlet.png", "Домашняя котлета" },
                    { new Guid("db5c6a11-7b3a-463e-add5-01b628cb30e6"), 2, 124m, "Ароматная встреча бульона, курицы, риса, морковки и специй", "/Content/Kharcho.png", "Харчо" },
                    { new Guid("9bb66902-d72c-422e-b18a-80d1ce411c12"), 4, 84m, "Вкусное сливочное картофельное пюре. Отсутствие комочков гаранитруем!", "/Content/MashedPotato.png", "Картофельное пюре" },
                    { new Guid("061dde30-b3f1-45ef-b00b-d70e35d7d9ac"), 6, 50m, "Прекрасно освежает и утоляет жажду. Порция 0,2л", "/Content/MorseBerry.png", "Ягодный морс" },
                    { new Guid("c589b5b2-ceda-439a-beed-66fc9ef9b52c"), 6, 50m, "Насыщенный и немного терпкий вкус. всеё как полагается облепихе. Порция 0,2л", "/Content/MorseBuckthorn.png", "Облепиховый морс" },
                    { new Guid("b46a8ceb-650c-46fb-a9d5-1d081ce4e335"), 0, 95m, "Фирменные толстые нежные блинчики с топпингом на выбор", "/Content/Pancake.png", "Панкейки с топпингом" },
                    { new Guid("dcef4e05-4a34-4a26-99b0-592bf38174cb"), 4, 54m, "Простой питательный гарнир. Подхлдит к любому горячему блюду", "/Content/Rice.png", "Рис рассыпчатый" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("061dde30-b3f1-45ef-b00b-d70e35d7d9ac"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11fac53d-bee3-4f63-b7e0-7a39d61336e9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4baf5f10-89c2-45ef-9e03-1981647b311b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d031949-6efb-4084-b2ce-7c01622ec7d7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5db89dfd-0615-453b-b9af-b39835d33c90"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d7e9765-9be5-4c3d-8ca6-868dfa887543"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9bb66902-d72c-422e-b18a-80d1ce411c12"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9d87c586-08bc-4af8-8604-2a1f44931a6e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9eb081b4-7e20-4e90-b5ab-f94d75e82ac9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2a5d4ad-73b8-4b1a-9164-9d37758765bd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b46a8ceb-650c-46fb-a9d5-1d081ce4e335"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b7efb40a-4090-46f1-896a-b84fd7b4fa85"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c589b5b2-ceda-439a-beed-66fc9ef9b52c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("db5c6a11-7b3a-463e-add5-01b628cb30e6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dcef4e05-4a34-4a26-99b0-592bf38174cb"));
        }
    }
}
