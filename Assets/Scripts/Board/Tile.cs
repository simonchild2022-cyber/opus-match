namespace OpusMatch.Board
{
    /// <summary>
    /// Represents a single board tile at a specific row and column.
    /// </summary>
    public sealed class Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="row">The tile row.</param>
        /// <param name="column">The tile column.</param>
        public Tile(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Gets the tile row index.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets the tile column index.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Gets or sets the ball currently occupying the tile.
        /// </summary>
        public Ball Ball { get; set; }
    }
}
