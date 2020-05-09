using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Anime anime = new Anime();
            this.Visible = false;
            anime.ShowDialog();
            this.Visible = true;
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            Staff staff = new Staff();
            this.Visible = false;
            staff.ShowDialog();
            this.Visible = true;
        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            Studio studio = new Studio();
            this.Visible = false;
            studio.ShowDialog();
            this.Visible = true;
        }

        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            Character character = new Character();
            this.Visible = false;
            character.ShowDialog();
            this.Visible = true;
        }

        private void b_MouseClick(object sender, MouseEventArgs e)
        {
            AddEpisodes eps = new AddEpisodes();
            this.Visible = false;
            eps.ShowDialog();
            this.Visible = true;
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            Manga manga = new Manga();
            this.Visible = false;
            manga.ShowDialog();
            this.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Publishers pubs = new Publishers();
            this.Visible = false;
            pubs.ShowDialog();
            this.Visible = true;
        }
    }
}
