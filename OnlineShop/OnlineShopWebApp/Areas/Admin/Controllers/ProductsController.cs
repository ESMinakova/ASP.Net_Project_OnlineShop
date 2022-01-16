using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Areas.Catalogue.Models;
using OnlineShopWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly IShop shop;
        private readonly ImageProcessing imageProcessing;

        public ProductsController(IShop shop, ImageProcessing imageProcessing)
        {
            this.shop = shop;
            this.imageProcessing = imageProcessing;
        }
        public IActionResult Index()
        {
            var productsViewModels = shop.GetAll().ToProductViewModels();
            return View(productsViewModels);
        }

        public IActionResult Delete(Guid productId)
        {
            var product = shop.TryGetProduct(productId);
            shop.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid productId)
        {
            var product = shop.TryGetProduct(productId);
            var editProduct = product.ToEditProductViewModel();
            return View(editProduct);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid productId, EditProductViewModel editedProduct)
        {
            if (ModelState.IsValid)
            {
                var product = editedProduct.ToProductFromEditedProductViewModel();
                var imagePaths = imageProcessing.UploadFiles(editedProduct.ImageFiles, ImageFolders.Products);
                product.ImagePaths = shop.TryGetProduct(productId).Images.Select(x => x.Path).ToList();
                product.ImagePaths.AddRange(imagePaths);
                product.ImagePath.Distinct();
                
                var productDb = product.ToProduct(product.ImagePaths);
                shop.Edit(productId, productDb);
                return View("SuccessfulEdition");
            }
            return RedirectToAction(nameof(Edit));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateProductViewModel createdProduct)
        {
            if (ModelState.IsValid)
            {                
                var imagePaths = imageProcessing.UploadFiles(createdProduct.ImageFiles, ImageFolders.Products);
                var product = createdProduct.ToProductFromCreateProductViewModel();
                var productDb = product.ToProduct(imagePaths);
                shop.Add(productDb);
                return View("SuccessfulAdding");
            }
            return RedirectToAction(nameof(Add));
        }


    }
}
