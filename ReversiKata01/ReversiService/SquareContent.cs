using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiService
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
}
