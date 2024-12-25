namespace TetrisGame
{
    /// <summary>
    /// Представляет возможные направления движения фигур Тетриса.
    /// </summary>
    public enum Movement
    {
        /// <summary>
        /// Двигать фигуру влево.
        /// </summary>
        Left,

        /// <summary>
        /// Двигать фигуру вправо.
        /// </summary>
        Right,

        /// <summary>
        /// Двигать фигуру вниз.
        /// </summary>
        Down,

        /// <summary>
        /// Поворачивать фигуру по часовой стрелке.
        /// </summary>
        Rotate
    }
}