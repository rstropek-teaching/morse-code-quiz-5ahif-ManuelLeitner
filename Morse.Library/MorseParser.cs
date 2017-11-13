using System;
using System.IO;
using System.Text;

namespace Morse.Library {

    /// <summary>
    /// static class with methodes for parsing morse-code
    /// </summary>
    public class MorseParser {

        private static char[] hash = new char[243];

        static MorseParser() {
            hash[5] = 'A';
            hash[16] = 'B';
            hash[20] = 'C';
            hash[8] = 'D';
            hash[1] = 'E';
            hash[19] = 'F';
            hash[10] = 'G';
            hash[15] = 'H';
            hash[3] = 'I';
            hash[29] = 'J';
            hash[12] = 'K';
            hash[17] = 'L';
            hash[6] = 'M';
            hash[4] = 'N';
            hash[14] = 'O';
            hash[21] = 'P';
            hash[26] = 'Q';
            hash[9] = 'R';
            hash[7] = 'S';
            hash[2] = 'T';
            hash[11] = 'U';
            hash[23] = 'V';
            hash[13] = 'W';
            hash[24] = 'X';
            hash[28] = 'Y';
            hash[18] = 'Z';
            hash[61] = '1';
            hash[59] = '2';
            hash[55] = '3';
            hash[47] = '4';
            hash[31] = '5';
            hash[32] = '6';
            hash[34] = '7';
            hash[38] = '8';
            hash[46] = '9';
            hash[62] = '0';

        }

        /// <summary>
        /// reads the morse-text from <paramref name="input"/>, parses it and writes the result to
        /// <paramref name="output"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void Parse(TextReader input, TextWriter output) {
            Parse(input.Read, output.Write);
        }


        /// <summary>
        ///  reads the morse-text from a file specified by <paramref name="inputPath"/>, parses it 
        ///  and writes the result to the file specified by <paramref name="outputPath"/>
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        public static void Parse(string inputPath, string outputPath) {
            using (StreamReader isr = new StreamReader(inputPath, Encoding.Default, false, 4096))
            using (StreamWriter osw = new StreamWriter(outputPath, false, Encoding.Default, 4096))
                Parse(isr, osw);

        }

        /// <summary>
        /// parses <paramref name="text"/> and returns it
        /// </summary>
        /// <param name="text"></param>
        /// <returns>plain-text</returns>
        public static string Parse(string text) {
            StringBuilder sb = new StringBuilder();
            int i = -1;
            Parse(() => ++i < text.Length ? text[i] : -1, (c) => sb.Append(c));
            return sb.ToString();
        }

        /// <summary>
        /// parses morse-text inputed through <paramref name="read"/> char by char and passes it to <paramref name="write"/> char by char
        /// </summary>
        /// <param name="read"></param>
        /// <param name="write"></param>
        public static void Parse(Func<int> read, Action<char> write) {
            int number = 0, pow = 1, c;
            while ((c = read()) > 0) {
                if (c == ' ') {
                    char res = hash[number];
                    if (res == 0)
                        throw new FormatException("wrong morse pattern");

                    write(res);
                    int count = 1;
                    while (count < 5 && (c = read()) == ' ')
                        count++;
                    if (count == 4)
                        write(' ');
                    else if (count != 1)
                        throw new FormatException("wrong number of spaces " + count);
                    pow = 1;
                    number = 0;
                }

                if (c == '.') {
                    number += pow;
                    pow *= 2;
                } else if (c == '-') {
                    number += pow * 2;
                    pow *= 2;
                } else if (c != -1)
                    throw new FormatException("invalid character " + c);

            }
        }
    }
}