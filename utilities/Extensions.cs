using Sudoku.Objects;

namespace Sudoku.Tools;

public static class SudokuExtensions
{
    public static void DisplayBoard(this SudokuGame level)
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
                Console.Write($"|{level.FilledBoard[i,j]}");
            
            Console.WriteLine("\n-------------------");
        }
    }
    
//make private in SudokuGame class
    public static Cell[,] RandomizeBoard(this Cell[,] board, int counter)
    {
        int length = board.GetLength(0);
        int elements = length * length;

        HashSet<int> null_cells_set = new();

        Random random = new();
        while(counter > 0)
        {
            int num = random.Next(1, elements + 1);

            if(!null_cells_set.Contains(num))
            {
                null_cells_set.Add(num);
                counter--;
            }
        }

        Cell[,] res = new Cell[length, length];

        for(int a = 0; a < length; a++)
            for(int b = 0; b < length; b++)
            {
                if(null_cells_set.Contains(elements)) res[a, b] = new Cell((int)board[a,b], false);
                else res[a, b] = board[a, b];

                elements--;
            }

        return res;
    }
}