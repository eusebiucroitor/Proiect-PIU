using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Configuration;

namespace ConsoleApp
{
    public class ClassAgenda
    {
        public List<Activitate> agenda = new List<Activitate>();
        int currentActivitate = 0;
        public List<Activitate> getActivitate { get { return agenda; } }

        private readonly string fileName = ConfigurationManager.AppSettings.Get("NumeFisier");



        public List<Activitate> list()
        {
            return agenda;
        }

        public Activitate GetActivitate()
        {
            int index = Convert.ToInt32(Console.ReadLine());
            if (index <= 0 || index > agenda.Count)
            {
                throw new Exception("Invalid index");
            }
            return agenda[--index];
        }

        public void addActivitate()
        {
            agenda.Add(new Activitate());
            string name = Console.ReadLine();
            agenda[currentActivitate].setName(name);
            currentActivitate++;
        }

        public void removeActivitate()
        {
            int index = Convert.ToInt32(Console.ReadLine());
            if (index > agenda.Count || index < 0)
            {
                throw new Exception("Invalid index");
            }
            agenda.RemoveAt(index);
        }

        public List<Activitate> findByName()
        {
            List<Activitate> found = new List<Activitate>();
            string name = Console.ReadLine();
            foreach (Activitate agenda in agenda)
            {
                if (agenda.getName.ToLower().Contains(name.ToLower()))
                {
                    found.Add(agenda);
                }
            }
            return found;
        }

        public void writeDataInFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                streamWriter.Write(ConvertToSaveDateInFile());
            }
        }
        public ClassAgenda()
        {   
            string fileName = ConfigurationManager.AppSettings.Get("NumeFisier");
            Stream streamFisierText = File.Open(fileName, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void readDataFromFile() 
                                 
            
        {

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string lineFile;

                while ((lineFile = streamReader.ReadLine()) != null)
                {
                    agenda.Add(new Activitate());
                    agenda[currentActivitate].setName(lineFile.Trim());
                    currentActivitate++;
                }
            }
        }
        string ConvertToSaveDateInFile()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < agenda.Count; i++)
            {
                sb.AppendLine((i + 1) + ".\t" + agenda[i].getName);
            }
            return sb.ToString();
        }
    }
}