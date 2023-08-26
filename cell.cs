namespace Sudoku.Cells
{
    public struct Cell{
        public int Value{get;}
        public bool Visible{get; set;}

        public Cell(int value)
        {
            this.Value = value;
            this.Visible = true;
        }

        public override string ToString() => $"{Value}";

        public static implicit operator int(Cell cell)
        {
            return cell.Value;
        }

        public static implicit operator Cell(int foo)
        {
            return new Cell(foo);
        }
    }
}