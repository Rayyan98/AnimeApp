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
    public partial class User : Form
    {
        public static string userName;
        public User()
        {
            InitializeComponent();
            userName = "rayyan95";

        }

        public User(string val, string id)
        {
            InitializeComponent();
            if(val == "1")
            {
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
            }
            userName = id;
            lPublishers.Text = id;
        }

        private void lPublishers_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void User_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserSearch a = new UserSearch();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            UserAnime a = new UserAnime();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserManga a = new UserManga();
            this.Hide();
            a.ShowDialog();
            this.Show();

        }
    }
}
