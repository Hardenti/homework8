using System;

namespace AttendanceTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Classroom Attendance Tracker!");

            int numStudents;
            while (true)
            {
                Console.Write("Enter the number of students: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out numStudents) && numStudents > 0)
                {
                    break;
                }

                Console.WriteLine("Please enter a positive whole number.");
            }

            string[] studentNames = new string[numStudents];

            for (int i = 0; i < numStudents; i++)
            {
                while (true)
                {
                    Console.Write($"Enter name for student #{i + 1}: ");
                    string? name = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        studentNames[i] = name.Trim();
                        break;
                    }

                    Console.WriteLine("Student name cannot be blank.");
                }
            }

            int[,] attendance = new int[numStudents, 5];

            for (int i = 0; i < numStudents; i++)
            {
                Console.WriteLine($"\nAttendance for {studentNames[i]} (Y/N):");

                for (int day = 0; day < 5; day++)
                {
                    string? input;

                    while (true)
                    {
                        Console.Write($"Day {day + 1}: ");
                        input = Console.ReadLine();

                        if (string.Equals(input?.Trim(), "Y", StringComparison.OrdinalIgnoreCase))
                        {
                            attendance[i, day] = 1;
                            break;
                        }

                        if (string.Equals(input?.Trim(), "N", StringComparison.OrdinalIgnoreCase))
                        {
                            attendance[i, day] = 0;
                            break;
                        }

                        Console.WriteLine("Invalid input. Please enter only Y or N.");
                    }
                }
            }

            Console.WriteLine("\n--- Attendance Summary ---");

            List<string> perfectStudents = new List<string>();
            List<string> lowestStudents = new List<string>();
            int lowestAttendance = 5;

            for (int i = 0; i < numStudents; i++)
            {
                int totalDays = 0;

                for (int day = 0; day < 5; day++)
                {
                    totalDays += attendance[i, day];
                }

                double average = totalDays / 5.0;

                Console.WriteLine($"{studentNames[i]} attended {totalDays}/5 days (Average: {average * 100:F1}%)");
                Console.Write("Attendance chart: ");

                for (int day = 0; day < 5; day++)
                {
                    Console.Write(attendance[i, day] == 1 ? "*" : "-");
                }

                Console.WriteLine();

                if (totalDays == 5)
                {
                    perfectStudents.Add(studentNames[i]);
                    Console.WriteLine(" -> Perfect Attendance!");
                }
                else if (totalDays == 0)
                {
                    Console.WriteLine(" -> Needs Improvement!");
                }

                if (totalDays < lowestAttendance)
                {
                    lowestAttendance = totalDays;
                    lowestStudents = new List<string> { studentNames[i] };
                }
                else if (totalDays == lowestAttendance)
                {
                    lowestStudents.Add(studentNames[i]);
                }
            }

            Console.WriteLine("\nStudents with perfect attendance:");
            if (perfectStudents.Count == 0)
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (string student in perfectStudents)
                {
                    Console.WriteLine($"- {student}");
                }
            }

            Console.WriteLine("\nStudents with the lowest attendance:");
            if (lowestStudents.Count == 0)
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (string student in lowestStudents)
                {
                    Console.WriteLine($"- {student} ({lowestAttendance}/5 days)");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

