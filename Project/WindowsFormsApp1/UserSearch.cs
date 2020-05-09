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
    public partial class UserSearch : Form
    {
        int searchC = -1;
        public UserSearch()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("No Category Selected");
            }
            else if(comboBox1.SelectedIndex == 0)
            {
                DbConnection db = new DbConnection();
                DataTable dt =  db.Select("Select * from Animes where Name like '%"+textBox2.Text + "%' order by Name");
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Anime_ID";
                searchC = 0;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                DbConnection db = new DbConnection();
                DataTable dt = db.Select("Select * from Mangas where Name like '%" + textBox2.Text + "%' order by Name");
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Manga_ID";
                searchC = 1;

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                DbConnection db = new DbConnection();
                DataTable dt = db.Select("Select * from Characters where Name like '%" + textBox2.Text + "%' order by Name");
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Character_ID";
                searchC = 2;

            }
            else if (comboBox1.SelectedIndex == 3)
            {
                DbConnection db = new DbConnection();
                DataTable dt = db.Select("Select * from Staff where Name like '%" + textBox2.Text + "%' order by Name");
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Staff_ID";
                searchC = 3;

            }
            else if (comboBox1.SelectedIndex == 4)
            {
                DbConnection db = new DbConnection();
                DataTable dt = db.Select("Select * from Publishers where Name like '%" + textBox2.Text + "%' order by Name");
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Publisher_ID";
                searchC = 4;

            }
            else if (comboBox1.SelectedIndex == 5)
            {
                DbConnection db = new DbConnection();
                DataTable dt = db.Select("Select * from Studio where Name like '%" + textBox2.Text + "%' order by Name");
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Studio_ID";
                searchC = 5;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1)
            {
                if (searchC == 0)
                {
                    UserSearchAnimes a = new UserSearchAnimes(listBox1.SelectedValue.ToString());
                    this.Hide();
                    a.ShowDialog();
                    this.Show();
                }
                else if(searchC == 1)
                {
                    UserSearchMangas a = new UserSearchMangas(listBox1.SelectedValue.ToString());
                    this.Hide();
                    a.ShowDialog();
                    this.Show();
                }
                else if(searchC == 4)
                {
                    UserSearchPublishers a = new UserSearchPublishers(listBox1.SelectedValue.ToString());
                    this.Hide();
                    a.ShowDialog();
                    this.Show();
                }
                else if(searchC == 5)
                {
                    UserSearchStudio a = new UserSearchStudio(listBox1.SelectedValue.ToString());
                    this.Hide();
                    a.ShowDialog();
                    this.Show();

                }
                else if(searchC == 2)
                {
                    UserSearchCharacters a = new UserSearchCharacters(listBox1.SelectedValue.ToString());
                    this.Hide();
                    a.ShowDialog();
                    this.Show();
                }
                else if(searchC == 3)
                {
                    UserSearchStaff a = new UserSearchStaff(listBox1.SelectedValue.ToString());
                    this.Hide();
                    a.ShowDialog();
                    this.Show();

                }
            }
        }
    }
}
