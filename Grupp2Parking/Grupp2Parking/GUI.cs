using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp2Parking
{
    internal class GUI
    {
        internal static void MainMenu()
        {
            bool runProgram = true;

            while (runProgram)
            {
                var key = Console.ReadKey(true).Key;
                PrintMenuOptions();

                switch (key)
                {
                    case ConsoleKey.L:
                        // Lägg till ny bil
                        
                        break;

                    case ConsoleKey.P:
                        List<ParkingItems.Car> cars = ParkingHelpers.GetAllCars();

                        foreach (ParkingItems.Car c in cars)
                        {
                            Console.WriteLine($"{c.Id}\t\t{c.Plate}\t\t{c.Make}\t\t{c.Color}\t\t{c.ParkingSlotsId}");
                        }
                        Console.WriteLine("----------------------------------------------");
                        break;
                }
            
            }
        }

        private static void PrintMenuOptions()
        {
            Console.WriteLine("Välj funktion");
            Console.WriteLine("====");
            Console.WriteLine("[P]arkera");
            Console.WriteLine("[A]vparkera");
            Console.WriteLine("[L]ägga till");
            Console.WriteLine("[V]isa detaljer");
            Console.WriteLine("[A]vsluta");
          //Console.WriteLine("A: Växla inmatningsläge för fordon (manuellt, automatiskt)");
        }

        internal static void PrintParkingHouses(int CityId) {

        }
        internal static void PrintAvailableSlots() {

        }
        internal static void PrintCities() {

        }
        internal static void PrintParkedCars() {

        }
    }
}

