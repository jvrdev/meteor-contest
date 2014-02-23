using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace meteor_contest_csharp
{
    public class Board
    {
        public const int NUMBEROFCELLS = 50;
        public const int NUMBEROFCELLSINROW = 5;
        private BoardCell[] boardCells = new BoardCell[NUMBEROFCELLS];
        public Board()
        {
            for (int i = 0; i < NUMBEROFCELLS; i++)
            {
                boardCells[i] = new BoardCell();
            }
            for (int i = 0; i < NUMBEROFCELLS; i++)
            {
                initializeBoardCell(boardCells[i], i);
            }
        }
        /**
        * Initialize the neighbours of the given boardCell at the given
        * index on the board
        */
        private void initializeBoardCell(BoardCell boardCell, int index)
        {
            // define the row of the cell.
            int row = index / NUMBEROFCELLSINROW;
            // check if the cell is last or first in the row.
            bool isFirst = false;
            bool isLast = false;
            if (index % NUMBEROFCELLSINROW == 0) isFirst = true;
            if ((index + 1) % NUMBEROFCELLSINROW == 0) isLast = true;
            if (row % 2 == 0)
            { // Even rows
                if (row != 0)
                {
                    // Northern neighbours
                    if (!isFirst)
                    {
                        boardCell.SetNeighbor(CellSide.NORTH_WEST, boardCells[index - 6]);
                    }
                    boardCell.SetNeighbor(CellSide.NORTH_EAST, boardCells[index - 5]);
                }
                if (row != ((NUMBEROFCELLS / NUMBEROFCELLSINROW) - 1))
                {
                    // Southern neighbours
                    if (!isFirst)
                    {
                        boardCell.SetNeighbor(CellSide.SOUTH_WEST, boardCells[index + 4]);
                    }
                    boardCell.SetNeighbor(CellSide.SOUTH_EAST, boardCells[index + 5]);
                }
            }
            else
            { // Uneven rows
                // Northern neighbours
                if (!isLast)
                {
                    boardCell.SetNeighbor(CellSide.NORTH_EAST, boardCells[index - 4]);
                }
                boardCell.SetNeighbor(CellSide.NORTH_WEST, boardCells[index - 5]);
                // Southern neighbours
                if (row != ((NUMBEROFCELLS / NUMBEROFCELLSINROW) - 1))
                {
                    if (!isLast)
                    {
                        boardCell.SetNeighbor(CellSide.SOUTH_EAST, boardCells[index + (NUMBEROFCELLSINROW + 1)]);
                    }
                    boardCell.SetNeighbor(CellSide.SOUTH_WEST, boardCells[index + NUMBEROFCELLSINROW]);
                }
            }

            // Set the east and west neighbours
            if (!isFirst)
            {
                boardCell.SetNeighbor(CellSide.WEST, boardCells[index - 1]);
            }
            if (!isLast)
            {
                boardCell.SetNeighbor(CellSide.EAST, boardCells[index + 1]);
            }
        }
        public void findOccupiedBoardCells(List<BoardCell> occupiedCells, PieceCell pieceCell, BoardCell boardCell)
        {
            if (pieceCell != null && boardCell != null && !pieceCell.IsProcessed())
            {
                occupiedCells.Add(boardCell);

                /* Neighbouring cells can form loops, which would lead to an
                infinite recursion. Avoid this by marking the processed 
                cells. */

                pieceCell.SetProcessed(true);
                // Repeat for each neighbour of the piece cell
                for (int i = 0; i < Cell.NUMBER_OF_SIDES; i++)
                {
                    findOccupiedBoardCells(occupiedCells,
                    (PieceCell)pieceCell.GetNeighbor((CellSide)i),
                    (BoardCell)boardCell.GetNeighbor((CellSide)i));
                }
            }
        }
        public bool placePiece(Piece piece, int boardCellIdx)
        {
            // We will manipulate the piece using its first cell
            return placePiece(piece, 0, boardCellIdx);
        }

        public bool placePiece(Piece piece, int pieceCellIdx, int boardCellIdx)
        {
            // We're going to process the piece
            piece.ResetProcessed();
            // Get all the boardCells that this piece would occupy
            var occupiedBoardCells = new List<BoardCell>();
            findOccupiedBoardCells(occupiedBoardCells, piece.getPieceCell(pieceCellIdx), boardCells[boardCellIdx]);
            if (occupiedBoardCells.Count != Piece.NUMBER_OF_CELLS)
            {
                // Some cells of the piece don't fall on the board
                return false;
            }
            for (int i = 0; i < occupiedBoardCells.Count; i++)
            {
                if (((BoardCell)occupiedBoardCells[i]).getPiece() != null)
                    // The board cell is already occupied by another piece
                    return false;
            }
            // Occupy the board cells with the piece
            for (int i = 0; i < occupiedBoardCells.Count; i++)
            {
                ((BoardCell)occupiedBoardCells[i]).setPiece(piece);
            }
            return true; // The piece fits on the board
        }
        public void removePiece(Piece piece)
        {
            for (int i = 0; i < NUMBEROFCELLS; i++)
            {
                // Piece objects are unique, so use reference equality
                if (boardCells[i].getPiece() == piece)
                {
                    boardCells[i].setPiece(null);
                }
            }
        }
    }
}
