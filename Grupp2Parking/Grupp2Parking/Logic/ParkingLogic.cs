using Grupp2Parking.ParkingItems;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Runtime.ConstrainedExecution;

namespace Grupp2Parking.Logic
{
    internal class ParkingLogic
    {
        static readonly string connString = "data source=.\\SQLEXPRESS; initial catalog=Grupp2Parking; persist security info=True; Integrated Security=True";
        public static bool AddCarToDatabase(Car car)
        {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cars(Plate, Make, Color) VALUES('{car.Plate}', '{car.Make}', '{car.Color}')";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows > 0;
        }
        public static bool AddCityToDatabase(City city)
        {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cities(CityName) VALUES('{city.CityName}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows > 0;
        }
        public static bool AddParkinghouseToDatabase(ParkingHouse house)
        {
            int affectedRows = 0;

            string sql = $"INSERT INTO ParkingHouses(HouseName, CityId) VALUES('{house.HouseName}', '{house.CityId}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows > 0;
        }

        public static void InsertCar()
        {
            Console.WriteLine("Ange registreringsnummer(ABC123), märke och färg");
            var newCar = new Car
            {
                Plate = Console.ReadLine().ToUpper(),
                Make = Console.ReadLine(),
                Color = Console.ReadLine()
            };
            bool success = AddCarToDatabase(newCar);
            Console.WriteLine("Antal bilar tillagda: " + success);
            Console.WriteLine("----------------------------------------------");
        }

        public static void InsertCity()
        {
            // Lägg till ny stad
            Console.WriteLine("Ange stad att lägga till");
            var newCity = new City
            {
                CityName = Console.ReadLine()
            };
            bool success = AddCityToDatabase(newCity);
            Console.WriteLine("Antal städer tillagda: " + success);
            Console.WriteLine("----------------------------------------------");
        }

        public static List<City> GetAllCities()
        {
            var sql = "SELECT * FROM Cities";
            var cities = new List<City>();

            using (var connection = new SqlConnection(connString))
            {
                cities = connection.Query<City>(sql).ToList();
            }
            return cities;

        }


        public static List<ParkingSlot> GetAllFreeSlots()
        {
            var sql = @"SELECT
	                c.CityName,
	                ph.HouseName,
	                ps.Id AS Slot,
	                ps.Id AS SlotId
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

            using (var connection = new SqlConnection(connString))
            {
                parkingSlots = connection.Query<ParkingSlot>(sql).ToList();
            }
            return parkingSlots;
        }

        public static List<Car> GetAllCars()
        {
            var sql = "SELECT * FROM Cars";
            var cars = new List<Car>();

            using (var connection = new SqlConnection(connString))
            {
                cars = connection.Query<Car>(sql).ToList();
            }
            return cars;
        }


        public static bool ParkCarAtGarage(Car car)
        {
            int affectedRow = 0;

            string sql = $"UPDATE Cars SET ParkingSlotsId = {car.ParkingSlotsId} WHERE Id = {car.Id}";

            using (var connection = new SqlConnection(connString))
            {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow > 0;
        }

        internal static void ParkCar()
        {
            var cars = GetAllCars();
            GUI.PrintUnParkedCars(cars);
            var slots = GetAllFreeSlots();
            GUI.PrintAvailableSlots()

            var car = ParkingLogic.GetCar();
            var slot = ParkingLogic.GetParkingSlot();
            //var city = ParkingLogic.GetCity();
            //var house = ParkingLogic.GetParkingHouse(city);
            car.ParkingSlotsId = slot.Id;
            if (!ParkingLogic.ParkCarAtGarage(car))
            {
                //Felmeddelande
            }
        }

        internal static Car GetCar()
        {
            throw new NotImplementedException();
        }

        internal static City GetCity()
        {
            throw new NotImplementedException();
        }

        internal static ParkingHouse GetParkingHouse(City city)
        {
            throw new NotImplementedException();
        }

        internal static ParkingSlot GetParkingSlot()
        {
            throw new NotImplementedException();
        }
    }
}
