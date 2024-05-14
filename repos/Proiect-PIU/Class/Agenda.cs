using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Agenda
    {
        public List<Agenda> obiecte = new List<Agenda>();
        int currentActivitate = 0;
        private string fileName = ConfigurationManager.AppSettings.Get("NumeFisier");

        public int getActivitate { get { return activitate; } }
        public List<Activitate> getActivitate { get { return activitate; } }
        public List<Agenda> list()
        {
            return obiecte;
        }

        public Agenda GetActivitate()
        {
            int index = Convert.ToInt32(Console.ReadLine());
            if (index <= 0 || index > obiecte.Count)
            {
                throw new Exception("Invalid index");
            }
            return obiecte[--index];
        }

        public void addActivitate()
        {
            obiecte.Add(new Agenda());
            string name = Console.ReadLine();
            obiecte[currentActivitate].setName(name);
            currentActivitate++;
        }

        public void removeActivitate()
        {
            int index = Convert.ToInt32(Console.ReadLine());
            if (index > obiecte.Count || index < 0)
            {
                throw new Exception("Invalid index");
            }
            obiecte.RemoveAt(index);
        }
        public void updateActivitate(int index, string name)
        {
            Agenda toUpdate = GetActivitate(index);
            toUpdate.setName(name);
        }

        public List<Agenda> findByName()
        {
            List<Agenda> found = new List<>();
            string name = Console.ReadLine();
            foreach (Agenda agenda in obiecte)
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

        public void readDataFromFile() 
        {
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string lineFile;

                while ((lineFile = streamReader.ReadLine()) != null)
                {
                    string[] activitate = lineFile.Split('.');
                    activitate.Add(new Activitate());
                    activitate[nrActivitate].setName(activitate[1].Trim());
                    nrActivitate++;
                }
            }
        }

        string ConvertToSaveDateInFile()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < activitate.Count; i++)
            {
                sb.AppendLine((i + 1) + ".\t" + activitate[i].getName);
            }
            return sb.ToString();
        }
    }
}
    }
}