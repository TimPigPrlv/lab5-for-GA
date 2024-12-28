using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace TetrisGame 
{
    /// <summary>
    /// Класс для сортировки массивов с использованием различных алгоритмов сортировки.
    /// </summary>
    public class ArraySorting 
    {
        private int arrayLength; // Поле для количества элементов в массиве
        private int[] randomArray; // Поле для массива

        /// <summary>
        /// Конструктор без параметров (по умолчанию создает массив из 10 элементов).
        /// </summary>
        public ArraySorting()
        {
            arrayLength = 10;
            randomArray = GenerateRandomArray(arrayLength);
        }

        /// <summary>
        /// Конструктор с параметром для задания длины массива.
        /// </summary>
        /// <param name="length">Количество элементов в массиве.</param>
        public ArraySorting(int length)
        {
            arrayLength = length;
            randomArray = GenerateRandomArray(arrayLength);
        }

        /// <summary>
        /// Выполняет сортировку массива и сравнивает алгоритмы сортировки.
        /// </summary>
        public void PerformArraySorting()
        {
            Console.WriteLine("--- Сортировка массива ---");
            Console.WriteLine("Исходный массив:");
            CorrectnessOfValue.PrintArray(randomArray);

            Console.WriteLine("Сравниваем пузырьковую сортировку и сортировку вставками...");
            CompareSortingAlgorithms();
        }

        /// <summary>
        /// Генерирует массив случайных целых чисел заданной длины.
        /// </summary>
        /// <param name="length">Длина массива.</param>
        private static int[] GenerateRandomArray(int length)
        {
            int[] array = new int[length];
            Random random = new();

            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(-100, 100);
            }

            return array;
        }

        /// <summary>
        /// Копирует массив целых чисел.
        /// </summary>
        /// <param name="a">Массив, который нужно скопировать.</param>
        /// <returns>Копия массива целых чисел.</returns>
        private static int[] CopyArray(int[] a)
        {
            int[] b = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                b[i] = a[i];
            }
            return b;
        }

        /// <summary>
        /// Сравнивает время выполнения алгоритмов сортировки.
        /// </summary>
        private void CompareSortingAlgorithms()
        {
            // Используем поля класса для создания копий массива
            int[] bubbleSortArray = CopyArray(randomArray);
            int[] insertionSortArray = CopyArray(randomArray);

            Console.WriteLine("Пузырьковая сортировка:");
            double bubbleSortTime = MeasureExecutionTime(() => BubbleSort(bubbleSortArray));
            CorrectnessOfValue.PrintArray(bubbleSortArray);

            Console.WriteLine("Сортировка вставками:");
            double insertionSortTime = MeasureExecutionTime(() => InsertionSort(insertionSortArray));
            CorrectnessOfValue.PrintArray(insertionSortArray);

            Console.WriteLine($"Время пузырьковой сортировки: {bubbleSortTime} мс");
            Console.WriteLine($"Время сортировки вставками: {insertionSortTime} мс");

            if (bubbleSortTime < insertionSortTime)
            {
                Console.WriteLine("Пузырьковая сортировка быстрее.");
            }
            else
            {
                Console.WriteLine("Сортировка вставками быстрее.");
            }
        }

        /// <summary>
        /// Измеряет время выполнения функции сортировки.
        /// </summary>
        /// <param name="sortingFunction">Функция сортировки, время выполнения которой нужно измерить.</param>
        /// <returns>Время выполнения в миллисекундах.</returns>
        private static double MeasureExecutionTime(Action sortingFunction)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            sortingFunction();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// Выполняет пузырьковую сортировку массива.
        /// </summary>
        private void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Выполняет сортировку вставками массива.
        /// </summary>
        private void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }
    }
}