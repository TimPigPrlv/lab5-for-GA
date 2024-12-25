namespace TetrisGame 

{
    /// <summary>
/// Предоставляет методы для валидации и обработки ввода пользователя для управления игрой Тетрис.
/// </summary>
public static class InputValidator
{
    /// <summary>
    /// Проверяет, соответствует ли ввод допустимым управляющим командам игры Тетрис.
    /// Допустимые команды: A (Движение влево), D (Движение вправо), S (Движение вниз), W (Поворот) и Space (Быстрое падение).
    /// </summary>
    /// <param name="input">Строка ввода пользователя для проверки.</param>
    /// <returns><see langword="true"/> если ввод является допустимой командой; в противном случае <see langword="false"/>.</returns>
    public static bool ValidateControlInput(string input)
    {
        return input is "A" or "S" or "D" or "W" or " ";
    }

    /// <summary>
    /// Запрашивает пользователя до тех пор, пока не будет предоставлен допустимый ввод управления игрой Тетрис.
    /// </summary>
    /// <returns>Проверенная строка, представляющая допустимую команду.</returns>
    public static string GetValidatedControlInput()
    {
        while (true)
        {
            Console.Write("Введите ваш ход (A/D/S/W/Space): ");
            var key = Console.ReadKey(intercept: true);
            Console.WriteLine();
            
            string input = key.KeyChar.ToString().ToUpper();
            if (key.Key == ConsoleKey.Spacebar)
            {
                input = " ";
            }

            if (ValidateControlInput(input))
            {
                return input;
            }

            Console.WriteLine("Недопустимый ввод. Допустимые вводы: A, D, S, W или Space.");
        }
    }
}
}
