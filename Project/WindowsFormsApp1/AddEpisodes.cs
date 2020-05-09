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
    public partial class AddEpisodes : Form
    {
        public AddEpisodes()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddEpisodes_Load(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            panelView.Visible = false;
            panelView.Location = new Point(196, 107);
        }

        private void bAdd_MouseClick(object sender, MouseEventArgs e)
        {
            panelAdd.Visible = true;
            panelView.Visible = false;
        }

        private void bSearch_MouseClick(object sender, MouseEventArgs e)
        {
            panelAdd.Visible = false;
            panelView.Visible = true;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            DbConnection db = new DbConnection();
            DataTable dt = db.Select("select * from Animes order by Name");
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "Anime_ID";
            comboBox1.DisplayMember = "Name";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(tbName.Text == "")
            {
                MessageBox.Show("No Episode Name");
            }
            else if(comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("No Anime Selected");
            }
            else if(textBox1.Text == "")
            {
                MessageBox.Show("No number in series");
            }
            else
            {
                DbConnection db = new DbConnection();
                db.Inserts("insert into Episodes(Anime_ID, Name, Series_Number) values("+
                    comboBox1.SelectedValue.ToString() + ",'"+tbName.Text+"',"+textBox1.Text+")");
                tbName.Text = "";
                textBox1.Text = "";
                comboBox1.Text = "";
                MessageBox.Show("Episode Added");
            }
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            DbConnection db1 = new DbConnection();
            DataTable dt = db1.Select("select * from Animes");
            comboBox3.DataSource = dt;
            comboBox3.ValueMember = "Anime_ID";
            comboBox3.DisplayMember = "Name";
            comboBox3.Text = "";
            MessageBox.Show(comboBox3.SelectedValue.ToString());
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DbConnection db = new DbConnection();
            DataTable dt = db.Select("select * from Episodes where Anime_ID = " + comboBox3.SelectedValue.ToString());
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "Episode_ID";
            comboBox2.DisplayMember = "Name";
            comboBox2.Text = "";
            textBox3.Text = "";
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DbConnection db = new DbConnection();
            DataTable dt = db.Select("select * from Episodes where Episode_Id = " + comboBox2.SelectedValue.ToString());
            textBox2.Text = dt.Rows[0][3].ToString();
            DataTable dt1 = db.Select("select * from Animes");
            comboBox4.DataSource = dt1;
            comboBox4.ValueMember = "Anime_ID";
            comboBox4.DisplayMember = "Name";
            comboBox4.Text = "";
        }

        private void Update(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox3.Text == "")
            {
                MessageBox.Show("New Name cannot be empty");
            }
            else if(comboBox4.SelectedIndex < 0 || comboBox4.Text == "") 
            {
                MessageBox.Show("No new anime selected");
            }
            else
            {
                DbConnection db = new DbConnection();
                if(comboBox4.SelectedValue != comboBox3.SelectedValue)
                {
                    db.Inserts("update Episodes set anime_id = " + comboBox4.SelectedValue.ToString()
                        + "where episode_id = " + comboBox2.SelectedValue.ToString());
                }
                db.Inserts("update Episodes set Name = '" + textBox3.Text + "', Series_Number = "
                    + textBox2.Text +" where episode_id = " 
                    + comboBox2.SelectedValue.ToString());
                MessageBox.Show("Update Applied");
                comboBox3.SelectedIndex = -1;
            }
        }
    }
}
