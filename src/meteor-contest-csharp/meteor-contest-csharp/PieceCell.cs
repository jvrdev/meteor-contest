using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meteor_contest_csharp
{
    public class PieceCell : Cell
    {
        public void Flip()
        {
            SwapNeighbors(CellSide.NORTH_EAST, CellSide.NORTH_WEST);
            SwapNeighbors(CellSide.EAST, CellSide.WEST);
            SwapNeighbors(CellSide.SOUTH_EAST, CellSide.SOUTH_WEST);
        }
        public void Rotate()
        {
            // Clockwise rotation
            var eastNeighbor = GetNeighbor(CellSide.EAST);
            SetNeighbor(CellSide.EAST, GetNeighbor(CellSide.NORTH_EAST));
            SetNeighbor(CellSide.NORTH_EAST, GetNeighbor(CellSide.NORTH_WEST));
            SetNeighbor(CellSide.NORTH_WEST, GetNeighbor(CellSide.WEST));
            SetNeighbor(CellSide.WEST, GetNeighbor(CellSide.SOUTH_WEST));
            SetNeighbor(CellSide.SOUTH_WEST, GetNeighbor(CellSide.SOUTH_EAST));
            SetNeighbor(CellSide.SOUTH_EAST, eastNeighbor);
        }

        private void SwapNeighbors(CellSide a, CellSide b)
        {
            var buffer = GetNeighbor(a);
            SetNeighbor(a, GetNeighbor(b));
            SetNeighbor(b, buffer);
        }
    }
}
