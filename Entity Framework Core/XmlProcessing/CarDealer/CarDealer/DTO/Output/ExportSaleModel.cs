namespace CarDealer.DTO.Output
{
    using System.Xml.Serialization;

    [XmlType("sale")]
    public class ExportSaleModel
    {
        [XmlElement("car")]
        public ExportSaleCarModel Car { get; set; }

        [XmlElement("discount")]

        public decimal Discount { get; set; }

        [XmlElement("customer-name")]

        public string Name { get; set; }

        [XmlElement("price")]

        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]

        public decimal PriceWithDiscount { get; set; }
    }
}
