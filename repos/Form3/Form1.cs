using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApp;

namespace Form3
{
    public partial class Form1 : Form
    {
        public List<Activitate> activitati = new List<Activitate>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load activities from file
            ReadDataFromFile();

            // Display activities in DataGridView and ListBox
            DisplayActivitatiInDataGridView();
        }
        private void WriteDataToFile(string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    foreach (Activitate activitate in activitati)
                    {
                        // Scrie fiecare activitate într-o linie separată în fișier
                        sw.WriteLine($"{activitate.Nume},{activitate.Tip},{activitate.Data},{activitate.Descriere}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while writing to the file: " + ex.Message);
            }
        }

        private void ReadDataFromFile()
        {
            string fileName = ConfigurationManager.AppSettings.Get("NumeFisier");

            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            // Descompune linia citită în componente (nume, tip, data, descriere)
                            string[] components = line.Split(',');

                            // Verifică dacă linia are suficiente componente
                            if (components.Length >= 4)
                            {
                                // Creează un nou obiect Activitate și adaugă-l în lista de activități
                                Activitate activitate = new Activitate()
                                {
                                    Nume = components[0],
                                    Tip = components[1],
                                    Data = DateTime.Parse(components[2]),
                                    Descriere = components[3]
                                };
                                activitati.Add(activitate);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error: File name is empty or null in the configuration file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file '" + fileName + "': " + ex.Message);
            }
        }

        private void DisplayActivitatiInDataGridView()
        {
            dataGridView1.Rows.Clear();

            // Add headers to the DataGridView
            dataGridView1.ColumnCount = 5; // Adăugăm o coloană suplimentară pentru butonul de ștergere
            dataGridView1.Columns[0].Name = "Nume";
            dataGridView1.Columns[1].Name = "Tip";
            dataGridView1.Columns[2].Name = "Data";
            dataGridView1.Columns[3].Name = "Descriere";
            dataGridView1.Columns[4].Name = "Delete"; // Numele coloanei pentru butonul de ștergere

            // Add each activity to the DataGridView
            foreach (Activitate activitate in activitati)
            {
                DataGridViewButtonCell deleteButtonCell = new DataGridViewButtonCell();
                deleteButtonCell.Value = "Delete";
                dataGridView1.Rows.Add(activitate.Nume, activitate.Tip, activitate.Data, activitate.Descriere, deleteButtonCell);
            }

            // Subscriere la evenimentul de click pentru butoanele de ștergere
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
        }
            private void button1_Click(object sender, EventArgs e)
        {
            // Extrage valorile din text box-uri și radio buttons
            string nume = textBox1.Text;
            string activitate = textBox2.Text;
            DateTime data = dateTimePicker1.Value;
            string descriere = GetSelectedDescription();

            // Creează un obiect Activitate cu valorile extrase
            Activitate newActivitate = new Activitate()
            {
                Nume = nume,
                Tip = activitate,
                Data = data,
                Descriere = descriere
            };

            // Adaugă noul obiect Activitate în lista de activități
            activitati.Add(newActivitate);

            // Actualizează afișarea în DataGridView
            DisplayActivitatiInDataGridView();

            // Scrie datele în fișier
            string fileName = ConfigurationManager.AppSettings.Get("NumeFisier");
            if (!string.IsNullOrEmpty(fileName))
            {
                WriteDataToFile(fileName);
            }
            else
            {
                MessageBox.Show("Error: File name is empty or null in the configuration file.");
            }
            textBox1.Text = "";
            textBox2.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private string GetSelectedDescription()
        {
            // Verifică care radio button este selectat și întoarce textul corespunzător
            if (radioButton1.Checked)
            {
                return radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                return radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                return radioButton3.Text;
            }
            else if (radioButton4.Checked)
            {
                return radioButton4.Text;
            }
            else
            {
                return ""; // În cazul în care niciun radio button nu este selectat
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementează codul pentru acest eveniment aici sau poți lăsa această metodă goală dacă nu ai nevoie să faci nimic atunci când se face click pe conținutul celulei DataGridView.
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBox3.Text.ToLower();

            // Filtrăm activitățile după termenul de căutare și actualizăm afișarea
            List<Activitate> filteredActivitati = activitati.Where(act =>
                act.Nume.ToLower().Contains(searchTerm) ||
                act.Tip.ToLower().Contains(searchTerm) ||
                act.Descriere.ToLower().Contains(searchTerm) ||
                act.Data.ToShortDateString().Contains(searchTerm)).ToList();

            DisplayFilteredActivitatiInDataGridView(filteredActivitati);
        }

        private void DisplayFilteredActivitatiInDataGridView(List<Activitate> filteredActivitati)
        {
            dataGridView1.Rows.Clear();

            foreach (Activitate activitate in filteredActivitati)
            {
                dataGridView1.Rows.Add(activitate.Nume, activitate.Tip, activitate.Data, activitate.Descriere);
            }
        }
    }
}
