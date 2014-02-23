using System.Collections.Generic;
using System.Linq;

namespace meteor_contest_csharp
{
    public class Board
    {
        public const int TOTAL_CELL_COUNT = 50;
        private const int CELLS_IN_ROW_COUNT = 5;
        private readonly BoardCell[] _boardCells = new BoardCell[TOTAL_CELL_COUNT];

        public Board()
        {
            for (var i = 0; i < TOTAL_CELL_COUNT; i++)
            {
                _boardCells[i] = new BoardCell();
            }
            for (var i = 0; i < TOTAL_CELL_COUNT; i++)
            {
                InitializeBoardCell(_boardCells[i], i);
            }
        }

        /**
        * Initialize the neighbours of the given boardCell at the given
        * index on the board
        */
        private void InitializeBoardCell(BoardCell boardCell, int index)
        {
            // define the row of the cell.
            var row = index/CELLS_IN_ROW_COUNT;
            // check if the cell is last or first in the row.
            var isFirst = false;
            var isLast = false;
            if (index%CELLS_IN_ROW_COUNT == 0) isFirst = true;
            if ((index + 1)%CELLS_IN_ROW_COUNT == 0) isLast = true;
            if (row%2 == 0)
            {
                // Even rows
                if (row != 0)
                {
                    // Northern neighbours
                    if (!isFirst)
                    {
                        boardCell.SetNeighbor(CellSide.NORTH_WEST, _boardCells[index - 6]);
                    }
                    boardCell.SetNeighbor(CellSide.NORTH_EAST, _boardCells[index - 5]);
                }
                if (row != ((TOTAL_CELL_COUNT/CELLS_IN_ROW_COUNT) - 1))
                {
                    // Southern neighbours
                    if (!isFirst)
                    {
                        boardCell.SetNeighbor(CellSide.SOUTH_WEST, _boardCells[index + 4]);
                    }
                    boardCell.SetNeighbor(CellSide.SOUTH_EAST, _boardCells[index + 5]);
                }
            }
            else
            {
                // Uneven rows
                // Northern neighbours
                if (!isLast)
                {
                    boardCell.SetNeighbor(CellSide.NORTH_EAST, _boardCells[index - 4]);
                }
                boardCell.SetNeighbor(CellSide.NORTH_WEST, _boardCells[index - 5]);
                // Southern neighbours
                if (row != ((TOTAL_CELL_COUNT/CELLS_IN_ROW_COUNT) - 1))
                {
                    if (!isLast)
                    {
                        boardCell.SetNeighbor(CellSide.SOUTH_EAST, _boardCells[index + (CELLS_IN_ROW_COUNT + 1)]);
                    }
                    boardCell.SetNeighbor(CellSide.SOUTH_WEST, _boardCells[index + CELLS_IN_ROW_COUNT]);
                }
            }

            // Set the east and west neighbours
            if (!isFirst)
            {
                boardCell.SetNeighbor(CellSide.WEST, _boardCells[index - 1]);
            }
            if (!isLast)
            {
                boardCell.SetNeighbor(CellSide.EAST, _boardCells[index + 1]);
            }
        }

        private void FindOccupiedBoardCells(List<BoardCell> occupiedCells, PieceCell pieceCell, BoardCell boardCell)
        {
            if (pieceCell != null && boardCell != null && !pieceCell.IsProcessed)
            {
                occupiedCells.Add(boardCell);

                /* Neighbouring cells can form loops, which would lead to an
                infinite recursion. Avoid this by marking the processed 
                cells. */

                pieceCell.IsProcessed = true;
                // Repeat for each neighbour of the piece cell
                for (var i = 0; i < Cell.NUMBER_OF_SIDES; i++)
                {
                    FindOccupiedBoardCells(occupiedCells,
                        (PieceCell) pieceCell.GetNeighbor((CellSide) i),
                        (BoardCell) boardCell.GetNeighbor((CellSide) i));
                }
            }
        }

        public bool PlacePiece(Piece piece, int boardCellIdx)
        {
            // We will manipulate the piece using its first cell
            return PlacePiece(piece, 0, boardCellIdx);
        }

        private bool PlacePiece(Piece piece, int pieceCellIdx, int boardCellIdx)
        {
            // We're going to process the piece
            piece.ResetProcessed();
            // Get all the boardCells that this piece would occupy
            var occupiedBoardCells = new List<BoardCell>();
            FindOccupiedBoardCells(occupiedBoardCells, piece.GetPieceCell(pieceCellIdx), _boardCells[boardCellIdx]);
            if (occupiedBoardCells.Count != Piece.NUMBER_OF_CELLS)
            {
                // Some cells of the piece don't fall on the board
                return false;
            }
            if (occupiedBoardCells.Any(t => t.Piece != null))
            {
                return false;
            }
            // Occupy the board cells with the piece
            foreach (var t in occupiedBoardCells)
            {
                t.Piece = piece;
            }
            return true; // The piece fits on the board
        }

        public void RemovePiece(Piece piece)
        {
            for (var i = 0; i < TOTAL_CELL_COUNT; i++)
            {
                // Piece objects are unique, so use reference equality
                if (_boardCells[i].Piece == piece)
                {
                    _boardCells[i].Piece = null;
                }
            }
        }
    }
}