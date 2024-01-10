using SkiaSharp;
using Sudoku.Objects;

namespace Sudoku.Tools
{
    public static class SudokuImage
    {
        public static void GenerateCompleteImage(this SudokuGame game)
        {
            int cellSize = 60, gridSize = cellSize * 9;
            int boardSize = gridSize + 40;

            using (SKBitmap bitmap = new SKBitmap(boardSize, boardSize))
            using (SKCanvas canvas = GenerateGrid(bitmap, cellSize, boardSize))
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
                            int number = (int)game.FilledBoard[row, col];

                            float x = col * cellSize + (cellSize / 3);
                            float y = row * cellSize + (cellSize / 1.5f);
                            canvas.DrawText(number.ToString(), x + 40, y + 40, textPaint);
                        }
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite("images\\sudoku.png"))
                {
                    data.SaveTo(stream);
                }
            }
        }

        public static void GenerateIncompleteImage(this SudokuGame game)
        {
            int cellSize = 60, gridSize = cellSize * 9;
            int boardSize = gridSize + 40;

            using (SKBitmap bitmap = new SKBitmap(boardSize, boardSize))
            using (SKCanvas canvas = GenerateGrid(bitmap, cellSize, boardSize))
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
                            int? number = game.GameBoard[row, col].Visible ? (int)game.GameBoard[row, col] : null;
                            if (number is not null)
                            {
                                float x = col * cellSize + (cellSize / 3);
                                float y = row * cellSize + (cellSize / 1.5f);
                                canvas.DrawText(number.ToString(), x + 40, y + 40, textPaint);
                            }
                        }
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite("images\\sudoku_puzzle.png"))
                {
                    data.SaveTo(stream);
                }
            }
        }

        private static SKCanvas GenerateGrid(SKBitmap bitmap, int cellSize, int boardSize)
        {
            SKCanvas canvas = new(bitmap);
            using (SKPaint paint = new())
            {
                canvas.Clear(SKColors.White);

                paint.Color = SKColors.Black;
                paint.StrokeWidth = 5;

                canvas.DrawLine(40, 40, boardSize, 40, paint);
                canvas.DrawLine(40, 40, 40, boardSize, paint);
                
                for (int i = 1; i < 9; i++)
                {
                    if(i % 3 != 0) paint.StrokeWidth = 2;
                    else paint.StrokeWidth = 5;

                    
                    int linePosition = i * cellSize; //60
                    canvas.DrawLine(linePosition + 40, 0 + 40, linePosition + 40, boardSize, paint); //vertical line
                    canvas.DrawLine(0 + 40, linePosition + 40, boardSize, linePosition + 40, paint); //horizontal line
                }

                using SKTypeface typeface = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
                using SKPaint textPaint = new();

                textPaint.Color = new SKColor(30, 30, 30);
                textPaint.TextSize = 25;
                textPaint.Typeface = typeface;

                for (int a = 0; a <= 8; a++)
                {
                    float x = (cellSize * a) + 60;
                    var letter = (char)(65 + a);

                    canvas.DrawText(letter.ToString(), x, 30, textPaint);
                    canvas.DrawText($"{a + 1}", 15, x + 18, textPaint);
                }
            }
            return canvas;
        }
    }
}