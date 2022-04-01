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

          if (lines[line].Split("::=").Length == 2) { //Parte de uma gramática

            string[] nextStates = lines[line].Split("::=");

            if (!nextStates[0].Contains("S")) { //Sequência da gramática
              string[] states = nextStates[1].Split("|");
              foreach(var item in states) {
                if (item.Contains("<")) {
                string[] symbols = item.Split("<");
                  string temp = GetRow(AFND, 0, 1); 
                  if (temp.Contains(symbols[0])) { //Caso o símbolo esteja na matriz
                    string indexOfSymbol = string.Concat(temp.Where(c =>!char.IsWhiteSpace(c)));
                    int indexOfSymbolOnTable = indexOfSymbol.IndexOf(symbols[0].Replace(" ", String.Empty));

                    string[] State = symbols[1].Split(">");
                          
                    string temp2 = GetColumn(AFND, 0, 1);
                
                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)))) { //Caso o estado NÃO esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)) + ""; //Adiciona o estado na matriz
                    }

                    temp2 = GetColumn(AFND, 0, 1);
                    string indexOfSymbol2 = temp2.Replace(" ", String.Empty);
                    indexOfSymbol2 = indexOfSymbol2.Replace("*", String.Empty);
                    int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf(nextStates[0].ToCharArray()[1]);
                    AFND[(indexOfSymbolOnTable2+1) + (stateHandlerCounter-1), indexOfSymbolOnTable + 1] = AFND[(indexOfSymbolOnTable2+ 1) + (stateHandlerCounter-1), indexOfSymbolOnTable + 1] + (char)(65 + ((int) State[0].ToCharArray()[0] - 65) + (stateHandlerCounter - 1)) + ", ";
                  }
                  else { //Caso o símbolo NÃO esteja na matriz
                    ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1) + 1); //Função para aumentar o tamanho da matriz

                    AFND[0, AFND.GetLength(1) - 1] = symbols[0].Replace(" ", String.Empty) + ""; //Adiciona o Simbolo novo no topo da matriz
                    string[] State = symbols[1].Split(">");

                    string temp2 = GetColumn(AFND, 0, 1);
                    string indexOfSymbol2 = temp2.Replace(" ", String.Empty);
                    indexOfSymbol2 = indexOfSymbol2.Replace("*", String.Empty);
                    int indexOfSymbolOnTable2 = indexOfSymbol2.IndexOf(State[0].Replace("*", String.Empty));

                    AFND[indexOfSymbolOnTable2+1, AFND.GetLength(1) - 1] = (char)(65 + ((int) State[0].ToCharArray()[0] - 65) + (stateHandlerCounter-1)) + ", ";

                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)))) { //Caso o estado já esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + (stateHandlerCounter-1)) + ""; //Adiciona o estado na matriz
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
                  if (temp.Contains(symbols[0])) { //Caso a tabela já contém o simbolo
                    string indexOfSymbol = string.Concat(temp.Where(c =>!char.IsWhiteSpace(c)));
                    int indexOfSymbolOnTable = indexOfSymbol.IndexOf(symbols[0].Replace(" ", String.Empty));

                    string[] State = symbols[1].Split(">");
                    AFND[1, indexOfSymbolOnTable + 1] = AFND[1, indexOfSymbolOnTable + 1] + (char)(65 + ((int) State[0].ToCharArray()[0] - 65) + (stateHandlerCounter - 1)) + ", ";
                    string temp2 = GetColumn(AFND, 0, 1);

                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)))) { //Caso o estado NÃO esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)) + ""; //Adiciona o estado na matriz
                    }
                  }
                  else { // Caso a matriz não contenha o simbolo
                    ResizeArray(ref AFND, AFND.GetLength(0), AFND.GetLength(1) + 1); //Função para aumentar o tamanho da matriz

                    AFND[0, AFND.GetLength(1) - 1] = symbols[0].Replace(" ", String.Empty) + ""; //Adiciona o Simbolo novo no topo da matriz
                    string[] State = symbols[1].Split(">");

                    string temp2 = GetColumn(AFND, 0, 1);
                    if (!temp2.Contains((char)(State[0].ToCharArray()[0] + stateHandlerCounter - 1))) { //Caso o estado NÃO  esteja na matriz
                      ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                      AFND[AFND.GetLength(0) - 1, 0] = (char)(State[0].ToCharArray()[0] + (stateHandlerCounter - 1)) + ""; //Adiciona o estado na matriz
                    }

                    AFND[1, AFND.GetLength(1) - 1] = (char)(65 + ((int) State[0].ToCharArray()[0] - 65) + (stateHandlerCounter - 1)) + ", "; //Adiciona o ESTADO novo no topo da matriz

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

                if (symbol != 0) //Caso NÃO seja o primeiro simbolo da palavra
                {
                  ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                  AFND[stateHandlerCounter + 2, 0] = (char)(65 + stateHandlerCounter) + "";
                  AFND[stateHandlerCounter + 2, indexOfSymbolOnTable + 1] = AFND[stateHandlerCounter + 2, indexOfSymbolOnTable + 1] + (char)(65 + (stateHandlerCounter+1)) + ", ";
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
      Console.WriteLine();  
      Console.WriteLine();  
      Console.WriteLine();  
      Console.WriteLine("Determinização");

      determinize(ref AFND);
      setErrorState(ref AFND);
      PrintAF(ref AFND);
    }

    private static void determinize(ref string[, ] AFND) {
      for (int i = 1; i < AFND.GetLength(0); i++) {
        for (int j = 1; j < AFND.GetLength(1); j++) {
          if (AFND[i,j] != null && AFND[i,j].Contains(",") && !(AFND[i,j].Contains("["))) {
            string states2 = AFND[i, j].Replace(" ", String.Empty);
            string[] states = (states2.Split(','));
            if (states.Length >= 3) {
                AFND[i,j] = "[" + AFND[i,j].Remove(AFND[i,j].Length - 2) + "]";
                if (!GetColumn(AFND, 0, 1).Contains(AFND[i,j]))
                {
                    ResizeArray(ref AFND, AFND.GetLength(0) + 1, AFND.GetLength(1)); //Função para aumentar o tamanho da matriz
                    //newStates = AFND[i,j].Remove(AFND[i,j].Length - 2);
                    AFND[AFND.GetLength(0)-1, 0] = AFND[i,j];
                
                    foreach (var state in states)
                    {
                        if (!(state == states.Last()))
                        {
                            string temp = GetColumn(AFND, 0, 1);
                            string indexOfState1 = temp.Replace(" ", String.Empty);
                            string indexOfState = indexOfState1.Replace("*", String.Empty);
                            int indexOfStateOnTable = indexOfState.IndexOf(state);
                            
                            if (AFND[indexOfStateOnTable+1, 0].Contains("*") && !(AFND[AFND.GetLength(0)-1, 0].Contains("*")))
                            { //Passa o estado final para o novo estado criado;
                                AFND[AFND.GetLength(0)-1, 0] = "*" + AFND[AFND.GetLength(0)-1, 0];
                            }

                            for (var symbol = 1; symbol < AFND.GetLength(1); symbol++)
                            {
                                AFND[AFND.GetLength(0)-1, symbol] = AFND[AFND.GetLength(0)-1, symbol] + AFND[indexOfStateOnTable+1, symbol]; //Console.Write(AFND[indexOfStateOnTable+1, symbol]);
                            }             
                    
                        }
                    }
                }
            }
          }
        }
      }
    }

    private static void setErrorState(ref string[, ] AFND) {
      for (int i = 1; i <= AFND.GetLength(0)-1; i++) {
        for (int j = 1; j <= AFND.GetLength(1)-1; j++) {
            if (!(AFND[i,j] == null) && AFND[i,j].Length == 0 || (AFND[i,j] == null)) //Que crise meus amigos
            {
                AFND[i,j] = "<erro>";
            }
        }
      }
    }

    private static void PrintAF(ref string[, ] AFND) {
      Console.WriteLine();
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