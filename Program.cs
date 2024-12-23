using System;

namespace TetrisGame
{
    /// <summary>
    /// Entry point of the program.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
    {
        bool isExiting = false;

        while (!isExiting)
        {
            Console.WriteLine("--- Меню программы ---");
            Console.WriteLine("1. Угадай результат");
            Console.WriteLine("2. Информация об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Выход");
            Console.WriteLine("5. Игра Тетрис");
            Console.Write("Выберите пункт меню: ");

            string menuChoice = Console.ReadLine()?.Trim() ?? "";

            switch (menuChoice)
            {
                case "1":
                    GuessingGame.PlayGuessingGame();
                    break;
                case "2":
                    Author.ShowAuthorInfo();
                    break;
                case "3":
                    var arraySorting = new ArraySorting();
                    ArraySorting.PerformArraySorting(); 
                    break;
                    case "4":
                    CorrectnessOfValue.ConfirmExit();
                    break;
                    case "5":
                    var game = new Game();
                    game.Start();
                    break;

                default:
                    Console.WriteLine("Ошибка! Введите число от 1 до 5.");
                    break;
            }
        }
        // Основной класс программы
    
        // Создаем экземпляр класса с параметрами
        Console.Write("Введите количество элементов массива: ");
        int userInputLength = Convert.ToInt32(Console.ReadLine());
        ArraySorting customArraySort = new ArraySorting(userInputLength);
        customArraySorting.PerformArraySorting();

        // Создаем экземпляр класса без параметров
        ArraySorting defaultArraySort = new ArraySorting();
        defaultArraySorting.PerformArraySorting();

    }
    }
}
