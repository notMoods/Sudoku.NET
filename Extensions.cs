using Sudoku.Image;
using System.Collections.Generic;

namespace Sudoku.Extensions;

public static class SudokuExtensions
{
    public static void DisplayBoard(this SudokuImage level)
    {
        var board = level.Board;

        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                Console.Write($"|{board[i,j]}");
            }
            Console.WriteLine("\n-------------------");
        }
    }

    public static void RandomSetGenerator(this HashSet<int> hashset, int count)
    {
        Random random = new Random();
        while(count > 0)
        {
            int num = random.Next(1, 82);

            if(!hashset.Contains(num))
            {
                hashset.Add(num);
                count--;
            }
        }
    }
}