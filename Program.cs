using System;

namespace TetrisGame 
{
    /// <summary>
    /// Точка входа в программу
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
                        // Запрос длины массива у пользователя
                        Console.Write("Введите количество элементов массива: ");
                        int userInputLength;
                        if (int.TryParse(Console.ReadLine(), out userInputLength) && userInputLength > 0)
                        {
                            // Создаем экземпляр класса с параметрами
                            ArraySorting customArraySort = new ArraySorting(userInputLength); // конструктор с параметрами
                            customArraySort.PerformArraySorting();
                            // Создаем экземпляр класса без параметров и выполняем сортировку
                            ArraySorting defaultArraySort = new ArraySorting();
                            defaultArraySort.PerformArraySorting();
                        }
                        else
                        {
                            Console.WriteLine("Ошибка! Пожалуйста, введите корректное число.");
                        }
                        break;
                    case "4":
                        CorrectnessOfValue.ConfirmExit();
                        isExiting = true; // Устанавливаем флаг выхода
                        break;
                    case "5":
                        Game game = new Game();
                        game.Start();
                        break;
                    default:
                        Console.WriteLine("Ошибка! Введите число от 1 до 5.");
                        break;
                }
            }

            
        }
    }
}