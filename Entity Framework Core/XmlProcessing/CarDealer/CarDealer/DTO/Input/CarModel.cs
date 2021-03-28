namespace CarDealer.DTO.Input
{
    using CarDealer.Models;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class CarModel
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
//< Car >
//    < make > Opel </ make >
//    < model > Omega </ model >
//    < TraveledDistance > 176664996 </ TraveledDistance >
//    < parts >
//      < partId id = "38" />

//       < partId id = "102" />

//        < partId id = "23" />

//         < partId id = "116" />

//          < partId id = "46" />

//           < partId id = "68" />

//            < partId id = "88" />

//             < partId id = "104" />

//              < partId id = "71" />

//               < partId id = "32" />

//                < partId id = "114" />

//               </ parts >