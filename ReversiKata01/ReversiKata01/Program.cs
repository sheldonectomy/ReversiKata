using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiKata01
{
    class Program
    {

        static void Main(string[] args)
        {
            do
            {
                Game game = new Game();
                while (!game.EndOfGame())
                {
                    outputBeginningOfTurn(game);
                    int x, y;
                    bool gotInput;
                    bool validMove;
                    do
                    {
                        do
                        {
                            gotInput = tryGetInput(out x, out y);
                            if (!gotInput)
                            {
                                outputAlertWithBoard(game, "INVALID INPUT");
                            }
                        }
                        while (!gotInput);

                        validMove = game.MakeMove(x, y);
                        if (!validMove)
                        {
                            outputAlertWithBoard(game, "ILLEGAL MOVE");
                        }
                    }
                    while (!validMove);
                }
                outputGameResult(game);
            }
            while (Console.ReadKey().KeyChar.ToString().Equals("y", StringComparison.OrdinalIgnoreCase));
           
        }

        private static bool tryGetInput(out int xCoordinate, out int yCoordinate)
        {
            var success = false;
            var input = Console.ReadLine();
            int x = 0, y = 0;
            var coordinates = input.Trim(' ').Split(',');
            if(coordinates.Length == 2)
            {
                if(int.TryParse(coordinates[0], out x))
                {
                    success = x >= 0 && x <= 7;
                }
                if (int.TryParse(coordinates[1], out y))
                {
                    success = success && y >= 0 && y <= 7;
                }
                x = success ? x : 0;
                y = success ? y : 0;
            }
            xCoordinate = x;
            yCoordinate = y;
            return success;
        }

        private static void outputBeginningOfTurn(Game game)
        {
            Console.Clear();
            Console.WriteLine(game.GraphicalTextWithCoordinateLabels);
            if (!game.CanMove())
            {
                Console.WriteLine("No valid move for {0}",
                    game.NextToMove == "B" ? "Black" : "White");
                game.NextToMove = game.NotNextToMove;
            }
            Console.WriteLine("{0} to play",
                game.NextToMove == "B" ? "Black" : "White");
        }

        private static void outputAlertWithBoard(Game game, string alert)
        {
            Console.Clear();
            Console.WriteLine("{0}{1}{2}**** {3} *****{4}{5}",
                                game.GraphicalTextWithCoordinateLabels,
                                Environment.NewLine, Environment.NewLine,
                                alert, Environment.NewLine,
                                $"{(game.NextToMove == "B" ? "Black" : "White")} to play");
        }

        private static void outputGameResult(Game game)
        {
            if (game.WhiteScore == game.BlackScore)
            {
                Console.Clear();
                Console.WriteLine("{0}{1}*******************************{2}Game Drawn :: {3} Black to {4} White{5}*******************************",
                    game.GraphicalTextWithCoordinateLabels, Environment.NewLine, Environment.NewLine,
                    game.BlackScore, game.WhiteScore, Environment.NewLine);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("{0}{1}*******************************{2}{3} Wins :: {4} to {5}{6}*******************************",
                game.GraphicalTextWithCoordinateLabels, Environment.NewLine, Environment.NewLine,
                game.BlackScore > game.WhiteScore ? "Black" : "White",
                game.BlackScore > game.WhiteScore ? game.BlackScore : game.WhiteScore,
                game.BlackScore > game.WhiteScore ? game.WhiteScore : game.BlackScore,
                Environment.NewLine);
            }
            Console.WriteLine("Type 'y' for another game, or any other key to exit");
        }

        private static string exampleBoardJustBeforeEnd()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
            "WWWWWWWB",
            "WWWWWWWB",
            "WWWWWWWB",
            "WWWWBWWB",
            ".WWBWWWB",
            "WWBWWWWB",
            "BBBBBBBB",
            "BWWWWWWB",
                "B");
        }
    }
}
