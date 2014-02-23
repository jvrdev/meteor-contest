using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meteor_contest_csharp
{
    public class Cell
    {
        public const int NUMBER_OF_SIDES = 6;

        private Cell[] _neighbors = new Cell[NUMBER_OF_SIDES];

        private bool _isProcessed = false;

        public Cell GetNeighbor(CellSide side)
        {
            var neighbor = _neighbors[(int)side];

            return neighbor;
        }

        public void SetNeighbor(CellSide side, Cell cell)
        {
            _neighbors[(int)side] = cell;
        }

        public bool IsProcessed()
        {
            return _isProcessed;
        }

        public void SetProcessed(bool value)
        {
            _isProcessed = value;
        }
    }
}
