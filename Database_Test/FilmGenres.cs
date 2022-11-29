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
    public partial class FilmGenres : Form
    {

        Database Database = new Database();

        int selectedRow;
        List<String> Genres = new List<string>();

        public FilmGenres(string[] genres)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            foreach (string s in genres)
            {
                Genres.Add(s);
            }
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Жанр");
            dataGridView1.Columns.Add("Description", "Опис");
            dataGridView1.Columns.Add("Films", "Фільми");
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetValue(2), record.GetString(3));
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"SELECT g.ID, g.Name, g.Description, STRING_AGG('«' + f.Name, '», ') + '»' AS Films " +
                $"FROM Genre g, Film_Genre fg, Film f " +
                $"WHERE fg.GenreID = g.ID and fg.FilmID = f.ID and g.Name in (";


            for (int i = 0; i < Genres.Count; i++)
            {
                queryString += $"'{Genres[i]}'";

                if (i != Genres.Count - 1)
                {
                    queryString += ", ";
                }
                else
                {
                    queryString += ") GROUP BY g.ID, g.Name, g.Description";
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

        private void FilmGenres_Load(object sender, EventArgs e)
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
                textBox_Description.Text = row.Cells[2].Value.ToString();
                textBox_Films.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}
