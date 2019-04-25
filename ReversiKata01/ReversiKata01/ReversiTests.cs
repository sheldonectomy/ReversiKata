using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiKata01
{
    [TestFixture]
    public class ReversiTests
    {

        Board _board;

        [SetUp]
        public void SetUp()
        {
            _board = new Board();
        }

        [Test]
        public void NewBoardHasCorrectSetup()
        {
            var output = new StringBuilder();
            for(int y = 0; y < 8; y++)
            {
               for(int x = 0; x < 8; x++)
                {
                    output.Append(_board.GetSquareContent(x, y));
                }
                output.Append(
                    y == 7 ? string.Empty : Environment.NewLine);
            }
            Assert.AreEqual(exampleBoardAtStart(), output.ToString());
        }

        [Test]
        public void CanDisplayGraphicalTextString()
        {
            Assert.AreEqual(
                exampleBoardAtStart(),
                _board.GraphicalText);
        }

        [Test]
        public void BlackIsFirstToMove()
        {
            Assert.AreEqual("B", _board.NextToMove);
        }

        [Test]
        public void CanFindCandidateSquares()
        {
            var candidates = new List<SquareContent>
            {
                new SquareContent(4, 2, "C"),
                new SquareContent(5, 2, "C"),
                new SquareContent(5, 3, "C"),
                new SquareContent(2, 4, "C"),
                new SquareContent(2, 5, "C"),
                new SquareContent(3, 5, "C")
            };
            Assert.AreEqual(candidates, _board.CandidateSquares);
        }

        private string exampleBoardAtStart()
        {
            return
@"........
........
........
...BW...
...WB...
........
........
........";
        }
    }
}
