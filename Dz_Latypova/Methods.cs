using GrandMa_Hospital_Struct;
using Dz_Latypova.Data;
using Exams_Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Dz_Latypova_Methods
{
    class Methods
    {

        //1


        /// <summary>
        /// Метод находит файлы jpeg в папке текущего проекта, добавляет их в List и затем перемешивает
        /// </summary>
        public static void Excercise_1()
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"); // Папка с jpeg в проекте
            var imageFiles = Directory.GetFiles(folderPath, "*.jpeg").ToList();

            List<string> images = new List<string>();

            // Добавляем каждое имя файла дважды
            foreach (var file in imageFiles)
            {
                images.Add(Path.GetFileName(file));
                images.Add(Path.GetFileName(file));
            }

            // Вывод исходного списка
            Console.WriteLine("Изначальный список:");
            for (int i = 0; i < images.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {images[i]}");
            }

            // Перемешиваем список случайным образом
            Random rand = new Random();
            var shuffled = images.OrderBy(x => rand.Next()).ToList();

            // Вывод перемешанного списка
            Console.WriteLine("\nПеремешанный список:");
            for (int i = 0; i < shuffled.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {shuffled[i]}");
            }
        }


        //2

        public static List<Student> students = new List<Student>();

        static void LoadStudentsFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Файл не найден");
                return;
            }
            foreach (var line in File.ReadLines(filename))
            {
                var parts = line.Split(';'); // Ожидаемый формат: фамилия;имя;год;экзамен;баллы
                if (parts.Length == 5 &&
                    int.TryParse(parts[2], out int year) &&
                    Enum.TryParse(parts[3], out Exams exam) &&
                    int.TryParse(parts[4], out int score))
                {
                    students.Add(new Student
                    {
                        Surname = parts[0],
                        Name = parts[1],
                        Year = year,
                        Exam = exam,
                        Points = score
                    });
                }
            }
            Console.WriteLine($"Загружено студентов: {students.Count}");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Surname} {student.Name}, Год: {student.Year}, Экзамен: {student.Exam}, Баллы: {student.Points}");
            }
        }

        static void AddStudent()
        {
            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine();
            Console.Write("Имя: ");
            string firstName = Console.ReadLine();
            Console.Write("Год рождения: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Экзамен (Math, Physics, ...): ");
            Exams exam = (Exams)Enum.Parse(typeof(Exams), Console.ReadLine(), true);
            Console.Write("Баллы: ");
            int score = int.Parse(Console.ReadLine());

            students.Add(new Student
            {
                Surname = lastName,
                Name = firstName,
                Year = year,
                Exam = exam,
                Points = score
            });
            Console.WriteLine("\nСтудент добавлен");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Surname} {student.Name}, Год: {student.Year}, Экзамен: {student.Exam}, Баллы: {student.Points}");
            }
        }

        static void RemoveStudent()
        {
            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine();
            Console.Write("Имя: ");
            string firstName = Console.ReadLine();

            var student = students.FirstOrDefault(s => s.Surname == lastName && s.Name == firstName);
            if (student.Equals(default(Student)))
            {
                Console.WriteLine("\nСтудент не найден");
            }
            else
            {
                students.Remove(student);
                Console.WriteLine("\nСтудент удалён");
            }
            foreach (var student1 in students)
            {
                Console.WriteLine($"{student1.Surname} {student1.Name}, Год: {student1.Year}, Экзамен: {student1.Exam}, Баллы: {student1.Points}");
            }


        }

        static void SortStudents()
        {
            students = students.OrderBy(s => s.Points).ToList();
            Console.WriteLine("Студенты отсортированы по баллам");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Surname} {student.Name}, Год: {student.Year}, Экзамен: {student.Exam}, Баллы: {student.Points}");
            }
        }
        public static void Exercise_2()
        {
            // Загрузка студентов из файла
            LoadStudentsFromFile("students.txt");
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\nМеню: \n1. Новый студент\n2. Удалить\n3. Сортировать\n4. Выход");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        RemoveStudent();
                        break;
                    case "3":
                        SortStudents();
                        break;
                    case "4":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;
                }
            }

       

        }

        //3
        static Queue<Grandma> grandmas = new Queue<Grandma>();
        static Stack<Hospital> hospitals = new Stack<Hospital>();

        static void AddGrandma()
        {
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int age = int.Parse(Console.ReadLine());

            Grandma grandma = new Grandma(name, age);

            Console.WriteLine("Введите болезни через запятую (или пусто, если нет): ");
            string diseasesInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(diseasesInput))
            {
                grandma.Diseases = diseasesInput.Split(',').Select(s => s.Trim().ToLower()).ToList();
                grandma.Medicines = new Dictionary<string, string>();
                foreach (var disease in grandma.Diseases)
                {
                    Console.Write($"Введите лекарство от {disease}: ");
                    grandma.Medicines[disease] = Console.ReadLine();
                }
            }
            else
            {
                grandma.Diseases = new List<string>();
                grandma.Medicines = new Dictionary<string, string>();
            }

            grandmas.Enqueue(grandma);
            Console.WriteLine("Бабуля добавлена в очередь.");
        }

        static void ShowGrandmas()
        {
            if (grandmas.Count == 0)
            {
                Console.WriteLine("Очередь бабуль пуста.");
                return;
            }
            Console.WriteLine("Бабули в очереди:");
            foreach (var g in grandmas)
                Console.WriteLine(g);
        }

        static void ShowHospitals()
        {
            if (hospitals.Count == 0)
            {
                Console.WriteLine("Больницы отсутствуют.");
                return;
            }
            Console.WriteLine("Больницы:");
            foreach (var h in hospitals)
                Console.WriteLine(h);
        }

        static void DistributeGrandmas()
        {
            if (grandmas.Count == 0)
            {
                Console.WriteLine("Очередь бабуль пуста.");
                return;
            }

            var remaining = new Queue<Grandma>();

            while (grandmas.Count > 0)
            {
                var grandma = grandmas.Dequeue();
                bool admitted = false;

                foreach (var hospital in hospitals)
                {
                    if (hospital.CanAdmit(grandma))
                    {
                        hospital.Admit(grandma);
                        Console.WriteLine($"Бабуля {grandma.Name} принята в больницу {hospital.Name}.");
                        admitted = true;
                        break;
                    }
                }

                if (!admitted)
                {
                    Console.WriteLine($"Бабуля {grandma.Name} не нашла больницу и осталась на улице плакать.");
                    remaining.Enqueue(grandma);
                }
            }

            grandmas = remaining;
        }

        public static void Exercise_3()
        {
            // Создание больниц
            hospitals.Push(new Hospital("Городская", new List<string> { "грипп", "простуда", "кашель" }, 3));
            hospitals.Push(new Hospital("Областная", new List<string> { "артрит", "грипп", "сердце" }, 5));
            hospitals.Push(new Hospital("Специализированная", new List<string> { "диабет", "грипп" }, 4));

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nМеню:\n1 - Добавить бабулю\n2 - Показать бабуль\n3 - Показать больницы\n4 - Распределить бабуль\n5 - Выход");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGrandma();
                        break;
                    case "2":
                        ShowGrandmas();
                        break;
                    case "3":
                        ShowHospitals();
                        break;
                    case "4":
                        DistributeGrandmas();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }
    }



    }





