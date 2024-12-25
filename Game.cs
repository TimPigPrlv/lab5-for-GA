namespace TetrisGame
{
    /// <summary>
    /// Представляет контроллер игры Тетрис.
    /// Управляет игровым процессом, включая создание фигур, отображение поля и обработку пользовательского ввода.
    /// </summary>
    public class Game
    {
        private readonly GameField _gameField;
        private readonly Random _random;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Game"/>.
        /// </summary>
        public Game()
        {
            _gameField = new GameField(10, 10);
            _random = new Random();
        }

        /// <summary>
        /// Запускает игру Тетрис.
        /// Отображает инструкции по игре, обрабатывает создание фигур, ввод от пользователя и завершение игры.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Добро пожаловать в Тетрис!");

            try
            {
                while (_gameField.HasSpaceForNewFigure())
                {
                    var figure = TetrisFigureFactory.CreateRandomFigure(_random);
                    _gameField.SpawnFigure(figure);
                    _gameField.Render();

                    while (_gameField.CanMoveCurrentFigure)
                    {
                        string input = InputValidator.GetValidatedControlInput();
                        Movement? direction = GetMovement(input);

                        if (direction.HasValue)
                        {
                            _gameField.HandleMove(direction.Value);
                            _gameField.Render();
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод!");
                        }
                    }
                }
                Console.WriteLine("Игра окончена");
                Console.WriteLine($"Итоговый счет: {_gameField.Score}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Игра окончена");
                Console.WriteLine($"Итоговый счет: {_gameField.Score}");
                Console.WriteLine(ex.Message);
            }
        }

        private static Movement? GetMovement(string input)
        {
            return input.ToUpper() switch
            {
                "A" => Movement.Left,
                "D" => Movement.Right,
                "S" => Movement.Down,
                " " => Movement.Down,
                "W" => Movement.Rotate,
                _ => null
            };
        }
    }

    /// <summary>
    /// Фабрика для создания фигур Тетриса.
    /// Предоставляет методы для создания предопределенных или случайных фигур.
    /// </summary>
    public static class TetrisFigureFactory
    {
        private static readonly Dictionary<ShapeType, int[,]> CanonicalFigures = new()
        {
            { ShapeType.Square, new[,] { { 1, 1 }, { 1, 1 } } },
            { ShapeType.Line, new[,] { { 1, 1, 1, 1 } } },
            { ShapeType.L, new[,] { { 1, 1 }, { 1, 0 }, { 1, 0 } } },
            { ShapeType.J, new[,] { { 1, 1 }, { 0, 1 }, { 0, 1 } } },
            { ShapeType.Z, new[,] { { 0, 1, 1 }, { 1, 1, 0 } } },
            { ShapeType.S, new[,] { { 1, 1, 0 }, { 0, 1, 1 } } },
            { ShapeType.T, new[,] { { 1, 1, 1 }, { 0, 1, 0 } } }
        };

        /// <summary>
        /// Создает фигуру Тетриса указанного типа.
        /// </summary>
        /// <param name="type">Тип фигуры для создания.</param>
        /// <returns>Созданная <see cref="TetrisFigure"/>.</returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если указанный тип фигуры не распознан.
        /// </exception>
        public static TetrisFigure CreateFigure(ShapeType type)
        {
            Console.WriteLine($"Создание фигуры '{type}'");
            if (CanonicalFigures.TryGetValue(type, out var shape))
            {
                return new TetrisFigure(shape, type);
            }

            throw new ArgumentException($"Фигура '{type}' не распознана.", nameof(type));
        }

        /// <summary>
        /// Создает случайную фигуру Тетриса.
        /// </summary>
        /// <param name="random">Генератор случайных чисел для использования.</param>
        /// <returns>Случайно сгенерированная <see cref="TetrisFigure"/>.</returns>
        public static TetrisFigure CreateRandomFigure(Random random)
        {
            var figureTypes = Enum.GetValues<ShapeType>();
            ShapeType randomType = figureTypes[random.Next(figureTypes.Length)];
            return CreateFigure(randomType);
        }
    }
}