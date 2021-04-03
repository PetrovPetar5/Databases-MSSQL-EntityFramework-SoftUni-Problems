using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class JsonUserImportModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,} [A-Z]{1}[a-z]{2,}$")]
        public string FullName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(typeof(int), "3", "103")]
        public int Age { get; set; }
        public ICollection<CardDTO> Cards { get; set; }
    }

    public class CardDTO
    {
        [Required]
        [RegularExpression("^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}$")]
        public string CVC { get; set; }

        [Required]
        public CardType? Type { get; set; }
    }
}
