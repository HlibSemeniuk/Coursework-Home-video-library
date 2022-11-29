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
   

    public partial class Form1 : Form
    {

        Database database = new Database();

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        


        private void button1_Click(object sender, EventArgs e)
        {
            ActorMenu actorMenu = new ActorMenu();
            this.Hide();
            actorMenu.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Режисери directorsMenu = new Режисери();
            this.Hide();
            directorsMenu.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FilmMenu filmsMenu = new FilmMenu();
            this.Hide();
            filmsMenu.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Genres genresMenu = new Genres();
            this.Hide();
            genresMenu.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
