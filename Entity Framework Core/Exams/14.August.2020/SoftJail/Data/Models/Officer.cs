using SoftJail.Data.Models.Enums;
using System;
using System.Collections.Generic;
namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Officer
    {
        public Officer()
        {
            this.OfficerPrisoners = new HashSet<OfficerPrisoner>();
        }
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
        public decimal Salary { get; set; }

        [Required]
        public Position Position { get; set; }

        [Required]
        public Weapon Weapon { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<OfficerPrisoner> OfficerPrisoners { get; set; }
    }
}

