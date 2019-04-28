using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiKata01
{

    public class Game
    {
        private readonly Board _board = new Board();

        public Game()
        {
            NextToMove = "B";
        }

        public string GraphicalText
        {
            get
            {
                return _board.GraphicalText;
            }
        }

        public string NextToMove { get; set; }

        public string NotNextToMove => NextToMove == "B" ? "W" : "B";

        public List<SquareContent> CandidateSquares
        {
            get
            {
                var candidates = new List<SquareContent>();
                _board.DoActionOverSquares(
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

            if(_board.GetSquareContent(x, y) == ".")
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
                        result = _board.GetSquareContent(targetX, targetY) == targetColor;
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

            if(_board.GetSquareContent(x, y) == opponentColor)
            {
                do
                {
                    flipPeices.Add(new SquareContent(x, y));
                    x += xTransform;
                    y += yTransform;
                }
                while (_board.GetSquareContent(x, y) == opponentColor);

                if(_board.GetSquareContent(x, y) == currentColor)
                {
                    if (makeMove)
                    {
                        flipPeices.ForEach(a => _board.SetSquareContent(a.X, a.Y, NextToMove));
                    }
                    result = true;
                }
            }
            
            return result;
        }

        public string DelimitedStringOuput
        {
            get
            {
                var output = new StringBuilder();
                output.Append(_board.DelimitedStringOuput);
                return output.Append(NextToMove).ToString();
            }
        }

        public void SetupBoardFromDelimitedText(string delimitedText)
        {
            var input = delimitedText.Split('|');
            _board.SetupBoardFromTextArray(input);
            NextToMove = input[8];
        }

        public bool MakeMove(int x, int y)
        {
            if(_board.GetSquareContent(x, y) == "." && makeFlipsForMove(x, y))
            {
                _board.SetSquareContent(x, y, NextToMove);
                NextToMove = NotNextToMove;
            }
            return false;
        }

        public int WhiteScore
        {
            get
            {
                int score = 0;
                _board.DoActionOverSquares(
                    eachSquareMethod: (x, y) => score += _board.GetSquareContent(x, y) == "W" ? 1 : 0
                );
                return score;
            }
        }

        public int BlackScore
        {
            get
            {
                int score = 0;
                _board.DoActionOverSquares(
                    eachSquareMethod: (x, y) => score += _board.GetSquareContent(x, y) == "B" ? 1 : 0
                );
                return score;
            }
        }
    }
}
