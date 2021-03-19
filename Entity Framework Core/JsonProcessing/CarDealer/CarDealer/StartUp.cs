﻿namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.DTO;
    using CarDealer.Models;
    using Newtonsoft.Json;

    public class StartUp
    {
        static IMapper mapper;
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var suppliersInput = File.ReadAllText(@"..\..\..\Datasets\suppliers.json");
            //var partsInput = File.ReadAllText(@"..\..\..\Datasets\parts.json");
            //var carsInput = File.ReadAllText(@"..\..\..\Datasets\cars.json");
            //var customersInput = File.ReadAllText(@"..\..\..\Datasets\customers.json");
            //var salesInput = File.ReadAllText(@"..\..\..\Datasets\sales.json");



            //ImportSuppliers(context, suppliersInput);
            //ImportParts(context, partsInput);
            //ImportCars(context, carsInput);
            //ImportCustomers(context, customersInput);
            //ImportSales(context, salesInput);

            Console.WriteLine(GetLocalSuppliers(context));

        }

        //Query 8. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {

        }

        //Query 7. Export Cars from Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var carsToyota = context.Cars.Where(c => c.Make == "Toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(car => new
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance,
                })
                .ToArray();

            var result = JsonConvert.SerializeObject(carsToyota, Formatting.Indented);
            return result;
        }
        //Query 6. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var drivers = context
                .Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(x => new CustomerDTO
                {
                    BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
                    Name = x.Name,
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToArray();


            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var result = JsonConvert.SerializeObject(drivers, Formatting.Indented, settings);
            return result;
        }

        //Query 5. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            InitiliazeMapper();
            var salesDTO = JsonConvert.DeserializeObject<IEnumerable<SaleDTO>>(inputJson);
            var sales = mapper.Map<IEnumerable<Sale>>(salesDTO);

            context.AddRange(sales);
            context.SaveChanges();

            var result = $"Successfully imported {sales.Count()}.";

            return result;
        }
        //Query 4. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            InitiliazeMapper();
            var customersDTO = JsonConvert.DeserializeObject<IEnumerable<CustomerDTO>>(inputJson);
            var customers = mapper.Map<IEnumerable<Customer>>(customersDTO);

            context.AddRange(customers);
            context.SaveChanges();

            var result = $"Successfully imported {customers.Count()}.";

            return result;
        }

        //Query 3. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carInfo = JsonConvert.DeserializeObject<IEnumerable<CarDTO>>(inputJson);
            var cars = new List<Car>();
            foreach (var car in carInfo)
            {
                var currentCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance,
                };

                foreach (var partId in car.PartsId.Distinct())
                {
                    currentCar.PartCars.Add(new PartCar
                    {
                        PartId = partId,
                    });
                }

                cars.Add(currentCar);
            }

            context.AddRange(cars);
            context.SaveChanges();

            var result = $"Successfully imported {cars.Count}.";
            return result;
        }
        //Query 2. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            InitiliazeMapper();
            var supliersDTO = context.Suppliers.Select(x => x.Id).ToList();
            var partsDTO = JsonConvert.DeserializeObject<IEnumerable<PartDTO>>(inputJson);
            var parts = mapper.Map<IEnumerable<Part>>(partsDTO).Where(x => supliersDTO.Any(s => s == x.SupplierId)).ToArray();

            context.AddRange(parts);
            context.SaveChanges();

            var result = $"Successfully imported {parts.Count()}.";

            return result;
        }
        //Query 1. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            InitiliazeMapper();
            var supliersDTO = JsonConvert.DeserializeObject<IEnumerable<Supplier>>(inputJson);
            var supliers = mapper.Map<IEnumerable<Supplier>>(supliersDTO);

            context.AddRange(supliers);
            context.SaveChanges();

            var result = $"Successfully imported {supliers.Count()}.";

            return result;
        }
        public static void InitiliazeMapper()
        {
            var MappingConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarDealerProfile());
            });

            mapper = MappingConfiguration.CreateMapper();
        }
    }
}