namespace Sudoku.Objects
{
    public readonly struct Cell{
        private readonly int value;
        public bool Visible{get;}

        public Cell(int value, bool visibility = true)
        {
            this.value = value;
            Visible = visibility;
        }

        public override readonly string ToString() => $"{(Visible ? $"{value}" : "NIL")}";

        public static explicit operator int(Cell cell)
        {
            if(!cell.Visible) throw new InvalidCastException("Cell is not visible");

            return cell.value;
        }

        public static implicit operator Cell(int foo) => new(foo);
    }
}