namespace TetrisGame
{
/// <summary>
/// Представляет игровое поле Тетриса, включая логику движения фигур,
/// обнаружения коллизий и подсчета очков.
/// </summary>
public class GameField
{
    private readonly int[,] _matrix;
    private readonly int _rows;
    private readonly int _columns;
    private TetrisFigure? _activeFigure;
    private Position? _activePosition;

    /// <summary>
    /// Получает текущее количество очков в игре.
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GameField"/>.
    /// </summary>
    /// <param name="rows">Количество строк в игровом поле.</param>
    /// <param name="columns">Количество столбцов в игровом поле.</param>
    public GameField(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        _matrix = new int[rows, columns];
    }

    /// <summary>
    /// Определяет, есть ли место для появления новой фигуры Тетриса.
    /// </summary>
    /// <returns><see langword="true"/> если есть место; в противном случае <see langword="false"/>.</returns>
    public bool HasSpaceForNewFigure()
    {
        for (int col = 0; col < _columns; col++)
        {
            if (_matrix[0, col] != 0)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Появляется новая фигура Тетриса в верхней части игрового поля.
    /// </summary>
    /// <param name="figure">Фигура Тетриса для появления.</param>
    /// <exception cref="InvalidOperationException">Выбрасывается, если нет места для появления фигуры.</exception>
    public void SpawnFigure(TetrisFigure figure)
    {
        Console.WriteLine("Появляется новая фигура");
        _activeFigure = figure;
        _activePosition = new Position(0, (_columns - figure.Width) / 2);

        if (CalculateCollision(_activePosition, figure) != CollistionType.None)
        {
            throw new InvalidOperationException("Игра окончена");
        }
    }

    /// <summary>
    /// Получает значение, указывающее, может ли текущая фигура ещё двигаться.
    /// </summary>
    public bool CanMoveCurrentFigure => _activeFigure != null && _activePosition != null;

    /// <summary>
    /// Обрабатывает движение активной фигуры Тетриса.
    /// </summary>
    /// <param name="direction">Направление, в котором нужно двигать фигуру.</param>
    public void HandleMove(Movement direction)
    {
        if (_activeFigure == null || _activePosition == null)
        {
            return;
        }

        Console.WriteLine($"Текущая позиция: {_activePosition.Row}, {_activePosition.Column}");

        Position newPosition = direction switch
        {
            Movement.Left => _activePosition with { Column = _activePosition.Column - 1 },
            Movement.Right => _activePosition with { Column = _activePosition.Column + 1 },
            Movement.Down => _activePosition with { Row = _activePosition.Row + 1 },
            _ => _activePosition
        };

        Console.WriteLine($"Новая позиция: {newPosition.Row}, {newPosition.Column}");

        if (direction == Movement.Rotate)
        {
            var rotatedFigure = _activeFigure.Rotate();
            if (CalculateCollision(_activePosition, rotatedFigure) == CollistionType.None)
            {
                _activeFigure = rotatedFigure;
            }
            else
            {
                Console.WriteLine("Невозможно повернуть фигуру, она будет перекрывать существующие блоки.");
            }
        }
        else
        {
            if (CalculateCollision(newPosition, _activeFigure) == CollistionType.None)
            {
                _activePosition = newPosition;
            }
            else
            {
                var collision = CalculateCollision(newPosition, _activeFigure);
                if (collision == CollistionType.Left || collision == CollistionType.Right)
                {
                    Console.WriteLine($"Невозможно двигаться {direction}, вместо этого двигаем вниз.");
                    HandleMove(Movement.Down);
                }
                else
                {
                    if (direction == Movement.Down)
                    {
                        Console.WriteLine("Невозможно двигаться вниз, помещаем фигуру на поле.");
                        PlaceFigure(_activePosition, _activeFigure);
                        FixCurrentFigure();
                    }
                }
            }
        }
    }

    private void FixCurrentFigure()
    {
        _activeFigure = null;
        _activePosition = null;
        ClearFullRows();
    }

    private void ClearFullRows()
    {
        for (int row = 0; row < _rows; row++)
        {
            if (IsRowFull(row))
            {
                ClearRow(row);
                DropRowsAbove(row);
                Score += 10;
            }
        }
    }

    private bool IsRowFull(int row)
    {
        for (int col = 0; col < _columns; col++)
        {
            if (_matrix[row, col] == 0)
                return false;
        }
        return true;
    }

    private void ClearRow(int row)
    {
        for (int col = 0; col < _columns; col++)
            _matrix[row, col] = 0;
    }

    private void DropRowsAbove(int row)
    {
        for (int i = row; i > 0; i--)
        {
            for (int col = 0; col < _columns; col++)
            {
                _matrix[i, col] = _matrix[i - 1, col];
            }
        }
        for (int col = 0; col < _columns; col++)
        {
            _matrix[0, col] = 0;
        }
    }

    private CollistionType CalculateCollision(Position position, TetrisFigure figure)
    {
        for (int i = 0; i < figure.Height; i++)
        {
            for (int j = 0; j < figure.Width; j++)
            {
                if (figure.Shape[i, j] == 1)
                {
                    int row = position.Row + i;
                    int col = position.Column + j;
                    if (row < 0)
                    {
                        return CollistionType.Bottom;
                    }
                    if (row >= _rows)
                    {
                        return CollistionType.Block;
                    }
                    if (col < 0)
                    {
                        return CollistionType.Left;
                    }
                    if (col >= _columns)
                    {
                        return CollistionType.Right;
                    }
                    if (_matrix[row, col] == 1)
                    {
                        return CollistionType.Block;
                    }
                }
            }
        }
        return CollistionType.None;
    }

    private enum CollistionType
    {
        None,
        Left,
        Right,
        Bottom,
        Block
    }

    private void PlaceFigure(Position position, TetrisFigure figure)
    {
        for (int i = 0; i < figure.Height; i++)
        {
            for (int j = 0; j < figure.Width; j++)
            {
                if (figure.Shape[i, j] == 1)
                {
                    _matrix[position.Row + i, position.Column + j] = 1;
                }
            }
        }
    }

    private void ClearActiveFigure()
    {
        if (_activeFigure == null || _activePosition == null)
            return;

        for (int i = 0; i < _activeFigure.Height; i++)
        {
            for (int j = 0; j < _activeFigure.Width; j++)
            {
                if (_activeFigure.Shape[i, j] == 1)
                {
                    _matrix[_activePosition.Row + i, _activePosition.Column + j] = 0;
                }
            }
        }
    }

    /// <summary>
    /// Отрисовывает игровое поле и текущую фигуру Тетриса в консоли.
    /// </summary>
    public void Render()
    {
        Console.WriteLine("╔" + new string('═', _columns * 3) + "╗");

        for (int i = 0; i < _rows; i++)
        {
            Console.Write("║");
            for (int j = 0; j < _columns; j++)
            {
                if (_matrix[i, j] == 1)
                {
                    Console.Write("🟦 ");
                }
                else
                {
                    if (_activeFigure != null && _activePosition != null &&
                         IsPartOfActiveFigure(i, j, _activePosition, _activeFigure))
                    {
                        Console.Write("🟦 ");
                    }
                    else
                    {
                        Console.Write("⬜ ");
                    }
                }
            }
            Console.WriteLine("║");
        }

        Console.WriteLine("╚" + new string('═', _columns * 3) + "╝");

        if (_activeFigure != null && _activePosition != null)
        {
            Console.WriteLine($"Текущая фигура: \n{_activeFigure}");
        }
    }
private bool IsPartOfActiveFigure(int row, int col, Position position, TetrisFigure figure)
{
    for (int i = 0; i < figure.Height; i++)
    {
        for (int j = 0; j < figure.Width; j++)
        {
            if (figure.Shape[i, j] == 1 &&
                position.Row + i == row &&
                position.Column + j == col)
            {
                return true;
            }
        }
    }
    return false;
}

/// <summary>
/// Представляет позицию на игровом поле.
/// </summary>
/// <param name="Row">Индекс строки.</param>
/// <param name="Column">Индекс столбца.</param>
public record Position(int Row, int Column); //предназначена для упрощения создания неизменяемых объектов с семантикой значений
}
}
