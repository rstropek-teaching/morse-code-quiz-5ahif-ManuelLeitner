using Morse.Library;
using System;

namespace Morse.ConsoleApp {
    public class Program {
        public static void Main(string[] args) {
            try {
                switch (args[0]) {
                    case "--file":
                    case "-f":
                        MorseParser.Parse(args[1], args[2]);
                        break;
                    case "--param":
                    case "-p":
                        Console.WriteLine(MorseParser.Parse(args[1]));
                        break;
                    case "--std":
                        MorseParser.Parse(Console.In, Console.Out);
                        break;
                    default:
                        PrintUsage();
                        break;

                }
            } catch (IndexOutOfRangeException) {
                PrintUsage();
            } catch (Exception e) {
                Console.Error.WriteLine(e.Message);
            }
        }

        public static void PrintUsage() {
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName + " { {--file | -f} <input-file> " +
                "<output-file> | {--param | -p} <morse-text> | std}");
        }
    }
}
