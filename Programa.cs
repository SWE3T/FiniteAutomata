using System;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

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

            int i = 0;
            Console.WriteLine("\nLeitura do Arquivo: ");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static void showTable()
        {
            Console.WriteLine("Função ShowTable");
        }
    }
}
