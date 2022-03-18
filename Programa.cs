﻿using System;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace DETERMINADOR
{
    class Program
    {
        static void Main()
        {
            readFile();
            //constroi AF
            //determiniza
        }

        private static void readFile()
        {
            string filename = @"Entrada.txt";

            string[,] AFND = new string[3, 3]
            {
                { "Start", "s", "e" },
                { "S", "A, C", "H" },
                { "A", " ", "B" }
            };

            var lines = File.ReadAllLines(filename);

            Console.WriteLine("\nLeitura do Arquivo: ");

            for (var line = 0; line < lines.Length; line++)
            {
                if (Regex.IsMatch(lines[line], @"[a-zã]+")) //Caso o caractere seja um simbolo
                {
                    Console.WriteLine(lines[line]); // Printa o arquivo

                    if (lines[line].Split("::=").Length == 2)
                    { //Parte de um estado
                        Console.WriteLine("Atribuição de estado");
                        //Esta parte está meio confusa ainda, vou resolvendo nas próximas semanas
                        //procura no AFND se existe o estado => se sim, adiciona o proximo estado em S e cria um novo estado para o segundo simbolo da palavra, se não, cria um novo estado

                        //split por |; para ter o estado alterado e
                        //replace  <X> por ' '; para separar os simbolos
                    }
                    else
                    { //É uma palavra reservada
                        Console.WriteLine("Palavra reservada");
                        //ResizeArray(ref AFND, AFND.GetUpperBound(0) + 1);    //Função para aumentar o tamanho da matriz

                        //verificar se Console.Write(lines[line][0]); existe,
                        //adicionar um novo estado a 'S', e criar um novo estado para cada letra seguinte dessa palavra
                    }
                }
            }

            Console.WriteLine("Printing AFND: ");
            for (int i = 0; i <= AFND.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= AFND.GetUpperBound(0); j++)
                {
                    Console.Write(AFND[i, j] + "\t|");
                }

                Console.WriteLine();
            }
        }

        private static void determinize()
        {
            Console.WriteLine("Função De Determinação");
        }

        private static void ResizeArray(ref string[,] Arr, int x)
        {
            string[,] _arr = new string[x, 5];
            int minRows = Math.Min(x, Arr.GetLength(0));
            int minCols = Math.Min(5, Arr.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    _arr[i, j] = Arr[i, j];
            Arr = _arr;
        }
    }
}
