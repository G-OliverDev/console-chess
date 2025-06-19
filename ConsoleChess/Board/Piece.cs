namespace ConsoleChess.Board
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesQuantity { get; protected set; }
        public ChessBoard Board { get; protected set; }

        public Piece(Color color, ChessBoard board)
        {
            Position = null;
            Color = color;
            Board = board;
            MovesQuantity = 0;
        }

        public void IncreaseMovesQuantity()
        {
            MovesQuantity++;
        }

        public bool AreTherePossibleMoves()
        {
            bool[,] possibleMoves = PossibleMoves();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                        return true;
                }
            }

            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMoves()[position.Row, position.Column];
        }

        public abstract bool[,] PossibleMoves();
    }
}
