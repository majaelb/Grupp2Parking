using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Grupp2Parking
{
    internal class ParkingHelpers
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog=Grupp2Parking; persist security info=True; Integrated Security=True";


        public static void ParkingMenu()
        {
            bool runprogram = true;
            while (runprogram)
            {
                Console.WriteLine("Tryck B för bilalternativ");
                Console.WriteLine("Tryck S för stadalternativ");
                Console.WriteLine("Tryck P för parkeringshusalternativ");
                Console.WriteLine("Tryck A för att avsluta");

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'b':
                        GUI.CarMenu();
                        break;
                    case 's':
                        // Lägg till ny stad
                        Console.WriteLine("Ange stad att lägga till");
                        var newCity = new ParkingItems.City
                        {
                            CityName = Console.ReadLine()
                        };
                        int rowsAffected2 = InsertCity(newCity);
                        Console.WriteLine("Antal städer tillagda: " + rowsAffected2);
                        Console.WriteLine("----------------------------------------------");
                        break;
                    case 'v':
                        // Visa städer
                        List<ParkingItems.City> cities = GetAllCities();

                        foreach (ParkingItems.City c in cities)
                        {
                            Console.WriteLine($"{c.Id}\t{c.CityName}");
                        }
                        Console.WriteLine("----------------------------------------------");
                        break;
                    case 'a':
                        runprogram = false;
                        break;
                }
            }
        }


        public static int InsertCar(ParkingItems.Car car)
        {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cars(Plate, Make, Color) VALUES('{car.Plate}', '{car.Make}', '{car.Color}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows;
        }

        public static int InsertCity(ParkingItems.City city)
        {
            int affectedRows = 0;

            string sql = $"INSERT INTO Cities(CityName) VALUES('{city.CityName}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }

            return affectedRows;
        }

        public static List<ParkingItems.City> GetAllCities()
        {
            var sql = "SELECT * FROM Cities";
            var cities = new List<ParkingItems.City>();

            using (var connection = new SqlConnection(connString)) //Anslutning - Vi har bara tillgång till connection här inne:
            {
                cities = connection.Query<ParkingItems.City>(sql).ToList();
            }
            return cities;

        }
        public static List<ParkingItems.Car> GetAllCars()
        {
            var sql = "SELECT * FROM Cars";
            var cars = new List<ParkingItems.Car>();

            using (var connection = new SqlConnection(connString)) //Anslutning - Vi har bara tillgång till connection här inne:
            {
                cars = connection.Query<ParkingItems.Car>(sql).ToList();
            }
            return cars;
        }
    }
}
