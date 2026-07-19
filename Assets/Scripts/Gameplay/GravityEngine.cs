using System;
using OpusMatch.Board;

namespace OpusMatch.Gameplay
{
    /// <summary>
    /// Applies gravity rules to the board after matches are resolved.
    /// </summary>
    public sealed class GravityEngine
    {
        /// <summary>
        /// Applies a placeholder gravity pass across the board.
        /// </summary>
        /// <param name="board">The board to update.</param>
        public void ApplyGravity(BoardManager board)
        {
            ArgumentNullException.ThrowIfNull(board);
        }
    }
}
