using Grupp2Parking.Logic;
using Grupp2Parking.ParkingItems;

namespace Grupp2Parking.UserInterface
{
    internal class GUI
    {
        internal static void MainMenu()
        {
            bool runProgram = true;

            while (runProgram)
            {
                Console.Clear();
                PrintParkingHouses(false);
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                PrintMenuOptions();
                var key = Console.ReadKey(true).Key;

                switch (key)
                {

                    case ConsoleKey.P:
                        Console.Clear();
                        Console.WriteLine("Aktivt val:");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("[P]arkera");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        ParkingLogic.ParkCar();
                        /*
                        List<ParkingItems.Car> cars = ParkingHelpers.GetAllCars();

                        foreach (ParkingItems.Car c in cars) {
                            Console.WriteLine($"{c.Id}\t\t{c.Plate}\t\t{c.Make}\t\t{c.Color}\t\t{c.ParkingSlotsId}");
                        }
                        Console.WriteLine("----------------------------------------------");
                        */
                        break;
                    case ConsoleKey.C: //Lista bilar med numrerat val?
                        Console.Clear();
                        Console.WriteLine("Aktivt val:");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("[C]hecka ut bil");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        ParkingLogic.UnParkCar();
                        //Val för söka bil baserat på regskylt?
                        //Hämta bil från vald metod
                        //Försök ta bort Bilen Från Databasen
                        break;
                    case ConsoleKey.L:
                        Console.Clear();
                        Console.WriteLine("Aktivt val:");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("[L]ägga till (Stad/P-hus/Bil)");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        GUI.AddMenu();
                        //Visa Lista med "Saker" att Lägga till
                        //Välj från listan och anropa rätt metod
                        break;
                    case ConsoleKey.V:
                        Console.Clear();
                        Console.WriteLine("Aktivt val:");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("[V]isa detaljer (Stad/P-hus/Parkerade bilar)");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        GUI.ShowMenu();
                        //Visa Lista med alternativ att kolla djupare på
                        //Välj från listan och anropa rätt metod
                        break;
                    case ConsoleKey.A:
                        //runProgram = false;
                        return;
                        //break;
                }
                //Thread.Sleep(1500);
                //Console.ReadKey();

            }
        }

        internal static void AddMenu()
        {
            Console.WriteLine("Välj sak att lägga till");
            Console.WriteLine("====");
            Console.WriteLine("[B]il");
            Console.WriteLine("[S]tad");
            Console.WriteLine("[P]arkeringshus");
            Console.WriteLine("[T]illbaka");

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.B:
                    ParkingLogic.InsertCar();
                    break;
                case ConsoleKey.S:
                    ParkingLogic.InsertCity();
                    break;
                case ConsoleKey.P:
                    int houseId = ParkingLogic.InsertParkingHouse();
                    if (houseId > 0)
                    {
                        ParkingLogic.InsertParkingSlotsToParkingHouse(houseId);
                    }
                    break;
                case ConsoleKey.T:
                    return;

            }
        }

        internal static void ShowMenu()
        {
            Console.WriteLine("Välj vy att visa:");
            Console.WriteLine("====");
            Console.WriteLine("[S]täder");
            Console.WriteLine("[P]arkeringshus");            
            Console.WriteLine("[L]ista parkerade bilar");
            Console.WriteLine("[T]illbaka");

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.S:
                    GUI.PrintCities();
                    Console.ReadKey();             
                    break;
                case ConsoleKey.P:
                    GUI.PrintParkingHouses();
                    Console.ReadKey();
                    break;
                case ConsoleKey.L:
                    var cars = ParkingLogic.GetAllCars("WHERE parkingslotsid IS NOT NULL");
                    GUI.PrintParkedCars(cars);
                    Console.ReadKey();
                    break;
                case ConsoleKey.T:
                    break;

            }
        }


        private static void PrintMenuOptions()
        {
            Console.WriteLine("Välj funktion");
            Console.WriteLine("====");
            Console.WriteLine("[P]arkera");
            Console.WriteLine("[C]hecka ut bil");
            Console.WriteLine("[L]ägga till (Stad/P-hus/Bil)");
            Console.WriteLine("[V]isa detaljer (Stad/P-hus)/Parkerade bilar");
            Console.WriteLine("[A]vsluta");
            //Console.WriteLine("W: Växla inmatningsläge för fordon (manuellt, automatiskt)");
            //Console.WriteLine("+-: Växla stad");
        }

        /**
         * Skriver ut en numrerad lista med alla parkeringhus i en stad
         */
        internal static void PrintParkingHouses(bool detailedView = true)
        {
            List<ParkingHouse> houses = ParkingLogic.GetAllParkingHouses(detailedView);
                        
            //Console.WriteLine($"Stad                  {(detailedView ? "P-Hus" : "     ")}         Elplatser       Upptagna       Lediga");
            Console.WriteLine("{0,-20}{1,-20}{2,-15}{3,-15}{4,-15}", "Stad", (detailedView ? "P-Hus" : "     "), "Elplatser", "Upptagna", "Lediga");
            foreach (ParkingHouse house in houses)
            {
                //Console.WriteLine($"{house.Stad}            \t{house.Parkeringshus}            \t{house.Elplatser}\t\t  {house.UpptagnaPlatser}\t\t   {house.LedigaPlatser}");
                Console.WriteLine("{0,-20}{1,-20}{2,-15}{3,-15}{4,-15}", house.Stad, house.Parkeringshus ,house.Elplatser, house.UpptagnaPlatser , house.LedigaPlatser);
            }
        }
        /**
        * Skriver ut en numrerad lista med alla lediga platser i ett parkeringshus
        */
        internal static void PrintAvailableSlots(List<ParkingSlot> slots)
        {
            foreach (ParkingSlot slot in slots)
            {
                Console.WriteLine($"Id: {slot.Id}\t{slot.CityName}           \t\t{slot.HouseName}           \t\t{(slot.ElectricOutlet ? "Elutttag" : "Ej Eluttag")}");
            }
        }
        /**
        * Skriver ut en numrerad lista med alla städer
        */
        internal static void PrintCities()
        {
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
        internal static void PrintParkedCars(List<Car> cars)
        {
            foreach (Car c in cars)
            {
                Console.WriteLine($"{c.Id}\t{c.Plate}\t{c.Make}\t{c.Color}\t{c.ParkingSlotsId}");
            }
            Console.WriteLine("----------------------------------------------");
        }

        internal static void PrintUnParkedCars(List<Car> cars)
        {
            foreach (Car c in cars)
            {
                Console.WriteLine($"{c.Id}\t{c.Plate}\t{c.Make}\t{c.Color}");
            }
            Console.WriteLine("----------------------------------------------");
        }
    }
}

