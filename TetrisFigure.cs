namespace TetrisGame
{
    /// <summary>
    /// Представляет фигуру Тетриса, включая её форму, тип и операции, такие как вращение.
    /// </summary>
    /// <param name="Shape">Двумерный массив, определяющий форму фигуры.</param>
    /// <param name="Type">Тип фигуры, как <see cref="ShapeType"/>.</param>
    public record TetrisFigure(int[,] Shape, ShapeType Type)
    {
        /// <summary>
        /// Получает ширину фигуры.
        /// </summary>
        public int Width => Shape.GetLength(1);

        /// <summary>
        /// Получает высоту фигуры.
        /// </summary>
        public int Height => Shape.GetLength(0);

        /// <summary>
        /// Вращает фигуру по часовой стрелке.
        /// </summary>
        /// <returns>Новая <see cref="TetrisFigure"/>, представляющая вращённую форму.</returns>
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
        /// Возвращает строковое представление формы фигуры.
        /// </summary>
        /// <returns>Строка, отображающая форму фигуры с использованием символов.</returns>
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