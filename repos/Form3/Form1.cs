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

        private void ReadDataFromFile()
        {
            string fileName = ConfigurationManager.AppSettings.Get("NumeFisier");

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Activitate activitate = new Activitate(line);
                        activitati.Add(activitate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file: " + ex.Message);
            }
        }

        private void DisplayActivitatiInDataGridView()
        {
            dataGridView1.Rows.Clear();

            // Add headers to the DataGridView
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Nume";
            dataGridView1.Columns[1].Name = "Tip";
            dataGridView1.Columns[2].Name = "Data";
            dataGridView1.Columns[3].Name = "Descriere";

            // Add each activity to the DataGridView
            foreach (Activitate activitate in activitati)
            {
                dataGridView1.Rows.Add(activitate.Nume, activitate.Tip, activitate.Data, activitate.Descriere);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Extrage valorile din text box-uri
            string nume = textBox1.Text;
            string activitate = textBox2.Text;
            DateTime data = dateTimePicker1.Value;
            string descriere = textBox4.Text;

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
        }

        private void label5_Click(object sender, EventArgs e)
        {

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
    

