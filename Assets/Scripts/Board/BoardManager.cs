using System;

namespace OpusMatch.Board
{
    /// <summary>
    /// Manages the match-three board state, tile grid, and ball placement.
    /// </summary>
    public sealed class BoardManager
    {
        /// <summary>
        /// Gets the width of the board in tiles.
        /// </summary>
        public const int Width = 8;

        /// <summary>
        /// Gets the height of the board in tiles.
        /// </summary>
        public const int Height = 8;

        /// <summary>
        /// Gets the tile grid representing the current board.
        /// </summary>
        public Tile[,] Tiles { get; private set; } = new Tile[Width, Height];

        /// <summary>
        /// Gets the ball grid representing the current board contents.
        /// </summary>
        public Ball[,] Balls { get; private set; } = new Ball[Width, Height];

        /// <summary>
        /// Initializes the board with an 8x8 grid of randomly assigned balls.
        /// </summary>
        public void InitializeBoard()
        {
            Tiles = new Tile[Width, Height];
            Balls = new Ball[Width, Height];
            var random = new Random();
            var ballTypeValues = Enum.GetValues(typeof(BallType));

            for (var row = 0; row < Height; row++)
            {
                for (var column = 0; column < Width; column++)
                {
                    var tile = new Tile(row, column);
                    var ballType = (BallType)ballTypeValues.GetValue(random.Next(ballTypeValues.Length))!;
                    var ball = new Ball(ballType, row, column);

                    tile.Ball = ball;
                    Tiles[row, column] = tile;
                    Balls[row, column] = ball;
                }
            }

            EnsureNoNullEntries();
        }

        /// <summary>
        /// Swaps the positions of two balls on the board.
        /// </summary>
        /// <param name="first">The first ball to swap.</param>
        /// <param name="second">The second ball to swap.</param>
        public void SwapBalls(Ball first, Ball second)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            var firstRow = first.Row;
            var firstColumn = first.Column;
            var secondRow = second.Row;
            var secondColumn = second.Column;

            if (firstRow == secondRow && firstColumn == secondColumn)
            {
                return;
            }

            var firstTile = Tiles[firstRow, firstColumn] ?? new Tile(firstRow, firstColumn);
            var secondTile = Tiles[secondRow, secondColumn] ?? new Tile(secondRow, secondColumn);

            Tiles[firstRow, firstColumn] = secondTile;
            Tiles[secondRow, secondColumn] = firstTile;
            Balls[firstRow, firstColumn] = second;
            Balls[secondRow, secondColumn] = first;

            firstTile.Ball = second;
            secondTile.Ball = first;

            first.Row = secondRow;
            first.Column = secondColumn;
            second.Row = firstRow;
            second.Column = firstColumn;
        }

        /// <summary>
        /// Ensures every board position contains a tile and a ball.
        /// </summary>
        private void EnsureNoNullEntries()
        {
            var ballTypeValues = Enum.GetValues(typeof(BallType));

            for (var row = 0; row < Height; row++)
            {
                for (var column = 0; column < Width; column++)
                {
                    if (Tiles[row, column] == null)
                    {
                        Tiles[row, column] = new Tile(row, column);
                    }

                    if (Balls[row, column] == null)
                    {
                        var fallbackType = (BallType)((row + column) % ballTypeValues.Length);
                        var fallbackBall = new Ball(fallbackType, row, column);
                        Balls[row, column] = fallbackBall;
                        Tiles[row, column].Ball = fallbackBall;
                    }
                    else
                    {
                        Tiles[row, column].Ball ??= Balls[row, column];
                    }
                }
            }
        }
    }
}
