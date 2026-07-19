using System;
using System.Collections.Generic;
using OpusMatch.Board;

namespace OpusMatch.Gameplay
{
    /// <summary>
    /// Detects horizontal and vertical matches formed by adjacent balls on the board.
    /// </summary>
    public sealed class MatchDetector
    {
        /// <summary>
        /// Detects all matching runs of three or more identical balls on the board.
        /// </summary>
        /// <param name="board">The board to inspect.</param>
        /// <returns>A collection of matched balls.</returns>
        public IReadOnlyList<Ball> DetectMatches(BoardManager board)
        {
            ArgumentNullException.ThrowIfNull(board);

            var matches = new List<Ball>();
            var matchedBalls = new HashSet<Ball>();

            if (board.Balls == null)
            {
                return matches;
            }

            for (var row = 0; row < BoardManager.Height; row++)
            {
                for (var column = 0; column < BoardManager.Width; column++)
                {
                    var currentBall = board.Balls[row, column];
                    if (currentBall == null)
                    {
                        continue;
                    }

                    AddMatchesForDirection(board, row, column, 0, 1, currentBall, matchedBalls, matches);
                    AddMatchesForDirection(board, row, column, 1, 0, currentBall, matchedBalls, matches);
                }
            }

            return matches;
        }

        /// <summary>
        /// Adds all balls in a matching run in the specified direction.
        /// </summary>
        /// <param name="board">The board to inspect.</param>
        /// <param name="row">The starting row index.</param>
        /// <param name="column">The starting column index.</param>
        /// <param name="rowStep">The row increment for the scan direction.</param>
        /// <param name="columnStep">The column increment for the scan direction.</param>
        /// <param name="startBall">The ball used as the comparison anchor.</param>
        /// <param name="matchedBalls">A set used to avoid duplicate matches.</param>
        /// <param name="matches">The collected list of matched balls.</param>
        private static void AddMatchesForDirection(
            BoardManager board,
            int row,
            int column,
            int rowStep,
            int columnStep,
            Ball startBall,
            HashSet<Ball> matchedBalls,
            List<Ball> matches)
        {
            var run = new List<Ball> { startBall };
            var nextRow = row + rowStep;
            var nextColumn = column + columnStep;

            while (nextRow >= 0 && nextRow < BoardManager.Height && nextColumn >= 0 && nextColumn < BoardManager.Width)
            {
                var nextBall = board.Balls[nextRow, nextColumn];
                if (nextBall != null && nextBall.Type == startBall.Type)
                {
                    run.Add(nextBall);
                    nextRow += rowStep;
                    nextColumn += columnStep;
                }
                else
                {
                    break;
                }
            }

            if (run.Count >= 3)
            {
                foreach (var match in run)
                {
                    if (matchedBalls.Add(match))
                    {
                        matches.Add(match);
                    }
                }
            }
        }
    }
}
