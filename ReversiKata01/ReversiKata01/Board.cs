using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        List<SquareContent> _squareContents = new List<SquareContent>();
        string _nextToMove;

        public Board()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
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
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
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

                return candidates;
            }
        }

        private bool isCandidateSquare(int x, int y)
        {
            bool result = false;
            if(GetSquareContent(x, y) == ".")
            {
                for(int xTransform = -1; xTransform <= 1; xTransform++)
                {
                    for(int yTransform = -1; yTransform <= 1; yTransform++)
                    {
                        
                        if(
                            (xTransform == 0 && yTransform ==0)
                            || xTransform + x < 0 || xTransform + x > 7
                            || y + yTransform < 0 || y + yTransform > 7
                            )
                        {
                            continue;
                        }
                    }
                }
            }
        }

    }
}
