using System.Collections.Generic;
using OpusMatch.Board;

namespace OpusMatch.Gameplay
{
    /// <summary>
    /// Tracks the player's score during gameplay.
    /// </summary>
    public sealed class ScoreManager
    {
        /// <summary>
        /// Gets the current total score.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Adds the specified number of points to the score.
        /// </summary>
        /// <param name="points">The number of points to add.</param>
        public void AddScore(int points)
        {
            if (points < 0)
            {
                return;
            }

            Score += points;
        }

        /// <summary>
        /// Adds score for a collection of matched balls.
        /// </summary>
        /// <param name="matchedBalls">The matched balls to score.</param>
        public void AddScoreForMatches(IReadOnlyCollection<Ball> matchedBalls)
        {
            if (matchedBalls == null || matchedBalls.Count == 0)
            {
                return;
            }

            AddScore(matchedBalls.Count * 10);
        }
    }
}
