
using System.Collections.Generic;
using System.IO;


namespace Dz_Tumakov_Methods
{
    class Methods
    { 
        public static bool IsVowel(char c)
        {
            c = char.ToLower(c);
            return "aeiouyаеёиоуыэюя".Contains(c);
        }

        public static bool IsConsonant(char c)
        {
            c = char.ToLower(c);
            return (c > 'a' && c <= 'z' || c > 'а' && c < 'я') && !IsVowel(c);
        }

        public static void CountVowelsAndConsonants(char[] chars)
        {
            int vowels = 0, consonants = 0;
            foreach (char c in chars)
            {
                if (IsVowel(c))
                    vowels++;
                else if (IsConsonant(c))
                    consonants++;
            }
            Console.WriteLine($"[Массив] Гласных: {vowels}, Согласных: {consonants}");
        }

        public static void CountVowelsAndConsonants(List<char> chars)
        {
            int vowels = 0, consonants = 0;
            foreach (char c in chars)
            {
                if (IsVowel(c))
                    vowels++;
                else if (IsConsonant(c))
                    consonants++;
            }
            Console.WriteLine($"[List] Гласных: {vowels}, Согласных: {consonants}");
        }

        public static void Exercise_1(string[] args)
        {
            try
            {


                string filename = "";
                if (args.Length == 0)
                {
                    Console.WriteLine("Введите имя файла:");
                    filename = Console.ReadLine();
                }
                else
                {
                    filename = args[0];
                }

                if (!File.Exists(filename))
                {
                    Console.WriteLine("Файл не найден: " + filename);
                    Console.WriteLine("Нажмите Enter для выхода...");
                    Console.ReadLine();
                    return;
                }

                // Вариант 1: читать в массив символов
                char[] chars = File.ReadAllText(filename).ToCharArray();
                CountVowelsAndConsonants(chars);

                // Вариант 2: читать в List<char>
                List<char> charList = new List<char>(chars);
                CountVowelsAndConsonants(charList);

                Console.ReadLine();
            }
            catch (Exception ex)
            { Console.WriteLine($"Ошибка : {ex.Message}"); }
        }




        //2


