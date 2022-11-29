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
    public partial class ActorsInFilm : Form
    {

        Database Database = new Database();

        int selectedRow;
        List<String> ActorsNames = new List<string>();

        public ActorsInFilm(string[] names)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            foreach (string s in names)
            {
                ActorsNames.Add(s);
            }
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Ім'я та прізвище");
            dataGridView1.Columns.Add("Age", "Вік");
            dataGridView1.Columns.Add("Country", "Країна");
            dataGridView1.Columns.Add("Description", "Опис");
            dataGridView1.Columns.Add("Filmohraphy", "Фільмографія");
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetValue(2), record.GetString(3), record.GetString(4), record.GetString(5));
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"SELECT a.ID, a.Name, a.Age, a.Country, a.Description, STRING_AGG('«' + f.Name, '», ') + '»' " +
                $"FROM Actor a, Film_Actor fa, Film f " +
                $"WHERE fa.ActorID = a.ID and fa.FilmID = f.ID and a.Name in (";


            for (int i = 0; i <  ActorsNames.Count; i++)
            {
                queryString += $"'{ActorsNames[i]}'";

                if (i != ActorsNames.Count -1)
                {
                    queryString += ", ";
                }
                else
                {
                    queryString += ") GROUP BY  a.ID, a.Name, a.Age, a.Country, a.Description";
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

        private void ActorsInFilm_Load(object sender, EventArgs e)
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
                textBox_Country.Text = row.Cells[3].Value.ToString();
                textBox_Description.Text = row.Cells[4].Value.ToString();
                textBox_Filmography.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
