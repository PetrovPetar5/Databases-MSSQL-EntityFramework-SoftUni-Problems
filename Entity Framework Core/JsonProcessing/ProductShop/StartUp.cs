using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DataTransferObjects;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main(string[] args)
        {
            var productShopContext = new ProductShopContext();
            //productShopContext.Database.EnsureDeleted();
            //productShopContext.Database.EnsureCreated();

            //var usersInput = File.ReadAllText(@"..\..\..\Datasets\users.json");
            //var productsInput = File.ReadAllText(@"..\..\..\Datasets\products.json");
            //var categoriesInput = File.ReadAllText(@"..\..\..\Datasets\categories.json");
            //var categoriesProductsInput = File.ReadAllText(@"..\..\..\Datasets\categories-products.json");

            //ImportUsers(productShopContext, usersInput);
            //ImportProducts(productShopContext, productsInput);
            //ImportCategories(productShopContext, categoriesInput);

            Console.WriteLine(GetCategoriesByProductsCount(productShopContext));
        }

        //Query 7. Export Categories by Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories.Select(category => new
            {
                category = category.Name,
                productsCount = category.CategoryProducts.Count,
                averagePrice = Math.Round(category.CategoryProducts.Average(p => p.Product.Price), 2),
                totalRevenue = Math.Round(category.CategoryProducts.Sum(p => p.Product.Price), 2),
            })
                .OrderByDescending(x => x.productsCount)
                .ThenBy(x=>x.category)
                .ToArray();

            var result = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return result;
        }

        //Query 6. Export Successfully Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer.Id != null))
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    soldProducts = user.ProductsSold.Where(ps => ps.Buyer.Id != null)
                                        .Select(product => new
                                        {
                                            name = product.Name,
                                            price = product.Price,
                                            buyerFirstName = product.Buyer.FirstName,
                                            buyerLastName = product.Buyer.LastName,
                                        }).ToList(),
                })
                .OrderBy(x => x.lastName)
                .ThenBy(x => x.firstName)
                .ToList();

            var result = JsonConvert.SerializeObject(users, Formatting.Indented);

            return result;
        }

        //Query 5. Export Products in Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(product => new
                {
                    name = product.Name,
                    price = product.Price,
                    seller = product.Seller.FirstName + " " + product.Seller.LastName,
                })
                .ToArray()
                .OrderBy(p => p.price);

            var result = JsonConvert.SerializeObject(products, Formatting.Indented);

            return result;
        }

        //Query 4. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string categoriesProductsInput)
        {
            InitiliazeAutoMapper();
            var dtoCategoryProducts = JsonConvert.DeserializeObject<IEnumerable<UserInputCategoryProduct>>(categoriesProductsInput);
            var categoryProducts = mapper.Map<IEnumerable<CategoryProduct>>(dtoCategoryProducts);

            context.AddRange(categoryProducts);
            context.SaveChanges();

            var result = $"Successfully imported {categoryProducts.Count()}";
            return result;
        }

        //Query 3. Import Categories
        public static string ImportCategories(ProductShopContext context, string categoriesInput)
        {
            InitiliazeAutoMapper();
            var dtoCategories = JsonConvert.DeserializeObject<IEnumerable<UserInputCategory>>(categoriesInput);
            var categories = mapper.Map<IEnumerable<Category>>(dtoCategories);
            categories = categories.Where(c => c.Name != null).ToList();

            context.AddRange(categories);
            context.SaveChanges();

            var result = $"Successfully imported {categories.Count()}";
            return result;
        }
        //Query 2. Import Products
        public static string ImportProducts(ProductShopContext context, string productsInput)
        {
            InitiliazeAutoMapper();
            var dtoProducts = JsonConvert.DeserializeObject<IEnumerable<UserInputProduct>>(productsInput);
            var products = mapper.Map<IEnumerable<Product>>(dtoProducts);

            context.AddRange(products);
            context.SaveChanges();

            var result = $"Successfully imported {products.Count()}";
            return result;
        }

        //Query 1. Import Users
        public static string ImportUsers(ProductShopContext context, string usersInput)
        {
            InitiliazeAutoMapper();
            var dtoUsers = JsonConvert.DeserializeObject<IEnumerable<UserInputModel>>(usersInput);
            var users = mapper.Map<IEnumerable<User>>(dtoUsers);
            context.AddRange(users);
            context.SaveChanges();
            var result = $"Successfully imported {users.Count()}";

            return result;
        }

        public static void InitiliazeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            mapper = config.CreateMapper();
        }
    }
}