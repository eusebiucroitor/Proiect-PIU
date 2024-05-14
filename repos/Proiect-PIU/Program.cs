using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace ConsoleApp
{
    internal class Program
    {
        private static string name;

        static void Main()
        {
            ClassAgenda test = new ClassAgenda();
            Agenda name = new Agenda(); 
            string option = "";
            while (!option.Equals("X"))
            {
                Console.WriteLine("A. Lista activitati");
                Console.WriteLine("B. Adauga activitati ");
                Console.WriteLine("C. Sterge activitati");
                Console.WriteLine("U. Modifica activitati");
                Console.WriteLine("F. Gaseste dupa nume");
                Console.WriteLine("I. Gaseste dupa index");
                Console.WriteLine("L. Incarca din fisier date");
                Console.WriteLine("W. Scrie datele in fisier");
                Console.WriteLine("X. Exit");

                option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "A": 
                        Console.Clear();
                        displayActivitate(test.getActivitate);
                        break;
                    case "B": 
                        Console.Write("Introduceti numele activitatii: ");
                        test.addActivitate();
                        Console.WriteLine("Activitatea a fost adaugat");
                        break;
                    case "C":
                        Console.Write("Introduceti ce activitate doriti sa eliminati: ");
                        test.removeActivitate();
                        Console.WriteLine("Activitatea a fost stearsa");
                        break;
                    case "F":
                        Console.Write("Introduceti numele activitatii cautate: ");
                        displayActivitate(test.findByName());
                        break;
                    case "I":
                        Console.Write("Introduceti indexul activitatii cautat: ");
                        Console.WriteLine(test.GetActivitate().getName);
                        break;
                    case "W": 
                        test.writeDataInFile();
                        Console.WriteLine("Datele au fost scrise in fisier");
                        break;
                    case "L": // Citeste date din fisier
                        test.readDataFromFile();
                        Console.WriteLine("Datele au fost citite din fisier");
                        break;
                    case "R": 
                        test.readDataFromFile();
                        Console.WriteLine("Datele au fost citite din fisier");
                        break;
                    case "X":
                        Console.WriteLine("Terminare Program");
                        break;
                    default:
                        Console.WriteLine("Optiune gresita");
                        break;
                }
            }
        }

       
        static void displayActivitate(List<Activitate> a)
        {
            if (a.Count == 0)
            {
                Console.WriteLine("Nu exista activitati");
                return;
            }
            int count = 0;
            foreach (Activitate agenda in a)
            {
                count++;
                Console.WriteLine(count + ". " + agenda.getName);
            }
        }
    }
}