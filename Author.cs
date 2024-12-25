using System.Diagnostics.Contracts;

namespace TetrisGame

{
    /// <summary>
    /// Представляет информацию об авторе 
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Ввыводит информацию об авторе в консоль 
        /// </summary>
       public static void ShowAuthorInfo()
    {
        Console.WriteLine("--- Информация об авторе ---");
        Console.WriteLine("Автор: Райан Гослинг из фильма Драйв(я не умер)");
        Console.WriteLine("Студент группы 6101-090301D");
    }
    }
}