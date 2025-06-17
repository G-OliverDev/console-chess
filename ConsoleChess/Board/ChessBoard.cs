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

        public void SetPiece(Piece piece, Position position)
        {
            _pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }
    }
}
