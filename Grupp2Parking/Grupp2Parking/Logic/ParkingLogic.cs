using Grupp2Parking.ParkingItems;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Grupp2Parking.Logic {
    internal class ParkingLogic {
        static readonly string connString = "data source=.\\SQLEXPRESS; initial catalog=Grupp2Parking; persist security info=True; Integrated Security=True";
        public static bool AddCarToDatabase(ParkingItems.Car car) {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cars(Plate, Make, Color) VALUES('{car.Plate}', '{car.Make}', '{car.Color}')";
            using (var connection = new SqlConnection(connString)) {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows>0;
        }
        public static bool AddCityToDatabase(ParkingItems.City city) {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cities(CityName) VALUES('{city.CityName}')";

            using (var connection = new SqlConnection(connString)) {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows>0;
        }
        public static bool AddParkinghouseToDatabase(ParkingItems.ParkingHouse house) {
            int affectedRows = 0;

            string sql = $"INSERT INTO ParkingHouses(HouseName, CityId) VALUES('{house.HouseName}', '{house.CityId}')";

            using (var connection = new SqlConnection(connString)) {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows > 0;
        }
        public static bool ParkCarAtGarage(ParkingItems.Car car) {
            int affectedRow = 0;

            string sql = $"UPDATE Cars SET ParkingSlotsId = {car.ParkingSlotsId} WHERE Id = {car.Id}";

            using (var connection = new SqlConnection(connString)) {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow>0;
        }

        internal static Car GetCar() {
            throw new NotImplementedException();
        }

        internal static City GetCity() {
            throw new NotImplementedException();
        }

        internal static ParkingHouse GetParkingHouse(City city) {
            throw new NotImplementedException();
        }

        internal static ParkingSlot GetParkingSlot(ParkingHouse house) {
            throw new NotImplementedException();
        }
    }
}
