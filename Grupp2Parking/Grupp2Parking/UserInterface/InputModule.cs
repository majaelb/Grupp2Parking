using System.Text.RegularExpressions;

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
        /**
         * Gets a string validated against a regular expression
         * ^ anchors front
         * [0-9]{3} 3 Figures
         * [A-Z]{3} 3 Uppercase letters
         * $ anchors end
         * 
         * Give a bad expression is VERY DANGER! and will lock the program in nonterminating loop
         */
        internal static string GetValidatedString(string patternRegEx = "^[A-Z]{3}[0-9]{3}$") {
            while (true) {
                string validateString = GetString().ToUpper();
                if (Regex.IsMatch(validateString, patternRegEx)) {
                    return validateString;
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
