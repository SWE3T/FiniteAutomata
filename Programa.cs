using System;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace DETERMINADOR
{
    static class Program
    {
        static void Main()
        {
            readFile();
            //constroi AF
            //Determiniza
        }

        private static void readFile()
        {
            string filename = @"Entrada.txt";

            var lines = File.ReadAllLines(filename);

            Console.WriteLine("\nLeitura do Arquivo: ");

            for (var line = 0; line < lines.Length - 1; line++)
            {
                if (Regex.IsMatch(lines[line], @"[a-z]+")) //caso o caractere seja um simbolo
                {
                    Console.WriteLine(lines[line]); // Debug
                }
            }
        }

        private static void determinize()
        {
            Console.WriteLine("Função De Determinação");
        }
    }
}
