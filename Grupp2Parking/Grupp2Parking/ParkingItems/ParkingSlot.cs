
namespace Grupp2Parking.ParkingItems {
    internal class ParkingSlot {
        public int Id { get; set; }
        public int SlotNumber { get; set; }
        public bool ElectricOutlet { get; set; }
        public int ParkingHouseId { get; set; }

        public string HouseName { get; set; }
        public string CityName { get; set; }
    }
}
