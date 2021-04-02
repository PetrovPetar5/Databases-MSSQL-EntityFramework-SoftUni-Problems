namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var departmentsDTO = JsonConvert.DeserializeObject<IEnumerable<DepartmentCellModel>>(jsonString);
            var departments = new List<Department>();
            foreach (var department in departmentsDTO)
            {
                var validation = department.Cells.Count == 0 || !IsValid(department) || !department.Cells.All(x => IsValid(x));
                if (validation)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var currentDepartment = new Department
                {
                    Name = department.Name,
                    Cells = department.Cells.Select(x => new Cell
                    {
                        CellNumber = x.CellNumber,
                        HasWindow = x.HasWindow
                    }).ToList()

                };

                departments.Add(currentDepartment);
                sb.AppendLine($"Imported {currentDepartment.Name} with {currentDepartment.Cells.Count} cells");
            }

            context.AddRange(departments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var prisonersInputDTOs = JsonConvert.DeserializeObject<IEnumerable<PrisonerMailModel>>(jsonString);
            var prisoners = new List<Prisoner>();

            foreach (var prisonerDTO in prisonersInputDTOs)
            {
                if (!IsValid(prisonerDTO) || !prisonerDTO.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                DateTime releaseDate;
                var isValidReleaseDate = DateTime.TryParseExact(prisonerDTO.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);
                var curPrisoner = new Prisoner
                {
                    Age = prisonerDTO.Age,
                    Bail = prisonerDTO.Bail,
                    FullName = prisonerDTO.FullName,
                    Nickname = prisonerDTO.Nickname,
                    IncarcerationDate = DateTime.ParseExact(prisonerDTO.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = isValidReleaseDate ? (DateTime?)releaseDate : null,
                    CellId = prisonerDTO.CellId,
                    Mails = prisonerDTO.Mails.Select(x => new Mail
                    {
                        Address = x.Address,
                        Description = x.Description,
                        Sender = x.Sender,
                    }).ToList()
                };


                prisoners.Add(curPrisoner);
                sb.AppendLine($"Imported {curPrisoner.FullName} {curPrisoner.Age} years old");
            }

            context.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}