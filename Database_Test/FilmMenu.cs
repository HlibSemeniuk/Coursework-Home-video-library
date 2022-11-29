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
using System.Globalization;

namespace Database_Test
{
    public partial class FilmMenu : Form
    {
        Database Database = new Database();

         int selectedRow;

        public FilmMenu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Назва");
            dataGridView1.Columns.Add("Release_date", "Дата випуску");
            dataGridView1.Columns.Add("Country", "Країна");
            dataGridView1.Columns.Add("Budget", "Бюджет");
            dataGridView1.Columns.Add("Rating", "Рейтинг");
            dataGridView1.Columns.Add("Genres", "Жанри");
            dataGridView1.Columns.Add("Directors", "Режисери");
            dataGridView1.Columns.Add("Actors", "Актори");
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetDateTime(2).ToString("dd.MM.yyyy"), record.GetString(3), record.GetDecimal(4).ToString("C"), String.Format("{0:0.00}", record.GetDecimal(5)), record.GetString(6), record.GetString(7), record.GetString(8));
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"SELECT f.ID, f.Name, f.Release_Date, f.Country, f.Budget, isnull(r.Rating, 0) AS Raiting,  isnull(g.Genres, 'Відсутня інформація') AS Genres, d.Directors, a.Actors " +
                $"FROM Film f " +
                $"LEFT JOIN (" +
                $"select FilmID, sum(Rating) / count (Reviewer_Name) AS Rating " +
                $"from Review GROUP BY FilmID) r on f.ID = r.FilmID " +
                $"LEFT JOIN (" +
                $"SELECT f.ID AS FilmID, isnull( string_agg(case when fd.FilmID = f.ID and fd.DirectorID = d.ID then d.Name else null end, ', '), 'Відсутня інформація') AS Directors " +
                $"FROM Director d, Film_Director fd, Film f GROUP BY f.ID) d on d.FilmID = f.ID " +
                $"LEFT JOIN (" +
                $"SELECT f.ID AS FilmID, isnull( string_agg(case when fa.FilmID = f.ID and fa.ActorID = a.ID then a.Name else null end, ', '), 'Відсутня інформація') AS Actors " +
                $"FROM Actor a, Film_Actor fa, Film f GROUP BY f.ID) a on a.FilmID = f.ID " +
                $"LEFT JOIN (" +
                $"SELECT f.ID AS FilmID, isnull(string_agg(case when fg.FilmID = f.ID and fg.GenreID = g.ID then g.Name else null end, ', '), 'Відсутня інформація') AS Genres " +
                $"FROM Genre g, Film_Genre fg, Film f GROUP BY f.ID) g on g.FilmID = f.ID \r\nGROUP BY f.ID, f.Name, f.Release_Date, f.Country, f.Budget, isnull(r.Rating, 0), d.Directors, a.Actors, g.Genres";

            SqlCommand command = new SqlCommand(queryString, Database.GetConnection());

            Database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void FilmMenu_Load(object sender, EventArgs e)
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
                textBox_ReleaseDate.Text = row.Cells[2].Value.ToString();
                textBox_Country.Text = row.Cells[3].Value.ToString();
                textBox_Budget.Text = row.Cells[4].Value.ToString();
                textBox_Budget.Text = row.Cells[4].Value.ToString();
                textBox_Rating.Text = row.Cells[5].Value.ToString();
                textBox_Genres.Text = row.Cells[6].Value.ToString();
                textBox1_Directors.Text = row.Cells[7].Value.ToString();
                textBox_Actors.Text = row.Cells[8].Value.ToString();

            }
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }



        private void SearchBy(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string searchString = $"SELECT f.ID, f.Name, f.Release_Date, f.Country, f.Budget, isnull(r.Rating, 0) AS Raiting,  isnull(g.Genres, 'Відсутня інформація') AS Genres, d.Directors, a.Actors " +
                $"FROM Film f " +
                $"LEFT JOIN (" +
                $"select FilmID, sum(Rating) / count (Reviewer_Name) AS Rating " +
                $"from Review GROUP BY FilmID) r on f.ID = r.FilmID " +
                $"LEFT JOIN (" +
                $"SELECT f.ID AS FilmID, isnull( string_agg(case when fd.FilmID = f.ID and fd.DirectorID = d.ID then d.Name else null end, ', '), 'Відсутня інформація') AS Directors " +
                $"FROM Director d, Film_Director fd, Film f GROUP BY f.ID) d on d.FilmID = f.ID " +
                $"LEFT JOIN (" +
                $"SELECT f.ID AS FilmID, isnull( string_agg(case when fa.FilmID = f.ID and fa.ActorID = a.ID then a.Name else null end, ', '), 'Відсутня інформація') AS Actors " +
                $"FROM Actor a, Film_Actor fa, Film f GROUP BY f.ID) a on a.FilmID = f.ID " +
                $"LEFT JOIN (" +
                $"SELECT f.ID AS FilmID, isnull(string_agg(case when fg.FilmID = f.ID and fg.GenreID = g.ID then g.Name else null end, ', '), 'Відсутня інформація') AS Genres " +
                $"FROM Genre g, Film_Genre fg, Film f GROUP BY f.ID) g on g.FilmID = f.ID " +
                $"GROUP BY f.ID, f.Name, f.Release_Date, f.Country, f.Budget, isnull(r.Rating, 0), d.Directors, a.Actors, g.Genres " +
                $"HAVING (concat (f.ID, f.Name, f.Release_Date, f.Country, f.Budget, isnull(r.Rating, 0), isnull(g.Genres, 'Відсутня інформація'), d.Directors, a.Actors) like '%{textBox_Search.Text}%')";


                SqlCommand command = new SqlCommand(searchString, Database.GetConnection());

            Database.OpenConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgv, read);
            }

            read.Close();
        }

        private void textBox_SearchByName_TextChanged(object sender, EventArgs e)
        {
            SearchBy(dataGridView1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
        }




        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                if (Decimal.Parse(row.Cells[5].Value.ToString()) != 0)
                {
                    Rating rating = new Rating(row.Cells[0].Value.ToString());
                    rating.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Фільм не має оглядів!", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                if (row.Cells[6].Value.ToString() != "Відсутня інформація")
                {
                    string[] stringSeparators = new string[] { ", " };
                    string[] Genres = row.Cells[6].Value.ToString().Split(stringSeparators, StringSplitOptions.None);
                    FilmGenres filmGenres = new FilmGenres(Genres);
                    filmGenres.ShowDialog();
                }
                else
                {
                    MessageBox.Show("У фільмі не вказані жанри!", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                if (row.Cells[7].Value.ToString() != "Відсутня інформація")
                {
                    string[] stringSeparators = new string[] { ", " };
                    string[] Directors = row.Cells[7].Value.ToString().Split(stringSeparators, StringSplitOptions.None);
                    DirectorsInFilm directorsInFilm = new DirectorsInFilm(Directors);
                    directorsInFilm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("У фільмі не вказані режисери!", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (e.RowIndex >= 0 && e.ColumnIndex == 8)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                if (row.Cells[8].Value.ToString() != "Відсутня інформація")
                {
                    string[] stringSeparators = new string[] { ", " };
                    string[] Names = row.Cells[8].Value.ToString().Split(stringSeparators, StringSplitOptions.None);
                    ActorsInFilm actorsInFilm = new ActorsInFilm(Names);
                    actorsInFilm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("У фільмі не вказані актори!", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
