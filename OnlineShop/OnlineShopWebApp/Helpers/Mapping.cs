using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Areas.Catalogue.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagePaths = product.Images.Select(x => x.Path).ToList(),                 
                Category = (CategoryViewModel)Enum.Parse(typeof(CategoryViewModel), product.Category.ToString(), true)
            };
        }
        public static Product ToProduct(this ProductViewModel product, List<string> imagesPaths)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Images = ToImages(imagesPaths),
                Category = (Category)Enum.Parse(typeof(Category), product.Category.ToString(), true)
            };
            return newProduct;
        }

        private static List<Image> ToImages(List<string> imagesPaths)
        {
            return imagesPaths.Select(x => new Image { Path = x }).ToList();
        }

        public static List<ProductViewModel> ToProductViewModels(this List<Product> products)
        {
            if (products != null) 
                return products.Select(x => ToProductViewModel(x)).ToList();
            return null;
        }

        public static CartViewModel ToCartViewModel(this Cart cart)
        {
            if (cart == null)
                return null;
            return new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = ToCartItemVewModels(cart.Items)
            };
        }

        public static List<CartItemViewModel> ToCartItemVewModels(this List<CartItem> items)
        {
            if (items != null)
                return items.Select(x => ToCartItemViewModel(x)).ToList();
            return null;
        }

        public static CartItemViewModel ToCartItemViewModel(this CartItem cartItem)
        {
            return new CartItemViewModel
            {
                Id = cartItem.Id,
                Product = ToProductViewModel(cartItem.Product),
                Amount = cartItem.Amount,
            };
        }


        public static FavouriteViewModel ToFavouriteViewModel(this Favourite favourite)
        {
            if(favourite != null)
            {
                return new FavouriteViewModel
                {
                    FavouriteProducts = favourite.FavouriteProducts.Select(x => ToProductViewModel(x)).ToList(),
                    UserId = favourite.UserId
                };
            }
            return null;
        }

        public static ComparisonViewModel ToComparisonViewModel(this Comparison comparison)
        {
            return new ComparisonViewModel
            {
                ProductsToCompare = comparison.ProductsToCompare.Select(x => ToProductViewModel(x)).ToList(),                
                UserId = comparison.UserId
            };
        }

        public static UserDeliveryInfoViewModel ToUserDeliveryViewModel(this UserDeliveryInfo userDeliveryInfo)
        {
            return new UserDeliveryInfoViewModel
            {
                Name = userDeliveryInfo.Name,
                Phone = userDeliveryInfo.Phone,
                Street = userDeliveryInfo.Street,
                HouseNumber = userDeliveryInfo.HouseNumber,
                FlatNumber = userDeliveryInfo.HouseNumber,
                Pay = userDeliveryInfo.Pay,
                Comments = userDeliveryInfo.Comments,
                IsAgree = true
            };
        }

        public static UserDeliveryInfo ToUserDeliveryInfo(this UserDeliveryInfoViewModel userDeliveryInfo)
        {
            return new UserDeliveryInfo
            {
                Name = userDeliveryInfo.Name,
                Phone = userDeliveryInfo.Phone,
                Street = userDeliveryInfo.Street,
                HouseNumber = userDeliveryInfo.HouseNumber,
                FlatNumber = userDeliveryInfo.HouseNumber,
                Pay = userDeliveryInfo.Pay,
                Comments = userDeliveryInfo.Comments,                
            };
        }

        public static OrderWithContactsViewModel ToOrderWithContactsViewModel(this OrderWithContacts order)
        {
            return new OrderWithContactsViewModel
            {
                Id = order.Id,
                UserDeliveryInfo = ToUserDeliveryViewModel(order.UserDeliveryInfo),
                Cart = ToCartViewModel(order.Cart),
                OrderTime = order.OrderTime,
                Status = (OrderStatusViewModel)Enum.Parse(typeof(OrderStatusViewModel), order.Status.ToString(), true),
            };
        }

        public static OrderWithContacts ToOrderWithContacts(this OrderWithContactsViewModel order)
        {
            return new OrderWithContacts
            {                
                UserDeliveryInfo = ToUserDeliveryInfo(order.UserDeliveryInfo),                
                OrderTime = order.OrderTime,
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), order.Status.ToString(), true),
            };
        }

        public static List<OrderWithContactsViewModel> ToOrderWithContactsViewModels(this List<OrderWithContacts> orders)
        {
            if (orders != null)
                return orders.Select(x => ToOrderWithContactsViewModel(x)).ToList();
            return null;
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Login = user.Email,
                RegistrationDate = user.RegistrationDate,
                Name = user.UserName,
                Phone = user.PhoneNumber,                
            };
        } 
        
        public static RoleViewModel ToRoleViewModel (this IdentityRole role)
        {
            return new RoleViewModel { Name = role.Name };
        }

        public static ProductViewModel ToProductFromCreateProductViewModel(this CreateProductViewModel createdProduct)
        {
            return new ProductViewModel
            {
                Id = Guid.NewGuid(),
                Name = createdProduct.Name,
                Cost = createdProduct.Cost,
                Description = createdProduct.Description,
                Category = createdProduct.Category,
                ImagePaths = new List<string>(),                
            };
        }

        public static EditProductViewModel ToEditProductViewModel(this Product product)
        {
            return new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Category = (CategoryViewModel)Enum.Parse(typeof(CategoryViewModel), product.Category.ToString(), true),                
                ImagePaths = product.Images.Select(x => x.Path).ToList()                 
            };
        }

        public static ProductViewModel ToProductFromEditedProductViewModel(this EditProductViewModel editedProduct)
        {
            return new ProductViewModel
            {
                Id = editedProduct.Id,
                Name = editedProduct.Name,
                Cost = editedProduct.Cost,
                Description = editedProduct.Description,
                Category = editedProduct.Category,
                ImagePaths = editedProduct.ImagePaths.Select(x => x).ToList(),                
            };
        }
    }
}
