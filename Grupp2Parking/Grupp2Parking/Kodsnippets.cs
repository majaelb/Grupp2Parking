using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp2Parking {
    internal class Kodsnippets {


    }
}
//Bra att ha för att välja Färg och tillverkare
/*
internal static readonly string[] s_colors = { "Röd", "Svart", "Gul", "Blå", "Vit", "Grå", "Grön", "Rosa" };
internal static readonly string[] s_brands = {  };

private static void PrintColorOptions() {
    Console.WriteLine("Välj färg på fordonet");
    Console.WriteLine("====");
    for (int option = 0; option < Vehicle.s_colors.Length; option++) {
        Console.WriteLine($"{option}: {Vehicle.s_colors[option]}");
    }
}
private static void PrintBrandOptions() {
    Console.WriteLine("Välj tillverkare");
    Console.WriteLine("====");
    for (int option = 0; option < Motorcycle.s_brands.Length; option++) {
        Console.WriteLine($"{option}: {Motorcycle.s_brands[option]}");
    }
}

input.Color = Vehicle.s_colors[InputModule.GetIntInRange(0, Vehicle.s_colors.Length - 1)];

private static string GenerateLicenseNumer() {
    return new string(Enumerable.Range(1, 3).Select(_ => (char)(s_random.Next(65, 91))).ToArray()) +
                   new string(Enumerable.Range(1, 3).Select(_ => (char)s_random.Next(48, 58)).ToArray());
}

private static string GenerateColor() {
    return s_colors[s_random.Next(s_colors.Length)];
}



*/