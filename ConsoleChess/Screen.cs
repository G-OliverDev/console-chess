using ConsoleChess.Board;
using ConsoleChess.Chess;

namespace ConsoleChess
{
    internal static class Screen
    {
        public static void PrintGame(ChessGame game)
        {
            PrintBoard(game.Board);
            PrintCapturedPieces(game);
            Console.WriteLine("\n\nTurn: " + game.Turn);
            Console.WriteLine("Waiting for play: " + game.CurrentPlayer);
        }

        private static void PrintCapturedPieces(ChessGame game)
        {
            Console.WriteLine("\nCaptured pieces: ");
            Console.Write("\nWhite:");
            PrintSet(game.CapturedPieces(Color.White));
            Console.Write("\nBlack:");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(game.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
        }

        private static void PrintSet(HashSet<Piece> set)
        {
            Console.Write(" [");
            foreach (Piece piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(ChessBoard board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(ChessBoard board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                        Console.BackgroundColor = changedBackground;
                    else
                        Console.BackgroundColor = originalBackground;

                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition ReadChessPosition()
        {
            string chessPosition = Console.ReadLine();
            char column = chessPosition[0];
            int row = int.Parse(chessPosition[1] + "");

            return new ChessPosition(column, row);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.Color == Color.White)
                    Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
