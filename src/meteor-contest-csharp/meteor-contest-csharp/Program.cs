using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace meteor_contest_csharp
{
    class Program
    {
        private int numberOfSolutions = 0;
        private Board board = new Board();
        private List<Piece> pieceList = new List<Piece>();

        static void Main(string[] args)
        {

            Console.WriteLine("Starting PuzzleSovler: " + DateTime.UtcNow);
            var watch = Stopwatch.StartNew();

            Program solver = new Program();
            solver.solve();

            Console.WriteLine("PuzzleSolver terminated: " + DateTime.UtcNow);
            Console.WriteLine(solver.numberOfSolutions + " solutions found.");
            Console.WriteLine("Run time: " + watch.ElapsedMilliseconds);
            Console.ReadLine();
        }

        public Program()
        {
            var cell = new PieceCell[50];
            for (int i = 0; i < cell.Length; i++)
            {
                cell[i] = new PieceCell();
            }

            // first piece
            cell[0].SetNeighbor(CellSide.EAST, cell[1]);
            cell[1].SetNeighbor(CellSide.WEST, cell[0]);
            cell[1].SetNeighbor(CellSide.EAST, cell[2]);
            cell[2].SetNeighbor(CellSide.WEST, cell[1]);
            cell[2].SetNeighbor(CellSide.EAST, cell[3]);
            cell[3].SetNeighbor(CellSide.WEST, cell[2]);
            cell[3].SetNeighbor(CellSide.SOUTH_EAST, cell[4]);
            cell[4].SetNeighbor(CellSide.NORTH_WEST, cell[3]);

            // second piece
            cell[5].SetNeighbor(CellSide.SOUTH_EAST, cell[6]);
            cell[6].SetNeighbor(CellSide.NORTH_WEST, cell[5]);
            cell[6].SetNeighbor(CellSide.SOUTH_WEST, cell[7]);
            cell[7].SetNeighbor(CellSide.NORTH_EAST, cell[6]);
            cell[7].SetNeighbor(CellSide.WEST, cell[8]);
            cell[8].SetNeighbor(CellSide.EAST, cell[7]);
            cell[8].SetNeighbor(CellSide.SOUTH_WEST, cell[9]);
            cell[9].SetNeighbor(CellSide.NORTH_EAST, cell[8]);

            // third piece
            cell[10].SetNeighbor(CellSide.WEST, cell[11]);
            cell[11].SetNeighbor(CellSide.EAST, cell[10]);
            cell[11].SetNeighbor(CellSide.SOUTH_WEST, cell[12]);
            cell[12].SetNeighbor(CellSide.NORTH_EAST, cell[11]);
            cell[12].SetNeighbor(CellSide.SOUTH_EAST, cell[13]);
            cell[13].SetNeighbor(CellSide.NORTH_WEST, cell[12]);
            cell[13].SetNeighbor(CellSide.SOUTH_EAST, cell[14]);
            cell[14].SetNeighbor(CellSide.NORTH_WEST, cell[13]);

            // fourth piece
            cell[15].SetNeighbor(CellSide.SOUTH_WEST, cell[16]);
            cell[16].SetNeighbor(CellSide.NORTH_EAST, cell[15]);
            cell[16].SetNeighbor(CellSide.WEST, cell[17]);
            cell[17].SetNeighbor(CellSide.EAST, cell[16]);
            cell[16].SetNeighbor(CellSide.SOUTH_WEST, cell[18]);
            cell[18].SetNeighbor(CellSide.NORTH_EAST, cell[16]);
            cell[17].SetNeighbor(CellSide.SOUTH_EAST, cell[18]);
            cell[18].SetNeighbor(CellSide.NORTH_WEST, cell[17]);
            cell[18].SetNeighbor(CellSide.SOUTH_EAST, cell[19]);
            cell[19].SetNeighbor(CellSide.NORTH_WEST, cell[18]);

            // fifth piece
            cell[20].SetNeighbor(CellSide.SOUTH_EAST, cell[21]);
            cell[21].SetNeighbor(CellSide.NORTH_WEST, cell[20]);
            cell[21].SetNeighbor(CellSide.SOUTH_WEST, cell[22]);
            cell[22].SetNeighbor(CellSide.NORTH_EAST, cell[21]);
            cell[21].SetNeighbor(CellSide.EAST, cell[23]);
            cell[23].SetNeighbor(CellSide.WEST, cell[21]);
            cell[23].SetNeighbor(CellSide.SOUTH_EAST, cell[24]);
            cell[24].SetNeighbor(CellSide.NORTH_WEST, cell[23]);

            // sixt piece
            cell[25].SetNeighbor(CellSide.SOUTH_WEST, cell[26]);
            cell[26].SetNeighbor(CellSide.NORTH_EAST, cell[25]);
            cell[25].SetNeighbor(CellSide.SOUTH_EAST, cell[27]);
            cell[27].SetNeighbor(CellSide.NORTH_WEST, cell[25]);
            cell[26].SetNeighbor(CellSide.SOUTH_EAST, cell[28]);
            cell[28].SetNeighbor(CellSide.NORTH_WEST, cell[26]);
            cell[27].SetNeighbor(CellSide.SOUTH_WEST, cell[28]);
            cell[28].SetNeighbor(CellSide.NORTH_EAST, cell[27]);
            cell[28].SetNeighbor(CellSide.SOUTH_WEST, cell[29]);
            cell[29].SetNeighbor(CellSide.NORTH_EAST, cell[28]);

            // seventh piece
            cell[30].SetNeighbor(CellSide.SOUTH_WEST, cell[31]);
            cell[31].SetNeighbor(CellSide.NORTH_EAST, cell[30]);
            cell[32].SetNeighbor(CellSide.SOUTH_EAST, cell[31]);
            cell[31].SetNeighbor(CellSide.NORTH_WEST, cell[32]);
            cell[31].SetNeighbor(CellSide.SOUTH_EAST, cell[33]);
            cell[33].SetNeighbor(CellSide.NORTH_WEST, cell[31]);
            cell[33].SetNeighbor(CellSide.SOUTH_WEST, cell[34]);
            cell[34].SetNeighbor(CellSide.NORTH_EAST, cell[33]);

            // eigth piece
            cell[35].SetNeighbor(CellSide.SOUTH_EAST, cell[36]);
            cell[36].SetNeighbor(CellSide.NORTH_WEST, cell[35]);
            cell[35].SetNeighbor(CellSide.SOUTH_WEST, cell[37]);
            cell[37].SetNeighbor(CellSide.NORTH_EAST, cell[35]);
            cell[37].SetNeighbor(CellSide.SOUTH_WEST, cell[38]);
            cell[38].SetNeighbor(CellSide.NORTH_EAST, cell[37]);
            cell[38].SetNeighbor(CellSide.SOUTH_EAST, cell[39]);
            cell[39].SetNeighbor(CellSide.NORTH_WEST, cell[38]);

            // ninth piece
            cell[40].SetNeighbor(CellSide.EAST, cell[41]);
            cell[41].SetNeighbor(CellSide.WEST, cell[40]);
            cell[41].SetNeighbor(CellSide.EAST, cell[42]);
            cell[42].SetNeighbor(CellSide.WEST, cell[41]);
            cell[42].SetNeighbor(CellSide.NORTH_EAST, cell[43]);
            cell[43].SetNeighbor(CellSide.SOUTH_WEST, cell[42]);
            cell[43].SetNeighbor(CellSide.EAST, cell[44]);
            cell[44].SetNeighbor(CellSide.WEST, cell[43]);

            // tenth piece
            cell[45].SetNeighbor(CellSide.EAST, cell[46]);
            cell[46].SetNeighbor(CellSide.WEST, cell[45]);
            cell[46].SetNeighbor(CellSide.EAST, cell[47]);
            cell[47].SetNeighbor(CellSide.WEST, cell[46]);
            cell[47].SetNeighbor(CellSide.NORTH_EAST, cell[48]);
            cell[48].SetNeighbor(CellSide.SOUTH_WEST, cell[47]);
            cell[47].SetNeighbor(CellSide.EAST, cell[49]);
            cell[49].SetNeighbor(CellSide.WEST, cell[47]);
            cell[49].SetNeighbor(CellSide.NORTH_WEST, cell[48]);
            cell[48].SetNeighbor(CellSide.SOUTH_EAST, cell[49]);

            PieceCell[] cells;
            for (int i = 0; i < 10; i++)
            {
                cells = new PieceCell[5];
                for (int j = 0; j < 5; j++)
                {
                    cells[j] = cell[(i * 5) + j];
                }
                pieceList.Add(new Piece(cells, i));
            }
        }

        public void solve()
        {
            if (pieceList.Count > 0)
            {
                // Take the first available piece.
                Piece currentPiece = (Piece)pieceList[0];
                pieceList.RemoveAt(0);

                for (int i = 0; i < Piece.NUMBER_OF_PERMUTATIONS; i++)
                {
                    Piece permutation = currentPiece.GetNextPermutation();

                    for (int j = 0; j < Board.NUMBEROFCELLS; j++)
                    {
                        if (board.placePiece(permutation, j))
                        {

                            /* We have now put a piece on the board, so we have to
                               continue this process with the next piece by recursively
                               calling the solve() method. */

                            solve();

                            /* We're back from the recursion and we have to continue
                               searching at this level, so we remove the piece we
                               just added from the board. */

                            board.removePiece(permutation);
                        }
                        // else the permutation doesn't fit on the board
                    }
                }

                // we're done with this piece
                pieceList.Insert(0, currentPiece);
            }
            else
            {

                /* All pieces have been placed on the board so we
                   have found a solution! */
                puzzleSolved();
            }
        }


        /**
         * <p>Prints out a message and saves the found solution.
         */
        private void puzzleSolved()
        {

            // Print out the solution number and time.
            numberOfSolutions++;
            Console.WriteLine(numberOfSolutions + " - " + DateTime.UtcNow);
        }
    }
}
