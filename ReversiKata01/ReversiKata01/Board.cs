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

    public class Board
    {
        private readonly List<SquareContent> _squareContents = new List<SquareContent>();
        private string _nextToMove;

        public Board()
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
            _nextToMove = "B";
        }

        public string GetSquareContent(int x, int y)
        {
            return _squareContents.First(a => a.X == x && a.Y == y).Content;
        }

        public void SetSquareContent(int x, int y, string content)
        {
            _squareContents.First(a => a.X == x && a.Y == y).Content = content;
        }

        public string GraphicalText
        {
            get
            {
                var output = new StringBuilder();
                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        output.Append(GetSquareContent(x, y));
                    }
                    output.Append(
                        y == 7 ? string.Empty : Environment.NewLine);
                }
                return output.ToString();
            }
        }

        public string NextToMove
        {
            get => _nextToMove;
            set => _nextToMove = value;
        }

        public List<SquareContent> CandidateSquares
        {
            get
            {
                var candidates = new List<SquareContent>();
                for (var x = 0; x < 7; x++)
                {
	                for (var y = 0; y < 7; y++)
	                {
		                if (isCandidateSquare(x, y) 
		                    && !candidates.Any(a => a.X == x && a.Y == y))
		                {
							candidates.Add(new SquareContent(x, y, "O"));
		                }
	                }
                }
                return candidates;
            }
        }

        private bool isCandidateSquare(int x, int y)
        {
            var result = false;
            var targetColor = _nextToMove == "B" ? "W" : "B";

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
                            || targetX < 0 || targetX > 7
                            || targetY < 0 || targetY > 7
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

		        return legalMoves;
	        }
        }

		

    }
}
