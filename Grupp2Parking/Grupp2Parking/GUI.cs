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
                        var car = ParkingLogic.GetCar();
                        var city = ParkingLogic.GetCity();
                        var house = ParkingLogic.GetParkingHouse(city);
                        var slot = ParkingLogic.GetParkingSlot(house);
                        car.ParkingSlotsId = slot.Id;
                        if (!ParkingLogic.ParkCarAtGarage(car)) {
                            //Felmeddelande
                        }
                        /*
                        List<ParkingItems.Car> cars = ParkingHelpers.GetAllCars();

                        foreach (ParkingItems.Car c in cars) {
                            Console.WriteLine($"{c.Id}\t\t{c.Plate}\t\t{c.Make}\t\t{c.Color}\t\t{c.ParkingSlotsId}");
                        }
                        Console.WriteLine("----------------------------------------------");
                        */
                        break;
                    case ConsoleKey.A:
                                       //Lista bilar med numrerat val?
                                       //Val för söka bil baserat på regskylt?
                                       //Hämta bil från vald metod
                                       //Försök ta bort Bilen Från Databasen
                                       break;
                    case ConsoleKey.L: //Visa Lista med "Saker" att Lägga till
                                       //Välj från listan och anropa rätt metod
                                       break;
                    case ConsoleKey.V: 
                                       break;
                    case ConsoleKey.Q: 
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
            //Console.WriteLine("A: Växla inmatningsläge för fordon (manuellt, automatiskt)");
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

        }
        /**
         * Skriver ut en lista med alla parkerade bilar
         */
        internal static void PrintParkedCars() {

        }
        /**
         * Producerar en numrerad output av en lista
         */
        internal static void PrintNumberedOutput(List<string> items) {

        }
    }
}

