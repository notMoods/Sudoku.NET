using Sudoku.Tools;

namespace Sudoku.Objects
{
    public class SudokuGame
    {
        private readonly Cell[,] _filledboard;
        private readonly Cell[,] _gameboard;

        public Cell[,] FilledBoard{ get => _filledboard; }

        public Cell[,] GameBoard{ get => _gameboard; }

        public SudokuGame(int randomizer)
        {
            _filledboard = Board.BoardGenerator();

            _gameboard = _filledboard.RandomizeBoard(randomizer);
        }
    }
}