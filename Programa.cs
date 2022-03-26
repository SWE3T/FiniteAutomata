using System;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Linq;

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

            // string[,] AFND = new string[6, 6]
            // {
            //     { "57437", "s", "e", "n", "ã", "o" },
            //     { "S", "A, C", "H", " ", "K", " " },
            //     { "A", " ", "B", " ", " ", " " },
            //     { "B", " ", " ", " ", " ", " " },
            //     { "C", " ", "D", " ", " ", " " },
            //     { "D", " ", " ", "E", " ", " " },
            // };
            
            string[,] AFND = new string[2, 1]
            {
                { "57437" },
                { "S" },
            };

            var lines = File.ReadAllLines(filename);

            Console.WriteLine("\nLeitura do Arquivo: ");
            int stateHandlerCounter = 0;

            for (var line = 0; line < lines.Length; line++)
            {
                if (Regex.IsMatch(lines[line], @"[a-zã]+")) //Caso a linha seja válida seja um simbolo
                {
                    Console.WriteLine(lines[line]); // Printa a linha

                    if (lines[line].Split("::=").Length == 2)
                    { //Parte de um estado
                        Console.WriteLine("Atribuição de estado");
                        //Esta parte está meio confusa ainda, vou resolvendo nas próximas semanas
                        //procura no AFND se existe o estado => se sim, adiciona o proximo estado em S e cria um novo estado para o segundo simbolo da palavra, se não, cria um novo estado

                        //split por |; para ter o estado alterado e
                        //replace  <X> por ' '; para separar os simbolos
                    }
                    else
                    { //Parte de uma palavra reservada
                        Console.WriteLine("Palavra reservada");
                        string temp;
                        //Console.WriteLine(temp);

                        //ResizeArray(ref AFND, AFND.GetUpperBound(0) + 1);    //Função para aumentar o tamanho da matriz
                        for (var symbol = 0; symbol <= lines[line].Length - 1; symbol++) //Percore a palavra reservada
                        {
                        temp = GetRow(AFND, 0, 1);

                            if (temp.Contains(lines[line][symbol])) //Se o simbolo já existe na matrix
                            {
                                Console.WriteLine("O simbolo já existe na tabela");
                                string indexOfSymbol = string.Concat(temp.Where(c => !char.IsWhiteSpace(c)));
                                int indexOfSymbolOnTable = indexOfSymbol.IndexOf((lines[line][symbol]));

                                if (symbol != 0) //Caso seja o primeiro simbolo da palavra 
                                {
                                    ResizeArray(ref AFND, AFND.GetLength(0)+1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                                    AFND[stateHandlerCounter+2, 0] = (char)(65 + stateHandlerCounter)+"";
                                    stateHandlerCounter++;
                                    AFND[stateHandlerCounter+1, indexOfSymbolOnTable+1] = AFND[stateHandlerCounter+1, indexOfSymbolOnTable+1] + (char)(65 + stateHandlerCounter) + ", ";
                                }
                                  else
                                  {
                                    AFND[1, indexOfSymbolOnTable+1] = AFND[1, indexOfSymbolOnTable+1] + (char)(65 + stateHandlerCounter) + ", ";
                                  }
                               
                            }
                            else
                            {
                                Console.WriteLine("Não existe este simbolo na tabela");
                                ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1)+1); //Função para aumentar o tamanho da matriz
                                AFND[0, AFND.GetLength(1)-1] = lines[line][symbol]+""; //Adiciona o simbolo no topo da matriz
                            
                                if (symbol != 0) //Caso seja o primeiro simbolo da palavra 
                                {
                                    ResizeArray(ref AFND, AFND.GetLength(0)+1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                                    AFND[stateHandlerCounter+2, 0] = (char)(65 + stateHandlerCounter)+"";
                                    stateHandlerCounter++;
                                    AFND[stateHandlerCounter+1, AFND.GetLength(1)-1] = AFND[stateHandlerCounter+1, AFND.GetLength(1)-1] + (char)(65 + stateHandlerCounter) + ", ";
                                }
                                else
                                {
                                    AFND[1, AFND.GetLength(1)-1] = AFND[1, AFND.GetLength(1)-1] + (char)(65 + stateHandlerCounter) + ", ";
                                }
                            }
                        }
                        
                        ResizeArray(ref AFND, AFND.GetLength(0)+1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                        AFND[stateHandlerCounter+2, 0] = "*"+(char)(65 + stateHandlerCounter);
                        stateHandlerCounter++;
                    }
                }
            }

            Console.WriteLine("Printing AFND: ");
            for (int i = 0; i <= AFND.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= AFND.GetUpperBound(1); j++)
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

        private static void ResizeArray(ref string[,] Arr, int x, int y)
        {
            string[,] _arr = new string[x, y];
  
            for (int i = 0; i < Arr.GetLength(0); i++)
                for (int j = 0; j < Arr.GetLength(1); j++)
                    _arr[i, j] = Arr[i, j];
            Arr = _arr;
        }

        static string GetRow(string[,] matrix, int rowNumber, int trueORfalse)
        {
            string result = " ";
            for (var i = trueORfalse; i < matrix.GetLength(1); i++)
                result = result + " " + matrix[rowNumber, i];
            return result;
        }
    }
}
