using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace meteor_contest_csharp
{
    public class Piece
    {
        public const int NUMBER_OF_CELLS = 5;
        public const int NUMBER_OF_PERMUTATIONS = 6 * 2;

        private int number;
        private PieceCell[] pieceCells = new PieceCell[NUMBER_OF_CELLS];

        private int currentPermutation = 0;
        public Piece(PieceCell[] pieceCells, int number)
        {
            this.pieceCells = pieceCells;
            this.number = number;
        }

        private void RotatePiece()
        {
            for (var i = 0; i < NUMBER_OF_CELLS; i++)
            {
                pieceCells[i].Rotate();
            }
        }

        private void FlipPiece()
        {
            for (int i = 0; i < NUMBER_OF_CELLS; i++)
            {
                pieceCells[i].Flip();
            }
        }

        public Piece GetNextPermutation()
        {
            if (currentPermutation == 12) currentPermutation = 0;
            switch (currentPermutation % 6)
            {
                case 0:
                    // flip after every 6 rotations
                    FlipPiece();
                    break;

                default:
                    RotatePiece();
                    break;
            }
            currentPermutation++;
            return this;
        }

        public void ResetProcessed()
        {
            for (var i = 0; i < NUMBER_OF_CELLS; i++)
            {
                pieceCells[i].SetProcessed(false);
            }
        }

        public PieceCell getPieceCell(int pieceCellIdx)
        {
            return pieceCells[pieceCellIdx];
        }
    }
}
