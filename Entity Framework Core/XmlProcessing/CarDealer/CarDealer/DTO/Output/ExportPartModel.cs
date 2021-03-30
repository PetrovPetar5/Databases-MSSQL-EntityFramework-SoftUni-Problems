namespace CarDealer.DTO.Output
{
    using System.Xml.Serialization;

    [XmlType("part")]
    public class ExportPartModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
