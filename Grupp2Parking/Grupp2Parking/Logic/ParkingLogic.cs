using Grupp2Parking.ParkingItems;
using System.Data.SqlClient;
using Dapper;
using Grupp2Parking.UserInterface;

namespace Grupp2Parking.Logic {
    internal class ParkingLogic {
        static readonly string connString = "data source=.\\SQLEXPRESS; initial catalog=Grupp2Parking; persist security info=True; Integrated Security=True";
        public static bool AddCarToDatabase(Car car) {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cars(Plate, Make, Color) VALUES('{car.Plate}', '{car.Make}', '{car.Color}')";
            using (var connection = new SqlConnection(connString)) {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows > 0;
        }
        public static bool AddCityToDatabase(City city) {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cities(CityName) VALUES('{city.CityName}')";

            using (var connection = new SqlConnection(connString)) {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows > 0;
        }
        public static bool AddParkinghouseToDatabase(ParkingHouse house) {
            int affectedRows = 0;

            string sql = $"INSERT INTO ParkingHouses(HouseName, CityId) VALUES('{house.HouseName}', '{house.CityId}')";

            using (var connection = new SqlConnection(connString)) {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows > 0;
        }
        public static bool AddParkingSlotsToParkingHouse(int slotCount, int electricCount, int houseId) {
            int affectedRows = 0;
            string sql;
            for (int i = 0; i < slotCount; i++) {
                sql = $"INSERT INTO ParkingSlots(Slotnumber, ElectricOutlet, " +
                    $"ParkingHouseId) VALUES('{i + 1}', '{(i < electricCount)}', '{houseId}')";
                //$"ParkingHouseId) VALUES({i + 1}, {(slotCount < electricCount ? 1 : 0)}, {houseId})";

                using (var connection = new SqlConnection(connString)) {
                    affectedRows = connection.Execute(sql);
                }
            }
            return affectedRows > 0;
        }

        public static void InsertCar() {
            Console.WriteLine("Ange registreringsnummer(ABC123)");
            var plate = InputModule.GetValidatedString();
            if (IsPlateInDatabase(plate)) {
                Console.WriteLine("Registreringsnumret är redan upptaget!");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Registreringsnummer accepterat");
            Console.WriteLine("Ange Bilens tillverkare");
            var make = InputModule.GetString();
            Console.WriteLine("Ange bilens färg");
            var color = InputModule.GetString();
            var newCar = new Car {
                Plate = plate,
                Make = make,
                Color = color,
            };
            bool success = AddCarToDatabase(newCar);
            Console.WriteLine($"{(success? "Bilen med regnr "+ plate +" blev tillagd": "Kunde inte parkera bilen" )}");
            Console.WriteLine("----------------------------------------------");
        }

        public static void InsertCity() {
            // Lägg till ny stad
            Console.WriteLine("Ange stad att lägga till");
            var newCity = new City {
                CityName = InputModule.GetString()
            };
            bool success = AddCityToDatabase(newCity);
            Console.WriteLine("Antal städer tillagda: " + success);
            Console.WriteLine("----------------------------------------------");
        }

        public static int InsertParkingHouse() {
            // Lägg till ny stad
            GUI.PrintCities();
            Console.WriteLine("Välj stads-Id för nytt parkeringshus: ");
            var cities = GetAllCities();
            List<int> validIds = new();
            foreach (City city in cities) {
                validIds.Add(city.Id);
            }
            var CityId = InputModule.GetValidatedInt(validIds);
            validIds.Clear();
            Console.Clear();
            Console.WriteLine("Ange namnet på det nya p-huset");
            var newParkingHouse = new ParkingHouse {
                HouseName = Console.ReadLine(),
                CityId = CityId
            };
            bool success = AddParkinghouseToDatabase(newParkingHouse);
            Console.WriteLine("Antal parkeringshus tillagda: " + success);
            Console.WriteLine("----------------------------------------------");
            return success ? ParkingLogic.FindParkingHouseId(newParkingHouse) : -1;
        }

        private static int FindParkingHouseId(ParkingHouse newParkingHouse) {
            var sql = $"SELECT Id FROM ParkingHouses WHERE houseName = '{newParkingHouse.HouseName}'";
            var parkingHouses = new List<ParkingHouse>();

            using (var connection = new SqlConnection(connString)) {
                parkingHouses = connection.Query<ParkingHouse>(sql).ToList();
            }
            return parkingHouses[0].Id;
        }

        public static List<City> GetAllCities() {
            var sql = "SELECT * FROM Cities";
            var cities = new List<City>();

            using (var connection = new SqlConnection(connString)) {
                cities = connection.Query<City>(sql).ToList();
            }
            return cities;

        }


        public static List<ParkingSlot> GetAllFreeSlots() {
            var sql = @"SELECT
	                c.CityName,
	                ph.HouseName,
	                ps.Id,
                    ps.ElectricOutlet
                FROM 
	                Cities c
                JOIN
	                ParkingHouses ph ON ph.CityId = c.Id
                JOIN
	                ParkingSlots ps ON ph.Id = ps.ParkingHouseId
                LEFT JOIN
	                Cars car ON ps.Id = car.ParkingSlotsId
                WHERE 
	                car.ParkingSlotsId IS NULL";
            var parkingSlots = new List<ParkingSlot>();

            using (var connection = new SqlConnection(connString)) {
                parkingSlots = connection.Query<ParkingSlot>(sql).ToList();
            }
            return parkingSlots;
        }

        public static List<ParkingHouse> GetAllParkingHouses(bool detailedView = true)
        {
            var sql = @$"SELECT Cities.CityName AS [Stad]
                      {(detailedView ? ",ParkingHouses.HouseName AS [Parkeringshus]" : null) }
                      ,SUM(CASE WHEN ElectricOutlet = 'true' THEN 1 ELSE 0 END) AS [Elplatser]
                      ,SUM(CASE WHEN ParkingSlots.Id = Cars.ParkingSlotsId THEN 1 ELSE 0 END) AS [UpptagnaPlatser]
                      ,COUNT(*) - SUM(CASE WHEN ParkingSlots.Id = Cars.ParkingSlotsId THEN 1 ELSE 0 END) AS [LedigaPlatser]
                    FROM 
                        ParkingHouses
                    JOIN
                        Cities ON ParkingHouses.CityId = Cities.Id
                    JOIN
                        ParkingSlots ON ParkingSlots.ParkingHouseId = ParkingHouses.Id
                    LEFT JOIN
                    Cars ON ParkingSlots.Id = cars.ParkingSlotsId
                    GROUP BY Cities.CityName {(detailedView ? ", ParkingHouses.HouseName" : null)}
                    ORDER BY Cities.CityName {(detailedView ? ", ParkingHouses.HouseName" : null)}";
            var parkingHouses = new List<ParkingHouse>();

            using (var connection = new SqlConnection(connString))
            {
                parkingHouses = connection.Query<ParkingHouse>(sql).ToList();
            }
            return parkingHouses;
        }

        public static List<Car> GetAllCars(string condition) {
            var sql = $"SELECT * FROM Cars {condition}";
            var cars = new List<Car>();

            using (var connection = new SqlConnection(connString)) {
                cars = connection.Query<Car>(sql).ToList();
            }
            return cars;
        }

        public static bool IsPlateInDatabase(string plate) {
            var sql = $"SELECT * FROM Cars WHERE Cars.Plate = '{plate}'";
            var cars = new List<Car>();

            using (var connection = new SqlConnection(connString)) {
                cars = connection.Query<Car>(sql).ToList();
            }
            return (cars.Count>0);
        }


        public static bool ParkCarAtSlot(int carId, int? slotId) {
            int affectedRow = 0;

            string sql = $"UPDATE Cars SET ParkingSlotsId = {(slotId == null ? "NULL" : slotId)} WHERE Id = {carId}";

            using (var connection = new SqlConnection(connString)) {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow > 0;
        }

        internal static void ParkCar() {
            Console.WriteLine("Välj bil-ID");
            var cars = GetAllCars("WHERE parkingslotsid is null");
            GUI.PrintUnParkedCars(cars);
            List<int> validIds = new();
            foreach (Car car in cars) {
                validIds.Add(car.Id);
            }
            var carId = InputModule.GetValidatedInt(validIds);
            validIds.Clear();
            Console.Clear();
            Console.WriteLine("Välj parkeringsplats-ID");
            var slots = GetAllFreeSlots();
            GUI.PrintAvailableSlots(slots);
            foreach (ParkingSlot slot in slots) {
                validIds.Add(slot.Id);
            }
            var slotId = InputModule.GetValidatedInt(validIds);
            Console.Clear();
            //var city = ParkingLogic.GetCity();
            //var house = ParkingLogic.GetParkingHouse(city);
            if (ParkingLogic.ParkCarAtSlot(carId, slotId)) {
                Console.WriteLine("Du har parkerat bil " + carId + " på plats " + slotId);
            } else {
                Console.WriteLine("Kunde inte parkera bil " + carId + " på plats " + slotId);
            }
        }

        internal static void UnParkCar() {
            Console.WriteLine("Välj bil-ID");
            var cars = GetAllCars("WHERE parkingslotsid IS NOT NULL");
            GUI.PrintParkedCars(cars);
            List<int> validIds = new();
            foreach (Car car in cars) {
                validIds.Add(car.Id);
            }
            var carId = InputModule.GetValidatedInt(validIds);
            validIds.Clear();
            Console.Clear();
            if (ParkingLogic.ParkCarAtSlot(carId, null)) {
                Console.WriteLine("Du har checkat ut bil " + carId);
            } else {
                Console.WriteLine("Kunde inte checka ut bil " + carId);
            }
        }

        internal static void InsertParkingSlotsToParkingHouse(int houseId) {
            Console.WriteLine("Antal platser att lägga till: (1-50)");
            var slotCount = InputModule.GetIntInRange(1, 50);
            Console.WriteLine($"Varav platser med elbilsladdning (<{slotCount})");
            var electricCount = InputModule.GetIntInRange(1, slotCount);
            AddParkingSlotsToParkingHouse(slotCount, electricCount, houseId);
        }
    }
}
