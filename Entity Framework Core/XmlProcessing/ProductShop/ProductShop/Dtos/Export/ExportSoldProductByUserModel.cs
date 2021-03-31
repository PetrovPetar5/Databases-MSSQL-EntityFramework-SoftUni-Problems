namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("SoldProducts")]
    public class ExportSoldProductByUserModel
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public ExportProductUserModel[] Products { get; set; }
    }
}
