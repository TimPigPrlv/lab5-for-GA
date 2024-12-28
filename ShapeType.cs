namespace TetrisGame
{
    
    /// <summary>
    /// Представляет различные формы фигур Тетриса.
    /// </summary>
    public enum ShapeType//Перечисления  используются для создания набора именованных целочисленных констант, 
                         // которые могут представлять различные значения, связанные с одной темой
    {
        /// <summary>
        /// Фигура в форме квадрата.
        /// </summary>
        Square,

        /// <summary>
        /// Фигура в форме прямой линии.
        /// </summary>
        Line,

        /// <summary>
        /// Фигура в форме L.
        /// </summary>
        L,

        /// <summary>
        /// Фигура в форме обратной L.
        /// </summary>
        J,

        /// <summary>
        /// Фигура в форме Z.
        /// </summary>
        Z,

        /// <summary>
        /// Фигура в форме обратной Z.
        /// </summary>
        S,

        /// <summary>
        /// Фигура в форме T.
        /// </summary>
        T
    }
}