        // Метод для печати двумерного массива
        static void MatrixPrint(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // Метод умножения матриц
        static int[,] MatrixMultiply(int[,] A, int[,] B)
        {
            int rowA = A.GetLength(0);
            int colA = A.GetLength(1);
            int rowB = B.GetLength(0);
            int colB = B.GetLength(1);

            if (colA != rowB)
                throw new ArgumentException("Количество столбцов первой матрицы должно быть равно количеству строк второй.");

            int[,] result = new int[rowA, colB];

            for (int i = 0; i < rowA; i++)
            {
                for (int j = 0; j < colB; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < colA; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }

        // Конвертация двумерного массива в LinkedList
        static LinkedList<LinkedList<int>> ConvertToLinkedList(int[,] matrix)
        {
            var result = new LinkedList<LinkedList<int>>();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                var rowList = new LinkedList<int>();
                for (int j = 0; j < cols; j++)
                {
                    rowList.AddLast(matrix[i, j]);
                }
                result.AddLast(rowList);
            }
            return result;
        }

        // Получить элемент из LinkedList по индексу
        static int GetElementAt(LinkedList<int> list, int index)
        {
            var node = list.First;
            for (int i = 0; i < index; i++)
            {
                node = node.Next;
            }
            return node.Value;
        }

        // Получить узел списка по индексу
        static LinkedListNode<LinkedList<int>> GetNodeAt(LinkedList<LinkedList<int>> list, int index)
        {
            var node = list.First;
            for (int i = 0; i < index; i++)
            {
                node = node.Next;
            }
            return node;
        }

        // Метод умножения матриц, представленных как LinkedList
        static LinkedList<LinkedList<int>> MultiplyLinkedLists(LinkedList<LinkedList<int>> A, LinkedList<LinkedList<int>> B)
        {
            int rowA = A.Count;
            int colA = A.First.Value.Count;
            int rowB = B.Count;
            int colB = B.First.Value.Count;

            if (colA != rowB)
                throw new ArgumentException("Несовместимые размеры матриц.");

            var result = new LinkedList<LinkedList<int>>();

            var rowA_1 = A.First;
            for (int i = 0; i < rowA; i++)
            {
                var rowResult = new LinkedList<int>();
                for (int j = 0; j < colB; j++)
                {
                    int sum = 0;
                    var colIndex = j;
                    for (int k = 0; k < colA; k++)
                    {
                        int valA = GetElementAt(rowA_1.Value, k);
                        int valB = GetElementAt(GetNodeAt(B, k).Value, colIndex);
                        sum += valA * valB;
                    }
                    rowResult.AddLast(sum);
                }
                result.AddLast(rowResult);
                rowA_1 = rowA_1.Next;
            }

            return result;
        }


        // Печать LinkedList<LinkedList<int>>
        static void PrintLinkedListMatrix(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var val in row)
                {
                    Console.Write(val + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void Exercise_2()
        {
            int[,] matrixA =
            {
            {1, 2, 3},
            {4, 5, 6}
            };
            int[,] matrixB =
            {
            {7, 8},
            {9, 10},
            {11, 12}
            };

            Console.WriteLine("Матрица A:");
            MatrixPrint(matrixA);

            Console.WriteLine("Матрица B:");
            MatrixPrint(matrixB);

            int[,] result = MatrixMultiply(matrixA, matrixB);
            Console.WriteLine("Произведение A * B:");
            MatrixPrint(result);

            var listA = ConvertToLinkedList(matrixA);
            var listB = ConvertToLinkedList(matrixB);
            var listResult = MultiplyLinkedLists(listA, listB);
            Console.WriteLine("Результат умножения List-ов:");
            PrintLinkedListMatrix(listResult);
        }

        //3
        
        

        public static void Exercise_3()
        {
            int months = 12;
            int days = 30;
            Random rnd = new Random();

            
            int[,] temperature = new int[months, days];
            for (int i = 0; i < months; i++)
                for (int j = 0; j < days; j++)
                    temperature[i, j] = rnd.Next(-20, 41); 

            
            string[] monthNames = { "Январь", "Февраль", "Март", "Апрель",
                                "Май", "Июнь", "Июль", "Август",
                                "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            Dictionary<string, int[]> tempDict = new Dictionary<string, int[]>();
            for (int i = 0; i < months; i++)
            {
                int[] arr = new int[days];
                for (int j = 0; j < days; j++)
                    arr[j] = rnd.Next(-20, 41);
                tempDict[monthNames[i]] = arr;
            }

            // функция для подсчета средних по двумерному массиву
            double[] MonthlyAveragesArray(int[,] temps)
            {
                int m = temps.GetLength(0);
                int d = temps.GetLength(1);
                double[] avgs = new double[m];
                for (int i = 0; i < m; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < d; j++)
                        sum += temps[i, j];
                    avgs[i] = sum / d;
                }
                return avgs;
            }

            /// функция для подсчета средних по словарю
            Dictionary<string, double> MonthlyAveragesDict(Dictionary<string, int[]> tempDictLocal)
            {
                var avgs = new Dictionary<string, double>();
                foreach (var kvp in tempDictLocal)
                {
                    var temps = kvp.Value;
                    double sum = 0;
                    foreach (var t in temps)
                        sum += t;
                    avgs[kvp.Key] = sum / temps.Length;
                }
                return avgs;
            }

            
            double[] averages = MonthlyAveragesArray(temperature);
            Console.WriteLine("Средняя температура по месяцам (массив):");
            foreach (var avg in averages)
                Console.WriteLine($"{avg:F2}");

            Array.Sort(averages);
            Console.WriteLine("Отсортированные средние температуры (массив):");
            foreach (var avg in averages)
                Console.WriteLine($"{avg:F2}");

           
            var averagesDict = MonthlyAveragesDict(tempDict);
            Console.WriteLine("\nСредняя температура по месяцам (Dictionary):");
            foreach (var kvp in averagesDict)
                Console.WriteLine($"{kvp.Key}: {kvp.Value:F2}");

            var sortedList = new List<KeyValuePair<string, double>>(averagesDict);
            sortedList.Sort((a, b) => a.Value.CompareTo(b.Value));

            Console.WriteLine("\nОтсортированные средние температуры (Dictionary):");
            foreach (var kvp in sortedList)
                Console.WriteLine($"{kvp.Key}: {kvp.Value:F2}");

            Console.ReadLine();
        }


    } 
}
