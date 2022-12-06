using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp2Parking
{
    internal class GUI
    {
        internal static void CarMenu()
        {
            bool runProgram = true;

            while (runProgram)
            {
                var key = Console.ReadKey(true);

                Console.WriteLine("[L]ägg till bil");
                Console.WriteLine("[P]arkera bil");
                switch (key.KeyChar)
                {
                    case 'L':
                    case 'l':
                        // Lägg till ny bil
                        Console.WriteLine("Ange registreringsnummer(ABC123), märke och färg");
                        var newCar = new ParkingItems.Car
                        {
                            Plate = Console.ReadLine().ToUpper(),
                            Make = Console.ReadLine(),
                            Color = Console.ReadLine()
                        };
                        int rowsAffected1 = ParkingHelpers.InsertCar(newCar);
                        Console.WriteLine("Antal bilar tillagda: " + rowsAffected1);
                        Console.WriteLine("----------------------------------------------");
                        break;

                    case 'P':
                    case 'p':
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
    }
}

