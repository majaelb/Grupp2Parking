using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp2Parking.UserInterface {
    internal class InputModule {
        /**
         * Returns an input validated to exist within a collection of valid values
         */
        internal static int GetValidatedInt(IEnumerable<int> validationList) {
            while (true) {
                int validint = GetInt();
                if (validationList.Contains(validint)) {
                    return validint;
                }
            }
        }
        internal static string GetString() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null) {
                    return read;
                }
            }
        }
        internal static int GetInt() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null && int.TryParse(read, out int number)) {
                    return number;
                }
            }
        }
        internal static bool GetBool() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null && bool.TryParse(read, out bool value)) {
                    return value;
                }
            }
        }
        internal static int GetIntInRange(int lower, int upper) {
            int number = int.MaxValue;
            while (number < lower || number > upper) {
                number = GetInt();
            }
            return number;
        }
    }
}
