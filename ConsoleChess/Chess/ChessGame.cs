using ConsoleChess.Board;

namespace ConsoleChess.Chess
{
    internal class ChessGame
    {
        public ChessBoard Board { get; private set; }
        private int _turn;
        private Color _currentPlayer;
        public bool Finished { get; private set; }

        public ChessGame()
        {
            Board = new ChessBoard(8, 8);
            _turn = 1;
            _currentPlayer = Color.White;
            Finished = false;
            SetPieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncreaseMovesQuantity();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(piece, destination);
        }

        private void SetPieces()
        {
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.SetPiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.SetPiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }
    }
}
