using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiKata01
{

    public class SquareContent
    {
        public SquareContent(int x, int y, string content = ".")
        {
            X = x;
            Y = y;
            Content = content;
        }
        public int X { get; }
        public int Y { get; }
        public string Content { get; set; }
    }

    public class Game
    {
        private readonly List<SquareContent> _squareContents = new List<SquareContent>();

        public Game()
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    _squareContents.Add(new SquareContent(x, y, "."));
                }
            }
            SetSquareContent(3, 3, "B");
            SetSquareContent(4, 3, "W");
            SetSquareContent(3, 4, "W");
            SetSquareContent(4, 4, "B");
            NextToMove = "B";
        }

        public string GetSquareContent(int x, int y)
        {
            if(x < 0 || x > 7 || y < 0 || y > 7)
            {
                return string.Empty;
            }
            return _squareContents.First(a => a.X == x && a.Y == y).Content;
        }

        public void SetSquareContent(int x, int y, string content)
        {
            _squareContents.First(a => a.X == x && a.Y == y).Content = content;
        }

        private void iterateOverSquares(
            Action<int, int> eachSquareMethod, 
            Action<int> eachRowMethod = null)
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    eachSquareMethod.Invoke(x, y);
                }
                eachRowMethod?.Invoke(y);
            }
        }

        public string GraphicalText
        {
            get
            {
                var output = new StringBuilder();
                iterateOverSquares(
                    eachSquareMethod: (x, y) => output.Append(GetSquareContent(x, y)),
                    eachRowMethod: (y) => output.Append(
                                    y == 7 ? string.Empty : Environment.NewLine)
                );
                return output.ToString();
            }
        }

        public string NextToMove { get; set; }

        public string NotNextToMove => NextToMove == "B" ? "W" : "B";

        public List<SquareContent> CandidateSquares
        {
            get
            {
                var candidates = new List<SquareContent>();
                iterateOverSquares(
                    eachSquareMethod: (x, y) =>
                    {
                        if (isCandidateSquare(x, y)
                            && !candidates.Any(a => a.X == x && a.Y == y))
                        {
                            candidates.Add(new SquareContent(x, y, "O"));
                        }
                    });
                return candidates;
            }
        }

        private bool isCandidateSquare(int x, int y)
        {
            var result = false;
            var targetColor = NotNextToMove;

            if(GetSquareContent(x, y) == ".")
            {
                for(var xTransform = -1; xTransform <= 1; xTransform++)
                {
	                var targetX = x + xTransform;
                    for(var yTransform = -1; yTransform <= 1; yTransform++)
                    {
	                    var targetY = y + yTransform;
                        if(
                            (xTransform == 0 && yTransform ==0)
							|| result
                            )
                        {
	                        continue;
                        }
                        result = GetSquareContent(targetX, targetY) == targetColor;
                    }
                }
            }
            return result;
        }

        public List<SquareContent> LegalMoves
        {
	        get
	        {
		        var legalMoves = new List<SquareContent>();
                var candidates = CandidateSquares;
                foreach(var candidate in candidates)
                {
                    for(var xTransform = -1; xTransform <= 1; xTransform++)
                    {
                        for(var yTransform = -1; yTransform <= 1; yTransform++)
                        {
                            if(testForLine(candidate, xTransform, yTransform)
                                && !legalMoves.Any(
                                    a => a.X == candidate.X && a.Y == candidate.Y))
                            {
                                legalMoves.Add(candidate);
                            }
                        }
                    }
                }
		        return legalMoves;
	        }
        }

        public bool CanMove()
        {
            return LegalMoves.Count() > 0;
        }

        public bool EndOfGame()
        {
            var result = false;
            if(LegalMoves.Count() == 0)
            {
                NextToMove = NotNextToMove;
                result = (LegalMoves.Count() == 0);
                NextToMove = NotNextToMove;
            }
            return result;
        }


        private bool makeFlipsForMove(int x, int y)
        {
            var result = false;
            var piece = new SquareContent(x, y);
            for (var xTransform = -1; xTransform <= 1; xTransform++)
            {
                for (var yTransform = -1; yTransform <= 1; yTransform++)
                {
                    var lineFlipped =
                        testForLine(piece, xTransform, yTransform, true);
                    result = result || lineFlipped;
                }
            }
            return result;
        }

        private bool testForLine(SquareContent candidate, 
            int xTransform, int yTransform, bool makeMove = false)
        {
            var result = false;
            var currentColor = NextToMove;
            var opponentColor = NotNextToMove;
            int x = candidate.X + xTransform;
            int y = candidate.Y + yTransform;
            List<SquareContent> flipPeices = new List<SquareContent>();

            if(GetSquareContent(x, y) == opponentColor)
            {
                do
                {
                    flipPeices.Add(new SquareContent(x, y));
                    x += xTransform;
                    y += yTransform;
                }
                while (GetSquareContent(x, y) == opponentColor);

                if(GetSquareContent(x, y) == currentColor)
                {
                    if (makeMove)
                    {
                        flipPeices.ForEach(a => SetSquareContent(a.X, a.Y, NextToMove));
                    }
                    result = true;
                }
            }
            
            return result;
        }

        public string DeliminatedStringOuput
        {
            get
            {
                var output = new StringBuilder();
                iterateOverSquares(
                    eachSquareMethod: (x, y) => output.Append(GetSquareContent(x, y)),
                    eachRowMethod: (y) => output.Append("|")
                );
                output.Append(NextToMove);
                return output.ToString();
            }
        }

        public void SetupBoardFromDelimitedText(string delimitedText)
        {
            var input = delimitedText.Split('|');
            iterateOverSquares(
                eachSquareMethod: (x, y) => SetSquareContent(x, y, input[y].Substring(x, 1))
            );
            NextToMove = input[8];
        }

        public bool MakeMove(int x, int y)
        {
            if(GetSquareContent(x, y) == "." && makeFlipsForMove(x, y))
            {
                SetSquareContent(x, y, NextToMove);
                NextToMove = NotNextToMove;
            }
            return false;
        }

        public int WhiteScore
        {
            get
            {
                int score = 0;
                iterateOverSquares(
                    eachSquareMethod: (x, y) => score += GetSquareContent(x, y) == "W" ? 1 : 0
                );
                return score;
            }
        }

        public int BlackScore
        {
            get
            {
                int score = 0;
                iterateOverSquares(
                    eachSquareMethod: (x, y) => score += GetSquareContent(x, y) == "B" ? 1 : 0
                );
                return score;
            }
        }
    }
}
