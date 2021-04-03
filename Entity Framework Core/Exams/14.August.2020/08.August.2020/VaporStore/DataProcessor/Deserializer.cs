namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var games = JsonConvert.DeserializeObject<IEnumerable<JsonGameModel>>(jsonString);

            foreach (var jsonGame in games)
            {
                if (!IsValid(jsonGame) || jsonGame.Tags.Count() == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var curGenre = context.Genres.FirstOrDefault(x => x.Name == jsonGame.Genre) ?? new Genre() { Name = jsonGame.Genre };
                var curDeveloper = context.Developers.FirstOrDefault(x => x.Name == jsonGame.Developer) ?? new Developer() { Name = jsonGame.Developer };
                var tags = new List<string>();

                var game = new Game
                {
                    Developer = curDeveloper,
                    Genre = curGenre,
                    Name = jsonGame.Name,
                    Price = jsonGame.Price,
                    ReleaseDate = jsonGame.ReleaseDate.Value,
                };

                foreach (var jsonTag in jsonGame.Tags)
                {
                    var curTag = context.Tags.FirstOrDefault(x => x.Name == jsonTag) ?? new Tag() { Name = jsonTag };
                    game.GameTags.Add(new GameTag { Tag = curTag });
                }

                context.Games.Add(game);
                context.SaveChanges();
                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var jSONUsers = JsonConvert.DeserializeObject<IEnumerable<JsonUserImportModel>>(jsonString);
            var users = new List<User>();

            foreach (var jSONUser in jSONUsers)
            {
                if (!IsValid(jSONUser) || !jSONUser.Cards.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var curUser = new User
                {
                    FullName = jSONUser.FullName,
                    Username = jSONUser.Username,
                    Email = jSONUser.Email,
                    Age = jSONUser.Age,
                    Cards = jSONUser.Cards.Select(x => new Card
                    {
                        Cvc = x.CVC,
                        Number = x.Number,
                        Type = x.Type.Value,
                    })
                    .ToArray()
                };

                users.Add(curUser);
                sb.AppendLine($"Imported {curUser.Username} with {curUser.Cards.Count()} cards");
            }

            context.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(XmlPurchaseModel[]), new XmlRootAttribute("Purchases"));
            var xMLPurchases = serializer.Deserialize(reader) as XmlPurchaseModel[];

            foreach (var xMLPurchase in xMLPurchases)
            {
                if (!IsValid(xMLPurchase))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var result = DateTime.TryParseExact(xMLPurchase.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
                if (!result)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var curPurchase = new Purchase
                {
                    Date = date,
                    ProductKey = xMLPurchase.ProductKey,
                    Card = context.Cards.FirstOrDefault(x => x.Number == xMLPurchase.Card),
                    Game = context.Games.FirstOrDefault(x => x.Name == xMLPurchase.Title),
                    Type = xMLPurchase.Type.Value,
                };

                context.Purchases.Add(curPurchase);
                context.SaveChanges();
                sb.AppendLine($"Imported {curPurchase.Game.Name} for {curPurchase.Card.User.Username}");
            }

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}