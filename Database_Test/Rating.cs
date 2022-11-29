using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Test
{
    public partial class Rating : Form
    {

        Database Database = new Database();

        int selectedRow;
        string FilmID;

        public Rating(string filmID)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.FilmID = filmID;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Reviewer_Name", "Оглядач");
            dataGridView1.Columns.Add("Description", "Огляд");
            dataGridView1.Columns.Add("Rating", "Рейтинг");
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetString(1), String.Format("{0:0.00}", record.GetDecimal(2)));
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select Reviewer_Name, Description, Rating FROM Review WHERE FilmID = {FilmID}";
            SqlCommand command = new SqlCommand(queryString, Database.GetConnection());

            Database.OpenConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void Rating_Load(object sender, EventArgs e)
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

                textBox_ReviewerName.Text = row.Cells[0].Value.ToString();
                textBox_Description.Text = row.Cells[1].Value.ToString();
                textBox_Raiting.Text = row.Cells[2].Value.ToString();
            }
        }
    }
}
