﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp2Parking.ParkingItems {
    internal class ParkingSlot {
        public int Id { get; set; }
        public int SlotNumber { get; set; }
        public bool ElectricOutlet { get; set; }
        public int ParkingHouseId { get; set; }
    }
}
