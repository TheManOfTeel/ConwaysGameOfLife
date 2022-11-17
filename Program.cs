using System;

namespace ConwaysGameOfLife
{
  internal static class Program
  {
    /// <summary>
    /// Simulate the future cell generation using the Rule of Life and display it.
    /// Does not take into account wrapping edges.
    /// Does not simulate cells that could neigbor field of view
    /// </summary>
    /// <param name="grid">Starting grid from current generation</param>
    /// <param name="numOfGens">How many future generations to simulate</param>
    private static void SimulateCellGen(int[,] grid, int numOfGens)
    {
      // Get height
      var m = grid.GetLength(0);
      // Get width
      var n = grid.GetLength(1);

      // Create and print future generations one generation at a time
      for (var i = 0; i <= numOfGens; i++)
      {
        Console.Clear();
        Console.WriteLine("Generation " + i);
        // Only create a new grid if it isn't the base grid
        if (i != 0)
        {
          grid = BuildGraph(grid, m, n);
        }
        DrawBoard(grid, m, n);
        System.Threading.Thread.Sleep(1000);
      }
    }

    /// <summary>
    /// Construct a new grid by predicting cell growth/death using the Rules of Life
    /// </summary>
    /// <param name="grid">Current grid</param>
    /// <param name="m">Grid height</param>
    /// <param name="n">Grid width</param>
    /// <returns>Future grid prediction</returns>
    private static int[,] BuildGraph(int [,] grid, int m, int n)
      {
        var nextGrid = new int[m, n];

        // Loop through every cell that is not a border cell
        for (var i = 1; i < m - 1; i++)
        {
          for (var j = 1; j < n - 1; j++)
          {
            // find the number of living neighbours
            var livingNeighbors = 0;
            for (var k = -1; k <= 1; k++)
            {
              for (var l = -1; l <= 1; l++)
              {
                livingNeighbors += grid[i + k, j + l];
              }
            }
            // The cell needs to be subtracted from its neighbours as it was counted before
            livingNeighbors -= grid[i, j];

            // Cell is lonely and dies
            if (grid[i, j] == 1 && livingNeighbors < 2)
            {
              nextGrid[i, j] = 0;
            }
            // Cell dies because of over population
            else if (grid[i, j] == 1 && livingNeighbors > 3)
            {
              nextGrid[i, j] = 0;
            }
            // New cell is created
            else if (grid[i, j] == 0 && livingNeighbors == 3)
            {
              nextGrid[i, j] = 1;
            }
            // No change
            else
            {
              nextGrid[i, j] = grid[i, j];
            }
          }
        }

        return nextGrid;
      }

    /// <summary>
    /// Replace 0s with . and 1s with * and print the grid
    /// </summary>
    /// <param name="grid">Grid to print (mxn)</param>
    /// <param name="m">Grid height</param>
    /// <param name="n">Grid Width</param>
    private static void DrawBoard(int[,] grid, int m, int n)
    {
      for (var i = 0; i < m; i++)
      {
        for (var j = 0; j < n; j++)
        {
          Console.Write(grid[i, j] == 0 ? "." : "*");
        }
        Console.WriteLine();
      }
      Console.WriteLine();
    }

    /// <summary>
    /// Main method to run the simulation
    /// </summary>
    public static void Main()
    {
      // Input params
      const int numOfGens = 5;
      int[,] grid = {
        { 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
        { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 }
      };

      // Run the sim
      SimulateCellGen(grid, numOfGens);
    }
  }
}
