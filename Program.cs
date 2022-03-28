using System;

namespace ConwaysGameOfLife
{
  internal static class Program
  {
    private static void SimulateCellGen(int[,] grid, int numOfGens)
    {
      var m = grid.GetLength(0);
      var n = grid.GetLength(1);
      for (var i = 0; i < numOfGens; i++)
      {
        Console.WriteLine("Generation " + (i + 1));
        if (i != 0)
        {
          grid = BuildGraph(grid, m, n);
        }
        DrawBoard(grid, m, n);
      }
    }
    private static int[,] BuildGraph(int [,] grid, int m, int n)
      {
        var nextGrid = new int[m, n];

        // Loop through every cell
        for (var i = 1; i < m - 1; i++)
        {
          for (var j = 1; j < n - 1; j++)
          {
            // finding num Of neighbours that are alive
            var aliveNeighbours = 0;
            for (var k = -1; k <= 1; k++)
            {
              for (var l = -1; l <= 1; l++)
              {
                aliveNeighbours += grid[i + k, j + l];
              }
            }

            // The cell needs to be subtracted from its neighbours as it was counted before
            aliveNeighbours -= grid[i, j];

            // Implementing the Rules of Life
            // Cell is lonely and dies
            if (grid[i, j] == 1 && aliveNeighbours < 2)
            {
              nextGrid[i, j] = 0;
            }
            // Cell dies due to over population
            else if (grid[i, j] == 1 && aliveNeighbours > 3)
            {
              nextGrid[i, j] = 0;
            }
            // A new cell is born
            else if (grid[i, j] == 0 && aliveNeighbours == 3)
            {
              nextGrid[i, j] = 1;
            }
            // Remains the same
            else
            {
              nextGrid[i, j] = grid[i, j];
            }
          }
        }

        return nextGrid;
      }

    private static void DrawBoard(int[,] grid, int m, int n)
    {
      for (var i = 0; i < m; i++)
      {
        for (var j = 0; j < n; j++)
        {
          if (grid[i, j] == 0)
          {
            Console.Write(".");
          }
          else
          {
            Console.Write("*");
          }
        }
        Console.WriteLine();
      }
      Console.WriteLine();
    }

    public static void Main(string[] args)
    {
      // Set number of generations to simulate
      const int numOfGens = 5;
      // Init grid
      int[,] grid = {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
      };

      SimulateCellGen(grid, numOfGens);
    }
  }
}
