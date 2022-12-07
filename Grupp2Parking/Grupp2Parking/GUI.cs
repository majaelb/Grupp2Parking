using Grupp2Parking.Logic;
using Grupp2Parking.ParkingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp2Parking {
    internal class GUI {
        internal static void MainMenu() {
            bool runProgram = true;

            while (runProgram) {
                var key = Console.ReadKey(true).Key;
                PrintOverviewStatus(1);
                PrintMenuOptions();

                switch (key) {

                    //Console.WriteLine("Välj funktion");
                    //Console.WriteLine("====");
                    //Console.WriteLine("[P]arkera");
                    //Console.WriteLine("[A]vparkera");
                    //Console.WriteLine("[L]ägga till");
                    //Console.WriteLine("[V]isa detaljer");
                    //Console.WriteLine("[A]vsluta");
                    case ConsoleKey.P:
                        //Bryt ut till en metod och placera någonstans?
                        ParkingLogic.ParkCar();
                        /*
                        List<ParkingItems.Car> cars = ParkingHelpers.GetAllCars();

                        foreach (ParkingItems.Car c in cars) {
                            Console.WriteLine($"{c.Id}\t\t{c.Plate}\t\t{c.Make}\t\t{c.Color}\t\t{c.ParkingSlotsId}");
                        }
                        Console.WriteLine("----------------------------------------------");
                        */
                        break;
                    case ConsoleKey.A: //Lista bilar med numrerat val?
                                       //Val för söka bil baserat på regskylt?
                                       //Hämta bil från vald metod
                                       //Försök ta bort Bilen Från Databasen
                                       break;
                    case ConsoleKey.L: //Visa Lista med "Saker" att Lägga till
                                       //Välj från listan och anropa rätt metod
                                       break;
                    case ConsoleKey.V: //Visa Lista med alternativ att kolla djupare på
                                       //Välj från listan och anropa rätt metod
                                       break;
                    case ConsoleKey.Q: runProgram = false;
                                       break;
                }

            }
        }



        /**
* Information om alla relevanta data rörande parkering i en stad, 
* i.e. lista med parkeringshus, antal parkerade bilar, antal lediga platser, antalet elplatser+lediga elplatser
* 
*/
        private static void PrintOverviewStatus(int cityNumber) {
            throw new NotImplementedException();
        }

        private static void PrintMenuOptions() {
            Console.WriteLine("Välj funktion");
            Console.WriteLine("====");
            Console.WriteLine("[P]arkera");
            Console.WriteLine("[A]vparkera");
            Console.WriteLine("[L]ägga till (Stad/P-hus/Bil)");
            Console.WriteLine("[V]isa detaljer (Stad/P-hus)");
            Console.WriteLine("[A]vsluta");
            //Console.WriteLine("W: Växla inmatningsläge för fordon (manuellt, automatiskt)");
            //Console.WriteLine("+-: Växla stad");
        }

        /**
         * Skriver ut en numrerad lista med alla parkeringhus i en stad
         */
        internal static void PrintParkingHouses(int CityId) {

        }
        /**
        * Skriver ut en numrerad lista med alla lediga platser i ett parkeringshus
        */
        internal static void PrintAvailableSlots(int ParkinghouseId, bool isElectric) {

        }
        /**
        * Skriver ut en numrerad lista med alla städer
        */
        internal static void PrintCities() {
            // Visa städer
            List<City> cities = ParkingLogic.GetAllCities();

            foreach (City c in cities)
            {
                Console.WriteLine($"{c.Id}\t{c.CityName}");
            }
            Console.WriteLine("----------------------------------------------");

        }
        /**
         * Skriver ut en lista med alla parkerade bilar
         */
        internal static void PrintParkedCars(List<Car> cars) {

            foreach (ParkingItems.Car c in cars)
            {
                Console.WriteLine($"{c.Id}\t{c.Plate}\t{c.Make}\t{c.Color}\t{c.ParkingSlotsId}");
            }
            Console.WriteLine("----------------------------------------------");

        }

        internal static void PrintUnParkedCars(List<Car> cars)
        {
            foreach (ParkingItems.Car c in cars)
            {
                Console.WriteLine($"{c.Id}\t{c.Plate}\t{c.Make}\t{c.Color}");
            }
            Console.WriteLine("----------------------------------------------");
        }

        /**
         * Producerar en numrerad output av en lista
         */
        internal static void PrintNumberedOutput(List<string> items) {

        }
    }
}

