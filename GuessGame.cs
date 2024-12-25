using System.Diagnostics.Contracts;

namespace TetrisGame
{
    /// <summary>
    /// Представляет игру "Угадай результат".
    /// </summary>
    static public class GuessingGame
    {
        /// <summary>
        /// Запускает игру "Угадай результат".
        /// </summary>
        public static void PlayGuessingGame()
        {
            Console.WriteLine("--- Угадай результат ---");
            double numberA = ReadDoubleInput("Введите число A:");
            double numberB = ReadDoubleInput("Введите число B:");
            double correctAnswer = ComputeAnswer(numberA, numberB);

            TestUserGuess(correctAnswer);
        }

        /// <summary>
        /// Читает ввод пользователя и преобразует его в число с плавающей точкой.
        /// </summary>
        /// <param name="prompt">Сообщение, которое будет показано пользователю.</param>
        /// <returns>Число с плавающей точкой, введенное пользователем.</returns>
        static double ReadDoubleInput(string prompt = "Введите число (с плавающей точкой):")
        {
            Console.WriteLine(prompt);
            double result = 0.0;
            bool valid = false;

            while (!valid)
            {
                string input = Console.ReadLine()?.Trim() ?? "";

                try
                {
                    result = double.Parse(input);
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Введите число:");
                }
            }

            return result;
        }

        /// <summary>
        /// Вычисляет правильный ответ на основе двух чисел.
        /// </summary>
        /// <param name="a">Первое число.</param>
        /// <param name="b">Второе число.</param>
        /// <returns>Вычисленный ответ.</returns>
        static double ComputeAnswer(double a, double b)
        {
            return Math.PI * 5 * Math.Log(b) / (Math.Sin(a) + 1);
        }

        /// <summary>
        /// Проверяет догадки пользователя относительно правильного ответа.
        /// </summary>
        /// <param name="correctAnswer">Правильный ответ, который нужно угадать.</param>
        static void TestUserGuess(double correctAnswer)
{
    const int maxAttempts = 3;
    Console.WriteLine($"Угадайте результат вычисления. У вас есть {maxAttempts} попытки.");

    for (int attempt = 1; attempt <= maxAttempts; attempt++)
    {
        double userGuess = ReadDoubleInput("Введите ваш ответ:");

        if (Math.Abs(userGuess - correctAnswer) < 0.0001)
        {
            Console.WriteLine($"Поздравляем! Вы угадали правильный ответ: {correctAnswer}");
            return; // Завершаем метод, если пользователь угадал
        }
        else
        {
            Console.WriteLine($"Неверно. Осталось попыток: {maxAttempts - attempt}");
        }
    }

    // Если пользователь не угадал за все попытки, выводим правильный ответ
    Console.WriteLine($"Вы проиграли! Правильный ответ: {correctAnswer}");
}
    }
}