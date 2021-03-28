namespace CarDealer.DTO.Output
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarWithPartsOutputModel
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public PartOutputModel[] Parts { get; set; }
    }
}
