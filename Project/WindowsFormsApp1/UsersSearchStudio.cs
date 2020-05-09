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
    public partial class UserSearchStudio : Form
    {
        string iD;
        bool cChange;
        public UserSearchStudio()
        {
            InitializeComponent();
        }

        public UserSearchStudio(string id, bool change = false)
        {
            InitializeComponent();
            iD = id;
            DbConnection db = new DbConnection();
            DataTable dt = db.Select("select * from Animes_has_studio P, animes A " +
                "where P.anime_id = A.anime_id and p.studio_id = " + iD);
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Anime_id";
            DataTable dt2 = db.Select("Select * from studio where studio_id = " + iD);
            textBox2.Text = dt2.Rows[0][1].ToString();
            textBox2.Enabled = false;
            cChange = change;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UserSearch_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }



        private void UserSearchAnimes_Load(object sender, EventArgs e)
        {

        }

        private void UserSearchMangas_Load(object sender, EventArgs e)
        {

        }
    }
}
