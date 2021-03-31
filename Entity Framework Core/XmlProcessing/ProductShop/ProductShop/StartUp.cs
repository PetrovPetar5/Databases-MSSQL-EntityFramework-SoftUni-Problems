namespace ProductShop
{
    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var usersInput = File.ReadAllText(@".\Datasets\users.xml");
            //var productsInput = File.ReadAllText(@".\Datasets\products.xml");
            //var categoriesInput = File.ReadAllText(@".\Datasets\categories.xml");
            //var categoriesProductsInput = File.ReadAllText(@".\Datasets\categories-products.xml");

            //ImportUsers(context, usersInput);
            //ImportProducts(context, productsInput);
            //ImportCategories(context, categoriesInput);
            //ImportCategoryProducts(context, categoriesProductsInput);

            System.Console.WriteLine(GetUsersWithProducts(context));
        }


        //Query 8. Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportProductByUserModel[]), new XmlRootAttribute("Users"));
            var writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var products = context.Users.Where(x => x.ProductsSold.Count() > 0).Select(u => new ExportProductByUserModel
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Age = u.Age,
                SoldProducts = u.ProductsSold.Select(ps => new ExportSoldProductByUserModel
                {
                    Count = u.ProductsSold.Count,
                    Products = u.ProductsSold.Select(p => new ExportProductUserModel
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .OrderByDescending(x => x.Price)
                    .ToArray()

                }).ToArray()
            })
                .Take(10)
                .ToArray();

            serializer.Serialize(writer, products, ns);

            var result = writer.ToString();
            return result;
        }
        //Query 7. Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportCategoryByProductModel[]), new XmlRootAttribute("Categories"));
            var writter = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var categories = context.Categories.Select(c => new ExportCategoryByProductModel
            {
                Name = c.Name,
                Count = c.CategoryProducts.Count(),
                AveragePrice = c.CategoryProducts.Average(x => x.Product.Price),
                TotalRevenue = c.CategoryProducts.Sum(x => x.Product.Price),
            })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            serializer.Serialize(writter, categories, ns);

            var result = writter.ToString();
            return result;
        }
        //Query 6. Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportSoldProductModel[]), new XmlRootAttribute("Users"));
            var writter = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var users = context.Users.Where(x => x.ProductsSold.Count > 0).Select(user => new ExportSoldProductModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                SoldProducts = user.ProductsSold.Select(product => new ExportSoldModel
                {
                    Name = product.Name,
                    Price = product.Price,
                })
                .ToArray()
            })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToArray();

            serializer.Serialize(writter, users, ns);

            var result = writter.ToString();
            return result;

        }
        //Query 5. Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportProductInRangeModel[]), new XmlRootAttribute("Products"));
            var writter = new StringWriter();

            var products = context.Products.Where(x => x.Price > 500 && x.Price <= 1000).Select(p => new ExportProductInRangeModel
            {
                Name = p.Name,
                Price = p.Price,
                BuyerFullname = p.Buyer.FirstName + " " + p.Buyer.LastName,
            })
                .OrderBy(x => x.Price)
                .Take(10)
                .ToArray();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writter, products, ns);

            var result = writter.ToString();
            return result;
        }

        //Query 4. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CategoryProductInputModel[]), new XmlRootAttribute("CategoryProducts"));
            var reader = new StringReader(inputXml);

            var categoriesProductsDtos = serializer.Deserialize(reader) as CategoryProductInputModel[];
            var categoryProducts = categoriesProductsDtos.Select(x => new CategoryProduct
            {
                CategoryId = x.CategoryId,
                ProductId = x.ProductId,
            })
                .ToArray();

            context.AddRange(categoryProducts);
            context.SaveChanges();

            var result = $"Successfully imported {categoryProducts.Count()}";
            return result;
        }
        //Query 3. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CategoryInputModel[]), new XmlRootAttribute("Categories"));
            var reader = new StringReader(inputXml);

            var categoriesDto = serializer.Deserialize(reader) as CategoryInputModel[];
            var categories = categoriesDto.Select(x => new Category
            {
                Name = x.Name,
            })
                .ToArray();

            context.AddRange(categories);
            context.SaveChanges();

            var result = $"Successfully imported {categories.Count()}";
            return result;
        }

        //Query 2. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ProductInputModel[]), new XmlRootAttribute("Products"));
            var reader = new StringReader(inputXml);

            var productsDtos = serializer.Deserialize(reader) as ProductInputModel[];
            var products = productsDtos.Select(x => new Product
            {
                Name = x.Name,
                Price = x.Price,
                SellerId = x.SellerId,
                BuyerId = x.BuyerId,
            })
                .ToArray();

            context.AddRange(products);
            context.SaveChanges();

            var result = $"Successfully imported {products.Count()}";
            return result;
        }

        //Query 1. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(UserInputModel[]), new XmlRootAttribute("Users"));
            var reader = new StringReader(inputXml);

            var usersModel = serializer.Deserialize(reader) as UserInputModel[];
            var usersToImport = usersModel.Select(x => new User
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
            }).ToArray();

            context.AddRange(usersToImport);
            context.SaveChanges();

            var result = $"Successfully imported {usersToImport.Count()}";
            return result;
        }
    }
}