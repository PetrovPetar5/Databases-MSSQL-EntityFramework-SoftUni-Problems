namespace SoftJail.DataProcessor.ImportDto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class DepartmentCellModel
    {
        [Required]
        [StringLength(25), MinLength(3)]
        public string Name { get; set; }

        public ICollection<CellDTO> Cells { get; set; }
    }

    public class CellDTO
    {
        [Range(1, 1000)]
        public int CellNumber { get; set; }
        public bool HasWindow { get; set; }
    }
}
