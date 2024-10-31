using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace V._44Hemuppgift
{
    internal class MenuHelper
    {
        public static void ShowMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMeny:");
                Console.WriteLine("1: Registrera ny student");
                Console.WriteLine("2: Ändra en student");
                Console.WriteLine("3: Lista alla studenter");
                Console.WriteLine("4: Avsluta program");
                Console.Write("Välj ett alternativ: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        UpdateStudent();
                        break;
                    case "3":
                        ListAllStudents();
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("Avslutar program...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        } 

        private static void AddStudent()
        {   // Instans av databas.
            using (var db = new StudentDBContext())
            {
                Console.Write("Förnamn: ");
                string firstName = Console.ReadLine();

                Console.Write("Efternamn: ");
                string lastName = Console.ReadLine();

                Console.Write("Stad: ");
                string city = Console.ReadLine();

                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    City = city
                };

                db.Students.Add(student);
                db.SaveChanges();
                Console.WriteLine("Student registrerad!");
            }
        }

        private static void UpdateStudent()
        {
            using (var db = new StudentDBContext())
            {
                Console.Write("Ange StudentId för den student du vill ändra: ");
                if (int.TryParse(Console.ReadLine(), out int studentId))
                {
                    var student = db.Students.Find(studentId);
                    if (student != null)
                    {
                        Console.Write("Nytt förnamn (lämna tomt för att behålla): ");
                        string newFirstName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newFirstName))
                        {
                            student.FirstName = newFirstName;
                        }

                        Console.Write("Nytt efternamn (lämna tomt för att behålla): ");
                        string newLastName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newLastName))
                        {
                            student.LastName = newLastName;
                        }

                        Console.Write("Ny stad (lämna tomt för att behålla): ");
                        string newCity = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCity))
                        {
                            student.City = newCity;
                        }

                        db.SaveChanges();
                        Console.WriteLine("Student uppdaterad!");
                    }
                    else
                    {
                        Console.WriteLine("Student med angivet ID hittades inte.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt ID.");
                }
            }
        }

        private static void ListAllStudents()
        {
            using (var db = new StudentDBContext())
            {
                var students = db.Students.ToList();
                if (students.Count == 0)
                {
                    Console.WriteLine("Inga studenter registrerade.");
                }
                else
                {
                    Console.WriteLine("Alla registrerade studenter:");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.StudentId}, Namn: {student.FirstName} {student.LastName}, Stad: {student.City}");
                    }
                }
            }
        }

    }
}
