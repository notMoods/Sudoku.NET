using Sudoku;
using SkiaSharp;
using Sudoku.Cells;
using Sudoku.Extensions;

namespace Sudoku.Image
{
    public class SudokuImage
    {
        private readonly Cell[,] board;
        public Cell[,] Board {get => board;}
        private SudokuImage() => board = GenerateNewBoard();

        private Cell[,] GenerateNewBoard()
        {
            var intBoard = new SudokuBoard().Board;
            
            int length = intBoard.GetLength(0);

            var cellBoard = new Cell[9, 9];

            for(int i = 0; i < length; i++)
                for(int j = 0; j < length; j++)
                    cellBoard[i, j] = intBoard[i, j];

            return cellBoard;
        }

        private void GenerateCompleteImage()
        {
            int cellSize = 60, gridSize = cellSize * 9;

            using (SKBitmap bitmap = new SKBitmap(gridSize, gridSize))
            using (SKCanvas canvas = GenerateGrid(bitmap, cellSize, gridSize))
            using (SKPaint paint = new SKPaint())
            {
                paint.Color = SKColors.Black;
                paint.TextSize = 30;
                using (SKTypeface typeface = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright))
                using (SKPaint textPaint = new SKPaint())
                {
                    textPaint.Color = SKColors.Black;
                    textPaint.TextSize = 30;
                    textPaint.Typeface = typeface;

                    for (int row = 0; row < 9; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            int number = Board[row, col];
                            if (number != 0)
                            {
                                float x = col * cellSize + (cellSize / 3);
                                float y = row * cellSize + (cellSize / 1.5f);
                                canvas.DrawText(number.ToString(), x, y, textPaint);
                            }
                        }
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite("sudoku.png"))
                {
                    data.SaveTo(stream);
                }
            }

        }

        private void GenerateIncompleteImage(int numo)
        {
            RandomizeBoard(numo);

            int cellSize = 60, gridSize = cellSize * 9;

            using (SKBitmap bitmap = new SKBitmap(gridSize, gridSize))
            using (SKCanvas canvas = GenerateGrid(bitmap, cellSize, gridSize))
            using (SKPaint paint = new SKPaint())
            {
                paint.Color = SKColors.Black;
                paint.TextSize = 30;
                using (SKTypeface typeface = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright))
                using (SKPaint textPaint = new SKPaint())
                {
                    textPaint.Color = SKColors.Black;
                    textPaint.TextSize = 30;
                    textPaint.Typeface = typeface;

                    for (int row = 0; row < 9; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            int? number = Board[row, col].Visible ? Board[row, col] : null;
                            if (number is not null)
                            {
                                float x = col * cellSize + (cellSize / 3);
                                float y = row * cellSize + (cellSize / 1.5f);
                                canvas.DrawText(number.ToString(), x, y, textPaint);
                            }
                        }
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite("sudoku_puzzle.png"))
                {
                    data.SaveTo(stream);
                }
            }
        }

        private void RandomizeBoard(int counter)
        {
            HashSet<int> sets = new HashSet<int>();
            sets.RandomSetGenerator(counter);

            int length = Board.GetLength(0);
            int elements = length * length;


            while(elements > 0) 
                for(int i = 0; i < length; i++)
                    for(int j = 0; j < length; j++) 
                    {
                        if(sets.Contains(elements)) Board[i, j].Visible = false;
                        elements--;
                    }
        }

        private SKCanvas GenerateGrid(SKBitmap bitmap, int cellSize, int gridSize)
        {
            SKCanvas canvas = new SKCanvas(bitmap);
            using (SKPaint paint = new SKPaint())
            {
                canvas.Clear(SKColors.White);

                paint.Color = SKColors.Black;
                for (int i = 1; i < 9; i++)
                {
                    if(i % 3 != 0) paint.StrokeWidth = 2;
                    else paint.StrokeWidth = 5;

                    
                    int linePosition = i * cellSize;
                    canvas.DrawLine(linePosition, 0, linePosition, gridSize, paint);
                    canvas.DrawLine(0, linePosition, gridSize, linePosition, paint);
                }
            }

            return canvas;
        }

        public static void GenerateComplete(int number)
        {
            var sudokuImg = new SudokuImage();
            
            sudokuImg.DisplayBoard();
            sudokuImg.GenerateCompleteImage();
            sudokuImg.GenerateIncompleteImage(45);

        }
    }
}