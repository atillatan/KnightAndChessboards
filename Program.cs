/*
 * @Author: Atilla Tanrikulu 
 * @Date: 2022-01-09 09:06:06
 */
using System;
using System.Linq;
using System.Collections.Generic;
namespace Atilla.KnightAndChessboards
{

    public class Program
    {
        // Eight possible movements for a knight.
        private static readonly int[] X = { 2, 1, -1, -2, -2, -1, 1, 2, 2 };
        private static readonly int[] Y = { 1, 2, 2, 1, -1, -2, -2, -1, 1 };

        // Chessboard Dimensions
        private static readonly int N = 5;

        // Create Chessboard and load static Chessboard data.
        // ---------------------
        // | A | B | C |   | E |
        // |   | G | H | I | J |
        // | K | L | M | N | O |
        // | P | Q | R | S | T |
        // | U | V |   |   | Y |
        // --------------------- 
        private static readonly string[,] Chessboard = new string[5, 5] {
            { "U"  , "P", "K", null , "A"  },
            { "V"  , "Q", "L", "G"  , "B"  },
            { null , "R", "M", "H"  , "C"  },
            { null , "S", "N", "I"  , null },
            { "Y"  , "T", "O", "J"  , "E"  }
        };

        private static Dictionary<string, List<int[]>> ProducedStrings { get; set; } = new Dictionary<string, List<int[]>>();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Input: ");
                int remainingJumps = 0;
                var input = Console.ReadLine();  // Wait input from user
                bool isNumeric = int.TryParse(input, out remainingJumps);

                if (isNumeric)
                {
                    ProducedStrings.Clear();
                    StartKnightTour(remainingJumps);
                }
                else
                {
                    Console.WriteLine("Please Enter valid number!");
                }
            }
        }

        private static void StartKnightTour(int remainingJumps)
        {
            // visitedCells keeps history.
            // Start knights from all possible cells.
            for (var x = 0; x < N; x++)
            {
                for (var y = 0; y < N; y++)
                {
                    // A valid knight jump is not allowed to start or end on an non valid cell.
                    // Firts Cell has to be Valid
                    if (IsValid(x, y))
                    {
                        MoveKnight(new List<int[]>(), x, y, remainingJumps - 1);
                    }
                }
            }

            Console.WriteLine("Output: " + ProducedStrings.Count);
        }

        // Recursive function moves a knight using visited cell history.
        private static void MoveKnight(List<int[]> visitedCells, int x, int y, int remainingJumps)
        {
            // Add current cell to visited history      
            visitedCells.Add(new int[] { x, y });

            // if required jumps are completed, collect the solution
            if (remainingJumps == 0)
            {
                int vowelCount = VowelCount(visitedCells);

                // The resulting string cannot contain more than two vowels.            
                if (vowelCount <= 2)
                {
                    string producedString = ProduceString(visitedCells);
                    ProducedStrings[producedString] = visitedCells;
                    // Console.WriteLine(producedString + "," + visitedCells.Count);
                }

                // remove current cell from history for next possible paths
                visitedCells.RemoveAt(visitedCells.Count - 1);
                return;
            }

            // check all eight possible target cells for a knight.
            for (int k = 0; k < 8; k++)
            {
                // Create target cell using current cell
                int targetX = x + X[k];
                int targetY = y + Y[k];

                if (IsExist(targetX, targetY))
                {
                    if (remainingJumps == 1)
                    {
                        // A valid knight jump is not allowed to start or end on an non valid cell.
                        // Last Cell has to be Valid
                        if (IsValid(targetX, targetY))
                        {
                            MoveKnight(visitedCells, targetX, targetY, remainingJumps - 1);
                        }
                    }
                    else
                    {
                        // create new history for new path                     
                        MoveKnight(new List<int[]>(visitedCells), targetX, targetY, remainingJumps - 1);
                    }
                }
            }

            // remove current cell from history
            visitedCells.RemoveAt(visitedCells.Count - 1);
        }

        // Only cells containing a character are considered valid.
        private static bool IsValid(int x, int y)
        {
            return Chessboard[x, y] != null;
        }

        // Check if is valid chessboard coordinates. A knight cannot go out of the chessboard.
        private static bool IsExist(int x, int y)
        {
            if (x < 0 || y < 0 || x >= N || y >= N)
            {
                return false;
            }

            return true;
        }

        // The resulting string cannot contain more than two vowels.
        public static int VowelCount(List<int[]> visitedCells)
        {
            var vowels = new string[] { "A", "E", "I", "O", "U" };
            int vowelCount = 0;

            foreach (int[] c in visitedCells)
            {
                string cellValue = Chessboard[c[0], c[1]];

                if (cellValue != null && vowels.Contains<string>(cellValue))
                {
                    vowelCount++;
                }
            }

            return vowelCount;
        }

        private static string ProduceString(List<int[]> visitedCells)
        {
            string resultString = "";

            foreach (int[] c in visitedCells)
            {
                resultString = resultString + Chessboard[c[0], c[1]];
            }

            return resultString;
        }
    }
}

