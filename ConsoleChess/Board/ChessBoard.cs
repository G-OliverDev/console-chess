namespace ConsoleChess.Board
{
    internal class ChessBoard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] _pieces;

        public ChessBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _pieces = new Piece[rows, columns];
        }

        public Piece Piece(int row, int column)
        {
            return _pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            return _pieces[position.Row, position.Column];
        }

        public bool ExistPiece(Position position)
        {
            ValidatePosition(position);

            return Piece(position) != null;
        }

        public void SetPiece(Piece piece, Position position)
        {
            if (ExistPiece(position))
                throw new ChessBoardException("There is already a piece on this position!");

            _pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
                return null;

            Piece aux = Piece(position);
            aux.Position = null;
            _pieces[position.Row, position.Column] = null;

            return aux;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
                return false;

            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
                throw new ChessBoardException("Invalid position!");
        }
    }
}
