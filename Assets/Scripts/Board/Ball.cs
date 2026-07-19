namespace OpusMatch.Board
{
    /// <summary>
    /// Represents a single game ball placed on a board tile.
    /// </summary>
    public sealed class Ball
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ball"/> class.
        /// </summary>
        /// <param name="type">The ball type.</param>
        /// <param name="row">The row position of the ball.</param>
        /// <param name="column">The column position of the ball.</param>
        public Ball(BallType type, int row, int column)
        {
            Type = type;
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Gets or sets the row position of the ball.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column position of the ball.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Gets or sets the ball type.
        /// </summary>
        public BallType Type { get; set; }
    }
}
