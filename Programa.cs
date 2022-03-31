using System;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Linq;

namespace DETERMINADOR {
  class Program {
    
    static void Main() {
      string filename = @"Entrada.txt";

      // string[,] AFND = new string[6, 6]
      // {
      //     { "57437", "s", "g", "u", "i", "r" },
      //     { "S", "A, C", "H", " ", " ", " " },
      //     { "A", " ", "B", " ", " ", " " },
      //     { "B", " ", " ", " ", " ", " " },
      //     { "C", " ", "D", " ", " ", " " },
      //     { "D", " ", " ", "E", " ", " " },
      // };

      string[, ] AFND = new string[2, 1] {
        {"57437"},
        {"S"    },
      };

      var lines = File.ReadAllLines(filename);

      Console.WriteLine("\nLeitura do Arquivo: ");
      int stateHandlerCounter = 0;

      for (var line = 0; line < lines.Length; line++) {
        if (Regex.IsMatch(lines[line], @"[a-zã]+")) //Caso a linha seja válida seja um simbolo
        {
          Console.WriteLine(lines[line]); // Printa a linha

          if (lines[line].Split("::=").Length == 2) { //Parte de um estado

            string[] nextStates = lines[line].Split("::=");

            if (!nextStates[0].Contains("S")) { //Sequência da gramática
              string[] states = nextStates[1].Split("|");
              foreach(var item in states) {
                string[] symbols = item.Split("<");
                if (item.Contains("<")) {
                  string temp = GetRow(AFND, 0, 1);
                  if (temp.Contains(symbols[0])) {
                    string indexOfSymbol = string.Concat(
                    temp.Where(c =>!char.IsWhiteSpace(c)));
                    int indexOfSymbolOnTable = indexOfSymbol.IndexOf(symbols[0].Replace(" ", String.Empty));

                    string[] State = symbols[1].Split(">");
                    string temp2 = GetColumn(AFND, 0, 1);
                    string indexOfSymbol2 = string.Concat(temp2.Where(c =>!char.IsWhiteSpace(c)));
                    int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf(State[0].Replace(" ", String.Empty));

                    AFND[indexOfSymbolOnTable2 + stateHandlerCounter, indexOfSymbolOnTable + 1] = AFND[indexOfSymbolOnTable2 + stateHandlerCounter, indexOfSymbolOnTable + 1] + (char)(65 + ((int) State[0].ToCharArray()[0] - 65) + (stateHandlerCounter - 1)) + ", ";

                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)))) { //Caso o estado NÃO esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)) + ""; //Adiciona o estado na matriz
                    }
                  }
                  else {
                    ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1) + 1); //Função para aumentar o tamanho da matriz

                    AFND[0, AFND.GetLength(1) - 1] = symbols[0].Replace(" ", String.Empty) + ""; //Adiciona o Simbolo novo no topo da matriz
                    string[] State = symbols[1].Split(">");

                    AFND[1, AFND.GetLength(1) - 1] = (char)(
                    65 + ((int) State[0].ToCharArray()[0] - 65) + stateHandlerCounter) + ", ";
                    string temp2 = GetColumn(AFND, 0, 1);

                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)))) { //Caso o estado já esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + stateHandlerCounter) + ""; //Adiciona o estado na matriz
                    }
                  }
                }
              }
            }

            else { //Começo da gramática
              stateHandlerCounter++;
              string[] states = nextStates[1].Split("|");
              foreach(var item in states) {
                string[] symbols = item.Split("<");
                if (item.Contains("<")) {
                  string temp = GetRow(AFND, 0, 1);
                  if (temp.Contains(symbols[0])) {
                    string indexOfSymbol = string.Concat(
                    temp.Where(c =>!char.IsWhiteSpace(c)));
                    int indexOfSymbolOnTable = indexOfSymbol.IndexOf(symbols[0].Replace(" ", String.Empty));

                    string[] State = symbols[1].Split(">");
                    AFND[1, indexOfSymbolOnTable + 1] = AFND[1, indexOfSymbolOnTable + 1] + (char)(65 + ((int) State[0].ToCharArray()[0] - 65) + (stateHandlerCounter - 1)) + ", ";
                    string temp2 = GetColumn(AFND, 0, 1);

                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)))) { //Caso o estado NÃO esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)) + ""; //Adiciona o estado na matriz
                      string indexOfSymbol2 = temp2.Replace(" ", String.Empty);
                      int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf((char)(
                      State[0].ToCharArray()[0] + (stateHandlerCounter - 1)));
                    }
                  }
                  else {
                    ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1) + 1); //Função para aumentar o tamanho da matriz

                    AFND[0, AFND.GetLength(1) - 1] = symbols[0].Replace(" ", String.Empty) + ""; //Adiciona o Simbolo novo no topo da matriz
                    string[] State = symbols[1].Split(">");

                    AFND[1, AFND.GetLength(1) - 1] = (char)(65 + ((int) State[0].ToCharArray()[0] - 65) + (stateHandlerCounter - 1)) + ", "; //Adiciona o ESTADO novo no topo da matriz
                    string temp2 = GetColumn(AFND, 0, 1);

                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + stateHandlerCounter - 1))) { //Caso o estado NÃO  esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)) + ""; //Adiciona o estado na matriz
                    }
                  }
                }
              }
            }

            if (nextStates[1].Contains("ε")) {
              string temp = GetColumn(AFND, 0, 1);
              string indexOfState1 = temp.Replace(" ", String.Empty);
              string indexOfState = indexOfState1.Replace("*", String.Empty);
              int indexOfStateOnTable = indexOfState.IndexOf((char)(nextStates[0].ToCharArray()[1] + (stateHandlerCounter - 1))) + 1;
              AFND[indexOfStateOnTable, 0] = "*" + AFND[indexOfStateOnTable, 0];
            }
          }
          else { //Parte de uma palavra reservada
            string temp;

            for (var symbol = 0; symbol <= lines[line].Length - 1; symbol++) //Percore a palavra reservada
            {
              temp = GetRow(AFND, 0, 1);

              if (temp.Contains(lines[line][symbol])) //Se o simbolo já existe na matrix
              {
                string indexOfSymbol = string.Concat(temp.Where(c =>!char.IsWhiteSpace(c)));
                int indexOfSymbolOnTable = indexOfSymbol.IndexOf((lines[line][symbol]));

                if (symbol != 0) //Caso seja o primeiro simbolo da palavra
                {
                  ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                  AFND[stateHandlerCounter + 2, 0] = (char)(65 + stateHandlerCounter) + "";
                  AFND[stateHandlerCounter + 1, indexOfSymbolOnTable + 1] = AFND[stateHandlerCounter + 1, indexOfSymbolOnTable + 1] + (char)(65 + stateHandlerCounter) + ", ";
                  stateHandlerCounter++;
                }
                else {
                  AFND[1, indexOfSymbolOnTable + 1] = AFND[1, indexOfSymbolOnTable + 1] + (char)(65 + stateHandlerCounter) + ", ";
                }
              }
              else {
                ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1) + 1); //Função para aumentar o tamanho da matriz
                AFND[0, AFND.GetLength(1) - 1] = lines[line][symbol] + ""; //Adiciona o simbolo no topo da matriz

                if (symbol != 0) //Caso NÂO seja o primeiro simbolo da palavra
                {
                  ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                  AFND[stateHandlerCounter + 2, 0] = (char)(65 + stateHandlerCounter) + "";
                  stateHandlerCounter++;
                  AFND[stateHandlerCounter + 1, AFND.GetLength(1) - 1] = AFND[stateHandlerCounter + 1, AFND.GetLength(1) - 1] + (char)(65 + stateHandlerCounter) + ", ";
                }
                else //É o primeiro símbolo da palavra
                {
                  AFND[1, AFND.GetLength(1) - 1] = AFND[1, AFND.GetLength(1) - 1] + (char)(65 + stateHandlerCounter) + ", ";
                }
              }
            }

            ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
            AFND[stateHandlerCounter + 2, 0] = "*" + (char)(65 + stateHandlerCounter);
            stateHandlerCounter++;
          }
        }
      }
      PrintAF(ref AFND);
    }

    private static void determinize() {
      Console.WriteLine("Função De Determinação");
    }

    private static void PrintAF(ref string[, ] AFND) {
      Console.WriteLine("Imprimindo o Autômato: ");
      for (int i = 0; i <= AFND.GetUpperBound(0); i++) {
        for (int j = 0; j <= AFND.GetUpperBound(1); j++) {
          Console.Write(AFND[i, j] + "\t|");
        }

        Console.WriteLine();
      }
    }

    private static void ResizeArray(ref string[, ] Arr, int x, int y) {
      string[, ] _arr = new string[x, y];

      for (int i = 0; i < Arr.GetLength(0); i++)
      for (int j = 0; j < Arr.GetLength(1); j++)
      _arr[i, j] = Arr[i, j];
      Arr = _arr;
    }

    static string GetRow(string[, ] matrix, int rowNumber, int start) {
      string result = " ";
      for (var i = start; i < matrix.GetLength(1); i++)
      result = result + " " + matrix[rowNumber, i];
      return result;
    }

    static string GetColumn(string[, ] matrix, int ColumnNumber, int start) {
      string result = " ";
      for (var i = start; i < matrix.GetLength(0); i++)
      result = result + " " + matrix[i, ColumnNumber];
      return result;
    }
  }
}