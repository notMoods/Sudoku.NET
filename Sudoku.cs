namespace Sudoku
{
    public class SudokuBoard 
    {
        private readonly int[,] _board;
        public int[,] Board {get =>_board;}

        public SudokuBoard() => _board = BoardGenerator();

        private static int[,] BoardGenerator()
        {
            int[,] board = new int[9,9];

            int n = board.GetLength(0);
            int sq_n = (int)Math.Sqrt(n);
            
            //board generation
            DiagonalGenerator(board, n, sq_n);

            GeneratingTheRest(0, sq_n, board, n, sq_n);

            return board;
        }



        //board generation logic


        private static void DiagonalGenerator(int[,] board, int length, int sqr_length)
        {
            for(int i = 0; i < length; i += sqr_length)
            {
                FillBox(board, i, i, length, sqr_length);
            }
        }

        private static void FillBox(int[,] board, int row, int column, int length, int sqr_length)
        {
            int rand;

            for(int i = 0; i < sqr_length; i++)
            {
                for(int j = 0; j < sqr_length; j++)
                {
                    do
                    {
                        rand = RandomGen(length);
                    }
                    while(!UnusedInBox(board, row, column, sqr_length, rand));

                    board[row + i, column + j] = rand;
                }
            }
        }

        private static bool UnusedInBox(int[,] board, int rowStart, int colStart, int sqr_length, int num)
        {
            for(int i = 0; i < sqr_length; i++)
                for(int j = 0; j < sqr_length; j++)
                    if(board[rowStart + i, colStart + j] == num)
                        return false;
            
            return true;

            
        }

        private static int RandomGen(int length)
        {
            Random rand= new Random();
            return (int) Math.Floor((double)(rand.NextDouble()*length + 1));
        }

        private static bool GeneratingTheRest(int i, int j, int[,] board, int length, int sqr_length)
        {
            if (j >= length && i < length - 1) 
            {
                i = i + 1;
                j = 0;
            }

            if(i >= length && j >= length) return true;

            if(i < sqr_length)
            {
                if(j < sqr_length)
                    j = sqr_length;
            }
            else if(i < length - sqr_length){
                if(j == (int)(i / sqr_length) * sqr_length){
                    j += sqr_length;
                }
            }
            else{
                if(j == length - sqr_length){
                    i += 1;
                    j = 0;
                    if(i >= length) return true;
                }
            }

            for(int num = 1; num <= length; num++)
            {
                if(CheckIfSafe(i, j, num, board, length, sqr_length)){
                    board[i, j] = num;
                    if(GeneratingTheRest(i, j + 1, board, length, sqr_length)){
                        return true;
                    }
                    board[i, j] = 0;
                }
            }
            
            return false;
        }
        private static bool CheckIfSafe(int i, int j, int num, int[,] board, int length, int sqr_length)
        {
            return (unUsedInRow(board, length, i, num) && unUsedInCol(board, length, j, num) 
                   && UnusedInBox(board, i-i%sqr_length, j-j%sqr_length, sqr_length, num));
        }

        private static bool unUsedInCol(int[,] board, int length, int j, int num)
        {
            for (int i = 0; i < length; i++)
                if (board[i,j] == num)
                    return false;

            return true;
        }

        private static bool unUsedInRow(int [,] board, int length, int i, int num)
        {
            for (int j = 0; j < length; j++)
               if (board[i,j] == num)
                    return false;

            return true;
        }
    }
}