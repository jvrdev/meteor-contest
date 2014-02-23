namespace meteor_contest_csharp
{
    public class Piece
    {
        public const int NUMBER_OF_CELLS = 5;
        public const int NUMBER_OF_PERMUTATIONS = 6 * 2;

        private readonly PieceCell[] _pieceCells = new PieceCell[NUMBER_OF_CELLS];

        public int Number { get; private set; }

        private int _currentPermutation = 0;
        
        public Piece(PieceCell[] pieceCells, int number)
        {
            _pieceCells = pieceCells;
            Number = number;
        }

        private void RotatePiece()
        {
            for (var i = 0; i < NUMBER_OF_CELLS; i++)
            {
                _pieceCells[i].Rotate();
            }
        }

        private void FlipPiece()
        {
            for (var i = 0; i < NUMBER_OF_CELLS; i++)
            {
                _pieceCells[i].Flip();
            }
        }

        public Piece GetNextPermutation()
        {
            if (_currentPermutation == 12) _currentPermutation = 0;
            switch (_currentPermutation % 6)
            {
                case 0:
                    // flip after every 6 rotations
                    FlipPiece();
                    break;

                default:
                    RotatePiece();
                    break;
            }
            _currentPermutation++;
            return this;
        }

        public void ResetProcessed()
        {
            for (var i = 0; i < NUMBER_OF_CELLS; i++)
            {
                _pieceCells[i].IsProcessed = false;
            }
        }

        public PieceCell GetPieceCell(int index)
        {
            return _pieceCells[index];
        }
    }
}
