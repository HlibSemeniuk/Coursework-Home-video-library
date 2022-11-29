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


    public partial class ActorMenu : Form
    {
        Database Database = new Database();

        int selectedRow;

        public ActorMenu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Ім'я та прізвище");
            dataGridView1.Columns.Add("Age", "Вік");
            dataGridView1.Columns.Add("Country", "Країна");
            dataGridView1.Columns.Add("Description", "Опис");
            dataGridView1.Columns.Add("Filmography", "Фільмографія");
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetValue(2), record.GetString(3), record.GetString(4), record.GetString(5));
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"SELECT a.ID, a.Name, a.Age, a.Country, a.Description, isnull(STRING_AGG(case when fa.ActorID = a.ID and fa.FilmID = f.ID then('«' + f.Name + '»') else null end, ', '), 'Відсутня інформація')  AS Filmography " +
                $"FROM Actor a, Film_Actor fa, Film f GROUP BY a.ID, a.Name, a.Age, a.Country, a.Description";

            SqlCommand command = new SqlCommand(queryString, Database.GetConnection());

            Database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void FilmsMenu_Load(object sender, EventArgs e)
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


      
        private void button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
        }

        private void SearchBy(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string searchString = $"SELECT a.ID, a.Name, a.Age, a.Country, a.Description, isnull(STRING_AGG(case when fa.ActorID = a.ID and fa.FilmID = f.ID then('«' + f.Name + '»') else null end, ', '), 'Відсутня інформація')  AS Filmography " +
                $"FROM Actor a, Film_Actor fa, Film f GROUP BY a.ID, a.Name, a.Age, a.Country, a.Description " +
                $"HAVING  (concat (a.ID, a.Name, a.Age, a.Country, a.Description, isnull(STRING_AGG(case when fa.ActorID = a.ID and fa.FilmID = f.ID then('«' + f.Name + '»') else null end, ', '), 'Відсутня інформація')) like '%{textBox_Search.Text}%')";

            SqlCommand command = new SqlCommand(searchString, Database.GetConnection());

            Database.OpenConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgv, read);
            }

            read.Close();
        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            SearchBy(dataGridView1);
        }
    }
}
