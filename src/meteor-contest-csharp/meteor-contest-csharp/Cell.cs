namespace meteor_contest_csharp
{
    public class Cell
    {
        public const int NUMBER_OF_SIDES = 6;

        private readonly Cell[] _neighbors = new Cell[NUMBER_OF_SIDES];

        public bool IsProcessed { get; set; }

        public Cell GetNeighbor(CellSide side)
        {
            var neighbor = _neighbors[(int) side];

            return neighbor;
        }

        public void SetNeighbor(CellSide side, Cell cell)
        {
            _neighbors[(int) side] = cell;
        }
    }
}