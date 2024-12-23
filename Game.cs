namespace TetrisGame
{

    /// <summary>
    /// Represents the Tetris game controller.
    /// Manages the game flow, including spawning figures, rendering the field, and processing user inputs.
    /// </summary>
    public class Game
    {
        private readonly GameField _gameField;
        private readonly Random _random;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            _gameField = new GameField(10, 10);
            _random = new Random();
        }

        /// <summary>
        /// Starts the Tetris game.
        /// Displays game instructions, handles figure spawning, user input, and game termination.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Welcome to Tetris!");

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
                            Console.WriteLine("Invalid input!");
                        }
                    }
                }
                Console.WriteLine("Game Over");
                Console.WriteLine($"Final Score: {_gameField.Score}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Game Over");
                Console.WriteLine($"Final Score: {_gameField.Score}");
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
    /// A factory class for creating Tetris figures.
    /// Provides methods to create predefined or random figures.
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
        /// Creates a Tetris figure of the specified type.
        /// </summary>
        /// <param name="type">The type of figure to create.</param>
        /// <returns>The created <see cref="TetrisFigure"/>.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified figure type is not recognized.
        /// </exception>
        public static TetrisFigure CreateFigure(ShapeType type)
        {
            Console.WriteLine($"Creating figure '{type}'");
            if (CanonicalFigures.TryGetValue(type, out var shape))
            {
                return new TetrisFigure(shape, type);
            }

            throw new ArgumentException($"Figure '{type}' is not recognized.", nameof(type));
        }


        /// <summary>
        /// Creates a random Tetris figure.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>A randomly generated <see cref="TetrisFigure"/>.</returns>
        public static TetrisFigure CreateRandomFigure(Random random)
        {
            var figureTypes = Enum.GetValues<ShapeType>();
            ShapeType randomType = figureTypes[random.Next(figureTypes.Length)];
            return CreateFigure(randomType);
        }
    }
}