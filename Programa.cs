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

            string[,] AFND = new string[6, 6]
            {
                { "57437", "s", "g", "u", "i", "b" },
                { "S", "A, C", "H", " ", " ", " " },
                { "A", " ", "B", " ", " ", " " },
                { "B", " ", " ", " ", " ", " " },
                { "C", " ", "D", " ", " ", " " },
                { "D", " ", " ", "E", " ", " " },
            };
            
            // string[,] AFND = new string[2, 1]
            // {
            //     { "57437" },
            //     { "S" },
            // };

            var lines = File.ReadAllLines(filename);

            Console.WriteLine("\nLeitura do Arquivo: ");
            int stateHandlerCounter = 4;
            int newState = 0;

            for (var line = 0; line < lines.Length; line++)
            {
                if (Regex.IsMatch(lines[line], @"[a-zã]+")) //Caso a linha seja válida seja um simbolo
                {
                    Console.WriteLine(lines[line]); // Printa a linha

                    if (lines[line].Split("::=").Length == 2)
                    { //Parte de um estado
                        Console.WriteLine("Atribuição de estado");

                        string[] nextStates = lines[line].Split("::=");
                     
                        if (!nextStates[0].Contains("S")){ //Sequência da gramática
                            Console.WriteLine("Sequência da gramática");
                        }

                        else
                        { //Começo da gramática
                            Console.WriteLine("Começo da gramática");

                            string[] states = nextStates[1].Split("|");
                            foreach (var item in states)
                            {
                                Console.WriteLine(item);
                                string[] symbols = item.Split("<");
                                //Console.WriteLine(symbols[0]);
                                if (item.Contains("<")) { 
                                    string temp = GetRow(AFND, 0, 1);
                                    if (temp.Contains(symbols[0]))
                                    {
                                        string indexOfSymbol = string.Concat(temp.Where(c => !char.IsWhiteSpace(c)));
                                        int indexOfSymbolOnTable = indexOfSymbol.IndexOf(symbols[0].Replace(" ", String.Empty));
                                        AFND[1, indexOfSymbolOnTable+1] = AFND[1, indexOfSymbolOnTable+1] + (char)(65 + stateHandlerCounter) + ", ";
                                        
                                        string[] State = symbols[1].Split(">");
                                        string temp2 = GetColumn(AFND, 0, 1);

                                        if (temp2.Contains((char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1)))
                                        { //Caso o estado já esteja na matriz
                                            string indexOfSymbol2 = temp2.Replace(" ", String.Empty);
                                            int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf((char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1));
                                            AFND[indexOfSymbolOnTable2+1, indexOfSymbolOnTable+1] = (char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1) + ", ";
                                        }
                                        else
                                        { //Caso o estado NÃO esteja na matriz
                                            ResizeArray(ref AFND, AFND.GetLength(0)+1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                                            AFND[AFND.GetLength(0)-1, 0] = (char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1)+""; //Adiciona o estado na matriz
                                            string indexOfSymbol2 = temp2.Replace(" ", String.Empty);
                                            int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf((char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1));
                                            AFND[AFND.GetLength(0)-1, indexOfSymbolOnTable+1] = (char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1) + ", ";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Preciso adicionar o simbolo na matriz");
                                        ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1)+1); //Função para aumentar o tamanho da matriz

                                        string indexOfSymbol = string.Concat(temp.Where(c => !char.IsWhiteSpace(c)));
                                        int indexOfSymbolOnTable = indexOfSymbol.IndexOf(symbols[0].Replace(" ", String.Empty));
                                      
                                        AFND[0, AFND.GetLength(1)-1] = symbols[0].Replace(" ", String.Empty) + ""; //Adiciona o Simbolo novo no topo da matriz
                                        AFND[1, AFND.GetLength(1)-1] = (char)(65 + stateHandlerCounter) + ", ";
                                        
                                        string[] State = symbols[1].Split(">");
                                        string temp2 = GetColumn(AFND, 0, 1);

                                        if (temp2.Contains((char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1)))
                                        { //Caso o estado já esteja na matriz
                                            string indexOfSymbol2 = temp2.Replace(" ", String.Empty);
                                            int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf((char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1));
                                            AFND[indexOfSymbolOnTable2+1, indexOfSymbolOnTable+1] = (char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1) + ", ";
                                        }
                                        else
                                        { //Caso o estado NÃO esteja na matriz
                                            ResizeArray(ref AFND, AFND.GetLength(0)+1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                                            AFND[AFND.GetLength(0)-1, 0] = (char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1)+""; //Adiciona o estado na matriz
                                            string indexOfSymbol2 = temp2.Replace(" ", String.Empty);
                                            int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf((char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1));
                                            AFND[AFND.GetLength(0)-1, indexOfSymbolOnTable+1] = (char)(State[0].ToCharArray()[0] + stateHandlerCounter + 1) + ", ";
                                        }
                                    }
                                }
                            }
                            if (nextStates[1].Contains("ε"))
                            {
                                AFND[stateHandlerCounter+2, 0] = "*" + AFND[stateHandlerCounter+2, 0];
                            }
                        }

                        //ε 
                        
                     
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
                                //Console.WriteLine("O simbolo já existe na tabela"); //Debug
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
                                //Console.WriteLine("Não existe este simbolo na tabela"); //Debug
                                ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1)+1); //Função para aumentar o tamanho da matriz
                                AFND[0, AFND.GetLength(1)-1] = lines[line][symbol]+""; //Adiciona o simbolo no topo da matriz
                            
                                if (symbol != 0) //Caso NÂO seja o primeiro simbolo da palavra 
                                {
                                    ResizeArray(ref AFND, AFND.GetLength(0)+1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                                    AFND[stateHandlerCounter+2, 0] = (char)(65 + stateHandlerCounter)+"";
                                    stateHandlerCounter++;
                                    AFND[stateHandlerCounter+1, AFND.GetLength(1)-1] = AFND[stateHandlerCounter+1, AFND.GetLength(1)-1] + (char)(65 + stateHandlerCounter) + ", ";
                                }
                                else //É o primeiro símbolo da palavra
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
            PrintAF(ref AFND);
         
        }

        private static void determinize()
        {
            Console.WriteLine("Função De Determinação");
        }


        private static void PrintAF(ref string[,] AFND)
        {
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


        private static void ResizeArray(ref string[,] Arr, int x, int y)
        {
            string[,] _arr = new string[x, y];
  
            for (int i = 0; i < Arr.GetLength(0); i++)
                for (int j = 0; j < Arr.GetLength(1); j++)
                    _arr[i, j] = Arr[i, j];
            Arr = _arr;
        }

        static string GetRow(string[,] matrix, int rowNumber, int start)
        {
            string result = " ";
            for (var i = start; i < matrix.GetLength(1); i++)
                result = result + " " + matrix[rowNumber, i];
            return result;
        }
       
        static string GetColumn(string[,] matrix, int ColumnNumber, int start)
        {
            string result = " ";
            for (var i = start; i < matrix.GetLength(0); i++)
                result = result + " " + matrix[i, ColumnNumber];
            return result;
        }
    }
}
