using System;
using OpusMatch.Board;
using OpusMatch.Gameplay;

namespace OpusMatch.Gameplay
{
    /// <summary>
    /// Validates whether a proposed ball swap creates a match.
    /// </summary>
    public sealed class MoveValidator
    {
        private readonly MatchDetector matchDetector = new();

        /// <summary>
        /// Determines whether a swap between two balls is valid.
        /// </summary>
        /// <param name="board">The current board.</param>
        /// <param name="first">The first ball in the proposed swap.</param>
        /// <param name="second">The second ball in the proposed swap.</param>
        /// <returns><c>true</c> when the swap creates a match; otherwise <c>false</c>.</returns>
        public bool IsValidMove(BoardManager board, Ball first, Ball second)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            if (first == second)
            {
                return false;
            }

            var rowDelta = Math.Abs(first.Row - second.Row);
            var columnDelta = Math.Abs(first.Column - second.Column);
            var isAdjacent = (rowDelta == 1 && columnDelta == 0) || (rowDelta == 0 && columnDelta == 1);

            if (!isAdjacent)
            {
                return false;
            }

            board.SwapBalls(first, second);
            var matchedBalls = matchDetector.DetectMatches(board);

            if (matchedBalls.Count == 0)
            {
                board.SwapBalls(first, second);
                return false;
            }

            return true;
        }
    }
}
