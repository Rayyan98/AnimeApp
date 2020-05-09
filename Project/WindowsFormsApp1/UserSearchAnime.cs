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
    public partial class UserSearchAnimes : Form
    {
        string iD;
        bool cChange;
        public UserSearchAnimes()
        {
            InitializeComponent();
        }

        public UserSearchAnimes(string id, bool change = false)
        {
            InitializeComponent();
            cChange = change;
            iD = id;

            DbConnection db = new DbConnection();
            DataTable dt = db.Select("select * from Watched_animes where user_name =  '"
                + User.userName + "' and anime_id = " + iD);
            if (dt.Rows.Count > 0)
            {
                cChange = true;
            }
            DataTable dt9 = db.Select("select * from [animes] where anime_id = " + id);
         //   MessageBox.Show(dt9.Rows.Count.ToString());
            textBox2.Text = dt9.Rows[0][1].ToString();
            textBox4.Text = dt9.Rows[0][5].ToString();
            textBox5.Text = dt9.Rows[0][10].ToString();
            textBox6.Text = dt9.Rows[0][8].ToString();

            DataTable dt2 = db.Select("select s.name from animes_has_studio a, studio s where a.studio_id = s.studio_id and a.anime_id = " + id);
            listBox1.DataSource = dt2;
            listBox1.DisplayMember = "Name";
            DataTable dt3 = db.Select("select s.name from animes_has_characters a, characters s where a.character_id = s.character_id and a.anime_id = " + id);
            listBox2.DataSource = dt3;
            listBox2.DisplayMember = "Name";
            DataTable dt4 = db.Select("select s.name from anime_sequel a, animes s where a.anime_id2 = s.anime_id and a.anime_id1 = " + id);
            listBox3.DataSource = dt4;
            listBox3.DisplayMember = "Name";

            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            listBox1.Enabled = false;
            listBox1.SelectedIndex = -1;
            listBox2.Enabled = false;
            listBox2.SelectedIndex = -1;
            listBox3.Enabled = false;
            listBox3.SelectedIndex = -1;

            if (cChange)
            {
                button1.Text = "Remove from list";
                comboBox1.Enabled = false;
                comboBox1.Text = dt.Rows[0][2].ToString();
                comboBox2.Enabled = false;
                comboBox2.Text = dt.Rows[0][3].ToString();

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
            if (comboBox1.SelectedIndex < 0 && !cChange)
            {
                MessageBox.Show("Please select a status !");
            }
            else
            {
                DialogResult result;
                if(!cChange)
                {
                    result = MessageBox.Show("Add this Anime to Favourites as well ?", "", MessageBoxButtons.YesNo);

                }
                else
                {
                    result = MessageBox.Show("Anime will be removed from favourites as well. Continue ?", "", MessageBoxButtons.YesNo);

                }
                DbConnection db = new DbConnection();

                if(!cChange)
                {
                    string s1 = "insert into watched_animes(anime_id, user_name";
                    string s2 = "values(" + iD + ", '" + User.userName+"'";
                    s1 += ", status";
                    s2 += ", '" + comboBox1.SelectedItem+"'";
                    if(comboBox2.SelectedIndex > -1)
                    {
                        s1 += ", rating";
                        s2 += ", " + comboBox2.SelectedItem;
/*                        MessageBox.Show("update Animes set Users_added = users_added + 1, Users_rated =" +
                            "users_rated " + comboBox2.SelectedItem + " where anime_id = " + iD);*/
                        db.Inserts("update Animes set Users_added = users_added + 1, Users_rated =" +
                            "users_rated +" + comboBox2.SelectedItem + " where anime_id = "+ iD);
                    }
                    s1 += ", favourite";
                    if (result == DialogResult.Yes)
                    {
                        s2 += ", 1";
                        db.Inserts("update Animes set Users_favourited = Users_favourited + 1 "
                            + "where anime_id = " + iD);
                    }
                    else
                    {
                        s2 += ", 0";
                    }
                    string s = s1 + ") " + s2 + ")";
//                    MessageBox.Show(s);
                    db.Inserts(s);
                    db.Inserts("update Users set shows_" + comboBox1.SelectedItem + " = " +
                        "shows_" + comboBox1.SelectedItem + "+1 where user_name = '" + User.userName+ "'");
                    cChange = true;
                    button1.Text = "Remove from list";
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    MessageBox.Show("Addition succesful");
                }
                else
                {
                    if(result == DialogResult.Yes)
                    {
                        DataTable dt = db.Select("select * from watched_animes where anime_id = " + iD +
                            " and user_name = '" + User.userName+ "'");
                        if(dt.Rows[0][3].ToString() != "0")
                        {
                            db.Inserts("update animes set users_added = users_added - 1," +
                                "users_rated = users_rated - " + dt.Rows[0][3] + " where anime_id = " + iD);

                        }
                        if(dt.Rows[0][4].ToString() != "0")
                        {
                            db.Inserts("update animes set users" +
                                "_favourited = users_favourited - 1 where anime_id = " + iD);

                        }
                        db.Inserts("update users set shows_" + dt.Rows[0][2] + " = shows_" + dt.Rows[0][2] + " -1" +
                            " where user_name = '" + User.userName+"'");
                        db.Inserts("delete from Watched_animes where user_name = '" + User.userName
                            + "' and anime_id = " + iD);
                        cChange = false;
                        button1.Text = "Add to my List";
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        MessageBox.Show("Removed succesfully");
                    }
                }

            }

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}
