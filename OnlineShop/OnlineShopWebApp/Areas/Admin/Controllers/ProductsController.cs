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
using System.Threading.Tasks;

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

        public async Task<ActionResult> Index()
        {           
            var productsViewModels = await shop.GetAllAsync();
            return View(productsViewModels.ToProductViewModels());
        }

        public async Task<ActionResult> DeleteAsync(Guid productId)
        {
            var product = await shop.TryGetProductAsync(productId);
            await shop.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> EditAsync(Guid productId)
        {
            var product = await shop.TryGetProductAsync(productId);            
            return View(product.ToEditProductViewModel());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Guid productId, EditProductViewModel editedProduct)
        {
            if (ModelState.IsValid)
            {
                var product = editedProduct.ToProductFromEditedProductViewModel();
                var imagePaths = imageProcessing.UploadFiles(editedProduct.ImageFiles, ImageFolders.Products);
                var productBeforeEdition = await shop.TryGetProductAsync(productId);
                product.ImagePaths = productBeforeEdition.Images.Select(x => x.Path).ToList();
                product.ImagePaths.AddRange(imagePaths);
                product.ImagePath.Distinct();
                
                var productDb = product.ToProduct(product.ImagePaths);
                await shop.EditAsync(productId, productDb);
                return View("SuccessfulEdition");
            }
            return RedirectToAction(nameof(EditAsync));
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
                shop.AddAsync(productDb);
                return View("SuccessfulAdding");
            }
            return RedirectToAction(nameof(Add));
        }


    }
}
