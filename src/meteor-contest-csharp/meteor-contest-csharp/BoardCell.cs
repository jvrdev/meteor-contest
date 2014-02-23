using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meteor_contest_csharp
{
    public class BoardCell : Cell
    {
        private Piece piece = null;
        public Piece getPiece()
        {
            return piece;
        }
        public void setPiece(Piece piece)
        {
            this.piece = piece;
        }
    }
}
