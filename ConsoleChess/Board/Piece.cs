namespace ConsoleChess.Board
{
    internal class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesQuantity { get; protected set; }
        public ChessBoard Board { get; protected set; }

        public Piece(Position position, Color color, ChessBoard board)
        {
            Position = position;
            Color = color;
            Board = board;
            MovesQuantity = 0;
        }
    }
}
