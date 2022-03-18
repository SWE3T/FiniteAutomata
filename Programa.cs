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

            for (var line = 0; line < lines.Length; line++)
            {
                if (Regex.IsMatch(lines[line], @"[a-zã]+")) //caso o caractere seja um simbolo
                {
                    Console.WriteLine(lines[line]); // Debug
                    //Console.WriteLine(lines[line].Split("::=").Length);
                
                    var result = lines[line].Split("::=").Length == 2 ? "Atribuição de estado" : "Palavra reservada";
                    Console.WriteLine(result + '\n');
                    //split por |; para ter o estado alterado e
                    //replace  <X> por ' '; para separar os simbolos
                }
            }
        }

        private static void determinize()
        {
            Console.WriteLine("Função De Determinação");
        }
    }
}
