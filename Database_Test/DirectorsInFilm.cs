using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Database_Test
{
    public partial class DirectorsInFilm : Form
    {

        Database Database = new Database();

        int selectedRow;
        List<String> DirectorsNames = new List<string>();

        public DirectorsInFilm(string[] names)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            foreach (string s in names)
            {
                DirectorsNames.Add(s);
            }
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Ім'я та прізвище");
            dataGridView1.Columns.Add("Age", "Вік");
            dataGridView1.Columns.Add("Description", "Опис");
            dataGridView1.Columns.Add("Films", "Фільми");
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetValue(2), record.GetString(3), record.GetString(4));
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"SELECT d.ID, d.Name, d.Age, d.Description, STRING_AGG('«' + f.Name, '», ') + '»' AS Films " +
                $"FROM Director d, Film_Director fd, Film f " +
                $"WHERE fd.DirectorID = d.ID and fd.FilmID = f.ID and d.Name in (";


            for (int i = 0; i < DirectorsNames.Count; i++)
            {
                queryString += $"'{DirectorsNames[i]}'";

                if (i != DirectorsNames.Count - 1)
                {
                    queryString += ", ";
                }
                else
                {
                    queryString += ") GROUP BY  d.ID, d.Name, d.Age, d.Description";
                }
            }

            SqlCommand command = new SqlCommand(queryString, Database.GetConnection());

            Database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void DirectorsInFilm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox_ID.Text = row.Cells[0].Value.ToString();
                textBox_Name.Text = row.Cells[1].Value.ToString();
                textBox_Age.Text = row.Cells[2].Value.ToString();
                textBox_Description.Text = row.Cells[3].Value.ToString();
                textBox_Films.Text = row.Cells[4].Value.ToString();

            }
        }

  
    }
}
