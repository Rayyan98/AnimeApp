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
    public partial class UserSearchCharacters : Form
    {
        string iD;
        bool cChange;
        public UserSearchCharacters()
        {
            InitializeComponent();
        }

        public UserSearchCharacters(string id, bool change = false)
        {
            InitializeComponent();

            iD = id;

            cChange = change;
            if(cChange == false)
            {
                DbConnection db1 = new DbConnection();
                MessageBox.Show("select * from favourite_characters where user_name = '"
                 + User.userName + "' and character_id = " + iD);
                DataTable dt = db1.Select("select * from favourite_characters where user_name = '"
                 + User.userName + "' and character_id = " + iD);
                if (dt.Rows.Count > 0)
                {
                    cChange = true;
                }
            }


            DbConnection db = new DbConnection();
            DataTable dt9 = db.Select("select * from [characters] where character_id = " + id);
            textBox2.Text = dt9.Rows[0][1].ToString();
            textBox5.Text = dt9.Rows[0][2].ToString();
            textBox6.Text = dt9.Rows[0][5].ToString();

            DataTable dt2 = db.Select("select * from animes_has_characters a, animes s where a.anime_id = s.anime_id and a.character_id = " + id);
            listBox1.DataSource = dt2;
            listBox1.DisplayMember = "Name";
            DataTable dt3 = db.Select("select * from mangas_has_characters a, mangas s where a.manga_id = s.manga_id and a.character_id = " + id);
            listBox2.DataSource = dt3;
            listBox2.DisplayMember = "Name";

            textBox2.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            dateTimePicker1.Enabled = false;
            listBox1.Enabled = false;
            listBox1.SelectedIndex = -1;
            listBox2.Enabled = false;
            listBox2.SelectedIndex = -1;

            if (cChange == true)
            {
                button1.Text = "Remove from favourites";
            }
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
            DialogResult result;
            if(!cChange)
            {
                result = MessageBox.Show("Add this Character to your Favourites ?", "", MessageBoxButtons.YesNo);
            }
            else
            {
                result = MessageBox.Show("Remove this Character from your Favourites ?", "", MessageBoxButtons.YesNo);

            }
            DbConnection db = new DbConnection();

            if (result == DialogResult.Yes)
            {
                if (!cChange)
                {
                    db.Inserts("insert into favourite_characters values (" +
                        iD + ",'" + User.userName+"')");
                    db.Inserts("update Characters set users_favourited = users_favourited + 1"+
                        " where character_id = " + iD);
                    MessageBox.Show("Added To Favourites");
                    cChange = true;
                    button1.Text = "Remove from favourites";
                }
                else

                {
                    db.Inserts("delete from favourite_characters where user_name = '" + User.userName +
                        "' and character_id = " + iD);
                    db.Inserts("update characters set users_favourited = users_favourited - 1 " +
                        "where character_id = " + iD);
                    MessageBox.Show("Removed from favourites");
                    cChange = false;
                    button1.Text = "Add to favourites";

                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}
