using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Grupp2Parking.Logic;

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
                Console.WriteLine("Tryck B för att lägga till en ny bil");
                Console.WriteLine("Tryck S för att lägga till en ny stad");
                Console.WriteLine("Tryck V för att visa alla städer");
                Console.WriteLine("Tryck A för att avsluta");

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'b':
                        // Lägg till ny bil
                        Console.WriteLine("Ange registreringsnummer(ABC123), märke och färg");
                        var newCar = new ParkingItems.Car
                        {
                            Plate = Console.ReadLine().ToUpper(),
                            Make = Console.ReadLine(),
                            Color = Console.ReadLine()
                        };
                        int rowsAffected1 = InsertCar(newCar);
                        Console.WriteLine("Antal bilar tillagda: " + rowsAffected1);
                        Console.WriteLine("----------------------------------------------");
                        break;
                    case 's':
               
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

        public static void InsertCity()
        {
            // Lägg till ny stad
            Console.WriteLine("Ange stad att lägga till");
            var newCity = new ParkingItems.City
            {
                CityName = Console.ReadLine()
            };
            bool success = ParkingLogic.AddCityToDatabase(newCity);
            Console.WriteLine("Antal städer tillagda: " + success);
            Console.WriteLine("----------------------------------------------");
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
        public static List<ParkingItems.Car> GetAllCars() {
            var sql = "SELECT * FROM Cars";
            var cars = new List<ParkingItems.Car>();

            using (var connection = new SqlConnection(connString)) //Anslutning - Vi har bara tillgång till connection här inne:
            {
                cars = connection.Query<ParkingItems.Car>(sql).ToList();
            }
            return cars;
        }
        public static int ParkCar(int carId, int spotId) {
            int affectedRow = 0;

            string sql = $"UPDATE Cars SET ParkingSlotsId = {spotId} WHERE Id = {carId}";

            using (var connection = new SqlConnection(connString)) {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow;
        }
    }
}
