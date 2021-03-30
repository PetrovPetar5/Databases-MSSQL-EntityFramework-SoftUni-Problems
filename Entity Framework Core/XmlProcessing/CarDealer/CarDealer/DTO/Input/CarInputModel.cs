namespace CarDealer.DTO.Input
{
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class CarInputModel
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        public int TraveledDistance { get; set; }

        [XmlArray("parts")]
        public CarPartInputModel[] Parts { get; set; }
    }
}
