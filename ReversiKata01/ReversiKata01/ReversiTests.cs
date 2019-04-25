using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

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
                new SquareContent(2, 3, "o"),
                new SquareContent(2, 5, "o"),
                new SquareContent(3, 2, "o"),
                new SquareContent(2, 4, "o"),
                new SquareContent(4, 2, "o"),
                new SquareContent(4, 5, "o"),
                new SquareContent(5, 2, "o"),
                new SquareContent(3, 5, "o"),
                new SquareContent(5, 3, "o"),
                new SquareContent(5, 4, "o")
            };
            _board.CandidateSquares.Should().BeEquivalentTo(candidates,
                                               opt => opt.WithoutStrictOrdering()
                                                         .Excluding(a => a.Content));

        }

		[Test]
        public void CanFindLegalMoves()
        {
	        var legalMoves = new List<SquareContent>
	        {
		        new SquareContent(2, 4, "o"),
		        new SquareContent(3, 5, "o"),
		        new SquareContent(4, 2, "o"),
		        new SquareContent(5, 3, "o")
	        };
	        _board.CandidateSquares.Should().BeEquivalentTo(legalMoves,
                                                opt => opt.WithoutStrictOrdering()
                                                          .Excluding(a => a.Content));
        }

        private static string exampleBoardAtStart()
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
