namespace TetrisGame
{
    /// <summary>
    /// Represents a Tetris figure, including its shape, type, and operations like rotation.
    /// </summary>
    /// <param name="Shape">The 2D array defining the shape of the figure.</param>
    /// <param name="Type">The type of the figure, as a <see cref="ShapeType"/>.</param>
    public record TetrisFigure(int[,] Shape, ShapeType Type)
    {
        /// <summary>
        /// Gets the width of the figure.
        /// </summary>
        public int Width => Shape.GetLength(1);

        /// <summary>
        /// Gets the height of the figure.
        /// </summary>
        public int Height => Shape.GetLength(0);

        /// <summary>
        /// Rotates the figure clockwise.
        /// </summary>
        /// <returns>A new <see cref="TetrisFigure"/> representing the rotated shape.</returns>
        public TetrisFigure Rotate()
        {
            int newHeight = Width;
            int newWidth = Height;
            var rotatedShape = new int[newHeight, newWidth];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    rotatedShape[j, newWidth - i - 1] = Shape[i, j];
                }
            }

            return new TetrisFigure(rotatedShape, Type);
        }

        /// <summary>
        /// Returns a string representation of the figure's shape.
        /// </summary>
        /// <returns>A string displaying the figure's shape using symbols.</returns>
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    sb.Append(Shape[i, j] == 1 ? "🟦" : "⬜");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}