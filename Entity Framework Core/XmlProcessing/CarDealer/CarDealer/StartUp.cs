using CarDealer.Data;
using CarDealer.DTO.Input;
using CarDealer.DTO.Output;
using CarDealer.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var carDealerContext = new CarDealerContext();
            //carDealerContext.Database.EnsureDeleted();
            //carDealerContext.Database.EnsureCreated();

            //var inputXmlSuppliers = File.ReadAllText(@".\Datasets\suppliers.xml");
            //var inputXmlParts = File.ReadAllText(@".\Datasets\parts.xml");
            //var inputXmlCars = File.ReadAllText(@".\Datasets\cars.xml");
            //var inputXmlCustomers = File.ReadAllText(@".\Datasets\customers.xml");
            //var inputXmlSales = File.ReadAllText(@".\Datasets\sales.xml");

            //ImportSuppliers(carDealerContext, inputXmlSuppliers);
            //ImportParts(carDealerContext, inputXmlParts);
            //ImportCars(carDealerContext, inputXmlCars);
            //ImportCustomers(carDealerContext, inputXmlCustomers);
            //ImportSales(carDealerContext, inputXmlSales);
            System.Console.WriteLine(GetCarsWithTheirListOfParts(carDealerContext));

        }

        //Query 9. Cars with Their List of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsPartsDTO = context.Cars.Select(c => new CarWithPartsOutputModel
            {
                Make = c.Make,
                Model = c.Model,
                TraveledDistance = c.TravelledDistance,
                Parts = c.PartCars.Select(x => new PartOutputModel
                {
                    Name = x.Part.Name,
                    Price = x.Part.Price,

                })
                .OrderByDescending(x => x.Price)
                .ToArray()
            })
                .OrderByDescending(x => x.TraveledDistance)
                .ThenBy(x => x.Model)
                .Take(5)
                .ToArray();

            var writter = new StringWriter();
            var serializer = new XmlSerializer(typeof(CarWithPartsOutputModel[]), new XmlRootAttribute("cars"));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writter, carsPartsDTO, ns);

            var result = writter.ToString();
            return result;
        }

        //Query 8. GetLocalSuppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var LocalSuppliersDTO = context.Suppliers.Where(x => x.IsImporter == false).Select(supplier => new LocalSuppliersOutputModel
            {
                Id = supplier.Id,
                Name = supplier.Name,
                PartsCount = supplier.Parts.Count(),
            })
               .ToArray();

            var serializer = new XmlSerializer(typeof(LocalSuppliersOutputModel[]), new XmlRootAttribute("suppliers"));
            var writter = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writter, LocalSuppliersDTO, ns);

            var result = writter.ToString();
            return result;
        }
        //Query 7. Cars from make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var modelToLookFor = "BMW";
            var serializer = new XmlSerializer(typeof(CarBMVOutputModel[]), new XmlRootAttribute("cars"));
            var carsBMW = context.Cars.Where(x => x.Make == modelToLookFor).Select(bmw => new CarBMVOutputModel
            {
                Id = bmw.Id,
                Model = bmw.Model,
                TravelledDistance = bmw.TravelledDistance,
            })

                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToArray();

            var writter = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writter, carsBMW, ns);

            var result = writter.ToString();
            return result;
        }

        //Query 6. Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var serializer = new XmlSerializer(typeof(CarOutputModel[]), new XmlRootAttribute("cars"));
            var millageLimit = 2000000;
            var carsOverGivenMillage = context.Cars.Where(car => car.TravelledDistance > millageLimit).Select(x => new CarOutputModel
            {
                Make = x.Make,
                Model = x.Model,
                TraveledDistance = x.TravelledDistance,
            })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();

            var textWrite = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(textWrite, carsOverGivenMillage, ns);

            var result = textWrite.ToString();
            return result;
        }
        //Query 5. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(SaleInputModel[]), new XmlRootAttribute("Sales"));
            var salesInput = new StringReader(inputXml);
            var SalesDTO = serializer.Deserialize(salesInput) as SaleInputModel[];

            var carIds = context.Cars.Select(car => car.Id).ToList();
            var sales = SalesDTO.Where(s => carIds.Any(c => c == s.CarId)).Select(x => new Sale
            {
                CarId = x.CarId,
                CustomerId = x.CustomerId,
                Discount = x.Discount,
            })
                .ToList();

            context.AddRange(sales);
            context.SaveChanges();

            var result = $"Successfully imported {sales.Count()}";
            return result;
        }
        //Query 4. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CustomerInputModel[]), new XmlRootAttribute("Customers"));
            var customersInput = new StringReader(inputXml);
            var customersDTO = serializer.Deserialize(customersInput) as CustomerInputModel[];

            var customers = customersDTO.Select(customer => new Customer
            {
                Name = customer.Name,
                IsYoungDriver = customer.IsYoungDriver,
                BirthDate = customer.BirthDate,
            })
                .ToList();

            context.AddRange(customers);
            context.SaveChanges();

            var result = $"Successfully imported {customers.Count()}";
            return result;
        }
        //Query 3. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CarModel[]), new XmlRootAttribute("Cars"));
            var carsInput = new StringReader(inputXml);
            var carsDTO = serializer.Deserialize(carsInput) as CarModel[];

            var parts = context.Parts.Select(part => part.Id).ToList();

            var cars = carsDTO.Select(x => new Car
            {
                Make = x.Make,
                Model = x.Model,
                TravelledDistance = x.TraveledDistance,
                PartCars = x.Parts.Select(x => x.Id).Distinct().Intersect(parts).Select(pc => new PartCar
                {
                    PartId = pc,
                }).ToList()
            });

            context.AddRange(cars);
            context.SaveChanges();

            var result = $"Successfully imported {cars.Count()}";
            return result;

        }
        //Query 2. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(PartModel[]), new XmlRootAttribute("Parts"));
            var partsInput = new StringReader(inputXml);
            var partModels = serializer.Deserialize(partsInput) as PartModel[];

            var suppliers = context.Suppliers.Select(s => s.Id).ToList();
            var parts = partModels.Where(part => suppliers.Any(s => s == part.SupplierId)).Select(x => new Part
            {
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                SupplierId = x.SupplierId,
            })
                .ToArray();

            context.AddRange(parts);
            context.SaveChanges();

            var result = $"Successfully imported {parts.Count()}";
            return result;
        }

        //Query 1. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(SupplierDTO[]), new XmlRootAttribute("Suppliers"));
            var suppliersInput = new StringReader(inputXml);
            var suppliersDTO = serializer.Deserialize(suppliersInput) as SupplierDTO[];

            var suppliers = suppliersDTO.Select(x => new Supplier
            {
                Name = x.Name,
                IsImporter = x.IsImporter,
            })
                .ToArray();

            context.AddRange(suppliers);
            context.SaveChanges();

            var result = $"Successfully imported {suppliers.Count()}";
            return result;
        }
    }
}