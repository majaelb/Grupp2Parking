using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp2Parking.ParkingItems {
    internal class ParkingHouse {
        public int Id { get; set; }
        public string? HouseName { get; set; }
        public int CityId { get; set; }
        public string Parkeringshus { get; set; }
        public string Stad { get; set; }
        public int Elplatser { get; set; }
        public int UpptagnaPlatser { get; set; }
        public int LedigaPlatser { get; set; }
    }
}
