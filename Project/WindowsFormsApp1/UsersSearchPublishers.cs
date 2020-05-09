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
    public partial class UserSearchPublishers : Form
    {
        string iD;
        bool cChange;
        public UserSearchPublishers()
        {
            InitializeComponent();
        }

        public UserSearchPublishers(string id, bool change = false)
        {
            cChange = change;
            InitializeComponent();
            iD = id;
            DbConnection db = new DbConnection();
            DataTable dt = db.Select("select * from published_by P, mangas A " +
                "where P.manga_id = A.manga_id and p.publisher_id = " + iD);
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Manga_id";
            DataTable dt2 = db.Select("Select * from publishers where publisher_id = " + iD);
            textBox1.Text = dt2.Rows[0][1].ToString();
            textBox2.Text = dt2.Rows[0][2].ToString();
            textBox1.Enabled = false;
            textBox2.Enabled = false;

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
