using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiKata01
{
    public class Board
    {
        private readonly List<SquareContent> _squareContents = new List<SquareContent>();
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
        }

        public void SetSquareContent(int x, int y, string content)
        {
            _squareContents.First(a => a.X == x && a.Y == y).Content = content;
        }

        public string GetSquareContent(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                return string.Empty;
            }
            return _squareContents.First(a => a.X == x && a.Y == y).Content;
        }

        public void DoActionOverSquares(
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

        public void SetupBoardFromTextArray(string[] text)
        {
            DoActionOverSquares(
                eachSquareMethod: (x, y) => SetSquareContent(x, y, text[y].Substring(x, 1))
            );
        }

        public string DelimitedStringOuput
        {
            get
            {
                var output = new StringBuilder();
                DoActionOverSquares(
                    eachSquareMethod: (x, y) => output.Append(GetSquareContent(x, y)),
                    eachRowMethod: (y) => output.Append("|")
                );
                return output.ToString();
            }
        }

        public string GraphicalText
        {
            get
            {
                var output = new StringBuilder();
                DoActionOverSquares(
                    eachSquareMethod: (x, y) => output.Append(GetSquareContent(x, y)),
                    eachRowMethod: (y) => output.Append(
                                    y == 7 ? string.Empty : Environment.NewLine)
                );
                return output.ToString();
            }
        }
    }
}
