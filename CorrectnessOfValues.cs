namespace TetrisGame
{
    /// <summary>
    /// Предоставляет методы для проверки корректности ввода значений и работы с массивами.
    /// </summary>
    static class CorrectnessOfValue
    {
        /// <summary>
        /// Считывает целое положительное число от пользователя.
        /// </summary>
        /// <returns>Положительное целое число, введенное пользователем.</returns>
        public static int ReadInt()
        {
            int x;
            string y = Console.ReadLine();
            while (!(int.TryParse(y, out x)) || x <= 0)
            {
                Console.WriteLine("Ошибка! Введите правильное значение");
                y = Console.ReadLine();
            }
            return x;
        }

        /// <summary>
        /// Выводит содержимое массива целых чисел в консоль.
        /// </summary>
        /// <param name="array">Массив целых чисел для отображения.</param>
        public static void PrintArray(int[] array)
        {
            if (array.Length <= 10)
            {
                Console.WriteLine(string.Join(", ", array));
            }
            else
            {
                Console.WriteLine("Массив слишком длинный для отображения. (не шмогла :( )");
            }
        }

        /// <summary>
        /// Подтверждает, хочет ли пользователь выйти из программы.
        /// </summary>
        /// <returns>Возвращает <c>true</c>, если пользователь решает остаться в программе; <c>false</c>, если хочет выйти.</returns>
        public static bool ConfirmExit()
        {
            Console.WriteLine("Вы точно хотите выйти? [д/н]");
            string input;
            bool a = true;
            bool b = true;
            do
            {
                input = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (input == "д")
                {
                    Console.WriteLine("Программа завершена. До свидания!");
                    a = false;
                }
                else if (input == "н")
                {
                    Console.WriteLine("Возвращаемся в меню...");
                    a = false;
                    b = true;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода. Введите 'д' для выхода или 'н' для возврата.");
                    a = true;
                }
            } while (a);
            return b; 
        }
    }
}