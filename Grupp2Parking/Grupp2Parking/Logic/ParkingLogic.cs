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
    }
}
