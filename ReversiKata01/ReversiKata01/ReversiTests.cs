using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ReversiKata01
{
    [TestFixture]
    public class ReversiTests
    {

        Game _game;

        [SetUp]
        public void SetUp()
        {
            _game = new Game();
        }

        [Test]
        public void NewBoardHasCorrectSetup()
        {
            var output = new StringBuilder();
            for(int y = 0; y < 8; y++)
            {
               for(int x = 0; x < 8; x++)
                {
                    output.Append(_game.GetSquareContent(x, y));
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
                _game.GraphicalText);
        }

        [Test]
        public void BlackIsFirstToMove()
        {
            Assert.AreEqual("B", _game.NextToMove);
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
            _game.CandidateSquares.Should().BeEquivalentTo(candidates,
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
	        _game.LegalMoves.Should().BeEquivalentTo(legalMoves,
                                                opt => opt.WithoutStrictOrdering()
                                                          .Excluding(a => a.Content));
        }

        [Test]
        public void CanConvertBoardToDelimitedString()
        {
            Assert.AreEqual(exampleBoardAtStartDelimitedText(),
                _game.DeliminatedStringOuput);
        }

        [Test]
        public void CanSetupBoardFromDelimitedString()
        {
            _game.SetupBoardFromDelimitedText(exampleBoardAfter9MovesDelimitedText());
            Assert.AreEqual(exampleBoardAfter9MovesDelimitedText(),
                _game.DeliminatedStringOuput);
        }

        [Test]
        public void CanMakeLegalMove()
        {
            _game.SetupBoardFromDelimitedText(exampleBoardAfter9MovesDelimitedText());
            _game.MakeMove(1, 0);
            Assert.AreEqual(exampleBoardAfter10MovesDelimitedText(),
                _game.DeliminatedStringOuput);
        }

        [Test]
        public void CanMakeLegalMoveWithTwoFlippedRows()
        {
            _game.SetupBoardFromDelimitedText(exampleBoardAfter10MovesDelimitedText());
            _game.MakeMove(5, 5);
            Assert.AreEqual(exampleBoardAfter11MovesDelimitedText(),
                _game.DeliminatedStringOuput);
        }

        [Test]
        public void CanReportWhiteScore()
        {
            _game.SetupBoardFromDelimitedText(
                exampleBoardAfter11MovesDelimitedText());
            Assert.AreEqual(7, _game.WhiteScore);
        }

        [Test]
        public void CanReportBlackScore()
        {
            _game.SetupBoardFromDelimitedText(
                exampleBoardAfter11MovesDelimitedText());
            Assert.AreEqual(8, _game.BlackScore);
        }

        [Test]
        public void CanReportUnableToMove()
        {
            _game.SetupBoardFromDelimitedText(
                exampleBoardWithNoLegalMoveDelimitedText());
            Assert.AreEqual(0, _game.LegalMoves.Count());
        }

        [Test]
        public void DoesNotReportGameOverWhenOnlyOnePlayerIsUnableToMove()
        {
            _game.SetupBoardFromDelimitedText(
                exampleBoardWithNoLegalMoveDelimitedText());
            Assert.AreEqual(false, _game.EndOfGame());
        }

        [Test]
        public void ReportsGameOverWhenBothPlayersCantMove()
        {
            _game.SetupBoardFromDelimitedText(
                exampleBoardWithNoLegalMoveForBothPlayersDelimitedText());
            Assert.AreEqual(true, _game.EndOfGame());
        }

        [Test]
        public void ReportsGameOverWhenBoardIsFull()
        {
            _game.SetupBoardFromDelimitedText(
                exampleBoardWithAllSquaresFilledDelimitedText());
            Assert.AreEqual(true, _game.EndOfGame());
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

        private static string exampleBoardAtStartDelimitedText()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                "........",
                "........",
                "........",
                "...BW...",
                "...WB...",
                "........",
                "........",
                "........",
                "B");
        }

        private static string exampleBoardAfter9Moves()
        {
            return
@"........
..B...W.
...B..W.
..BBBBW.
...BWW..
...B....
........
........";
        }

        private static string exampleBoardAfter9MovesDelimitedText()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                "........",
                "..B...W.",
                "...B..W.",
                "..BBBBW.",
                "...BWW..",
                "...B....",
                "........",
                "........",
                "W");
        }

        private static string exampleBoardAfter10MovesDelimitedText()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                ".W......",
                "..W...W.",
                "...W..W.",
                "..BBWBW.",
                "...BWW..",
                "...B....",
                "........",
                "........",
                "B");
        }

        private static string exampleBoardAfter11MovesDelimitedText()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                ".W......",
                "..W...W.",
                "...W..W.",
                "..BBWBW.",
                "...BBB..",
                "...B.B..",
                "........",
                "........",
                "W");
        }

        private static string exampleBoardWithNoLegalMoveDelimitedText()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                "...BWWWW",
                "WWWWWWBB",
                "WWWWWWWB",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "B");
        }

        private static string exampleBoardWithNoLegalMoveForBothPlayersDelimitedText()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                "...WWWWW",
                "WWWWWWBB",
                "WWWWWWWB",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "B");
        }

        private static string exampleBoardWithAllSquaresFilledDelimitedText()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                "BBBWWWWW",
                "WWWWWWBB",
                "WWWWWWWB",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "WWWWWWWW",
                "B");
        }
    }
}
