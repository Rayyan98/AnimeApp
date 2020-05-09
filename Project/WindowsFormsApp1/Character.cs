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
    public partial class Character : Form
    {
        List<string> updatestr = new List<string>();
        public Character()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Description_Click(object sender, EventArgs e)
        {

        }

        private void Character_Load(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            panelSearch.Visible = false;
            panelView.Visible = false;
            panelView.Location = new Point(202, 120);
            panelAdd.Location = new Point(202, 120);
            panelSearch.Location = new Point(202, 120);
            DOB.CustomFormat = " ";
            DOB.Format = DateTimePickerFormat.Custom;

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = true;
            panelSearch.Visible = false;
            panelView.Visible = false;

            listBox2.ValueMember = "Anime_ID";
            listBox2.DisplayMember = "Name";
            listBox3.ValueMember = "Manga_ID";
            listBox3.DisplayMember = "Name";


            DbConnection read = new DbConnection();
            DataTable dt = read.Select("select * from Animes");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Anime_ID";

            DataTable dt1 = read.Select("select * from Mangas");
            comboBox2.DataSource = dt1;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Manga_ID";






        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            panelSearch.Visible = true;
            panelView.Visible = false;
            DbConnection read = new DbConnection();
            DataTable dt1 = read.Select("select * from Animes");
            DataTable dt2 = read.Select("select * from Mangas");
            comboBox4.DataSource = dt1;
            comboBox5.DataSource = dt2;
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "Anime_ID";
            comboBox5.DisplayMember = "Name";
            comboBox5.ValueMember = "Manga_ID";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("No Name of Anime");
            }
            else
            {
                DbConnection write = new DbConnection();
                string s1 = "Insert into Characters([Name]";
                string s2 = " values('" + tbName.Text + "'";
                string s3 = ")";


                if (textBox1.Text != "")
                {
                    s1 += ",[Description]";
                    s2 += ",'" + textBox1.Text + "'";
                }

                if (DOB.Format == DateTimePickerFormat.Short)
                {
                    s1 += ",[DOB]";
                    s2 += ",'" + DOB.Value.ToString() + "'";
                }
                if (rbFemale.Checked == true)
                {
                    s1 += ",[Gender]";
                    s2 += ",'" + "F" + "'";
                }
                else
                {
                    s1 += ",[Gender]";
                    s2 += ",'" + "M" + "'";
                }
                string s = s1 + ")" + s2 + s3;
                write.Inserts(s);

                DataTable dt3 = write.Select("Select top 1 Character_ID from Characters order by Character_ID desc");

                for (int a = 0; a < listBox2.Items.Count; a++)
                {
                    write.Inserts("Insert into Animes_has_Characters(Character_ID,Anime_ID) values(" + dt3.Rows[0][0].ToString() + "," + (((DataRowView)listBox2.Items[a])["Anime_ID"]).ToString() + ")");
                }

                for (int a = 0; a < listBox3.Items.Count; a++)
                {
                    write.Inserts("Insert into Mangas_has_Characters(Character_ID,Manga_ID) values(" + dt3.Rows[0][0].ToString() + "," + (((DataRowView)listBox3.Items[a])["Manga_ID"]).ToString() + ")");
                }


                tbName.Text = "";
                MessageBox.Show("Character Added Succesfully");
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panelSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bView_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                panelAdd.Visible = false;
                panelSearch.Visible = false;
                panelView.Visible = true;
                updatestr.Clear();

                textBox3.Text = ((DataRowView)listBox1.SelectedItem)["Character_Name"].ToString();
                dateTimePicker1.Value = System.Convert.ToDateTime(((DataRowView)listBox1.SelectedItem)["DOB"]);
                if (((DataRowView)listBox1.SelectedItem)["Gender"].ToString() == "M")
                {
                    radioButton3.Checked = true;
                }
                else
                    radioButton4.Checked = true;
                textBox2.Text = ((DataRowView)listBox1.SelectedItem)["Description"].ToString();

                DbConnection write = new DbConnection();


                DataTable dt1 = write.Select("select * from Animes order by Name");
                comboBox9.DataSource = dt1;
                comboBox9.DisplayMember = "Name";
                comboBox9.ValueMember = "Anime_ID";

                DataTable dt2 = write.Select("select * from Mangas  order by Name");
                comboBox8.DataSource = dt2;
                comboBox8.DisplayMember = "Name";
                comboBox8.ValueMember = "Manga_ID";



                DataTable dtt = write.Select("select * from Animes_has_Characters a,Animes b" +
                    " where a.Anime_ID = b.Anime_ID " +
                    "and a.Character_ID = " + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString() + "");
                listBox7.Items.Clear();
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    listBox7.Items.Add(dtt.DefaultView[i]);
                }
                listBox7.ValueMember = "Anime_ID";
                listBox7.DisplayMember = "Name";

                DataTable dttt = write.Select("select * from Mangas_has_Characters a,Mangas b" +
                    " where a.Manga_ID = b.Manga_ID " +
                    "and a.Character_ID = " + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString() + "");
                listBox6.Items.Clear();
                for (int i = 0; i < dttt.Rows.Count; i++)
                {
                    listBox6.Items.Add(dttt.DefaultView[i]);
                }
                listBox6.ValueMember = "Manga_ID";
                listBox6.DisplayMember = "Name";


            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Name cannot be empty!");
            }
            else
            {
                DbConnection read = new DbConnection();

                read.Inserts("update [Characters] set [Name] = '" + textBox3.Text + "' where Character_id = "
                    + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());

                if (updatestr.Count > 0)
                {
                    foreach (string a in updatestr)
                    {
                        MessageBox.Show(a);
                        read.Inserts(a);
                    }

                    updatestr.Clear();
                }
                if (textBox2.Text != "")
                {
                    read.Inserts("update [Characters] set [Description] = '"
                        + textBox2.Text + "' where Character_ID = " +
                        ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());
                }
                if (radioButton3.Checked == true)
                {
                    read.Inserts("update [Characters] set Gender = '" + "M"
                        + "'where Character_ID = " + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());
                }
                else
                {
                    read.Inserts("update [Characters] set Gender = '" + "F"
                        + "' where Character_ID = " + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());
                }
                if (DOB.Text != "")
                {
                    read.Inserts("update Characters set DOB = '" + DOB.Value.ToString() + "' where Character_ID = "
                        + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());
                }
                MessageBox.Show("Update Complete");
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (stbName.Text == "" && comboBox4.Text == "" && comboBox5.Text == "")
            {
                MessageBox.Show("No Criteria Specified");
            }
            else
            {
                string s = "select * from aview3 where 1=1";

                if (stbName.Text != "")
                {
                    s = s + " and [Character_Name] like '%" + stbName.Text + "%'";
                }

                if (rbFemale.Checked == true)
                {
                    s = s + "and [Gender] like '%" + "F" + "%'";

                }
                else
                {
                    s = s + "and [Gender] like '%" + "M" + "%'";
                }

                if (comboBox4.Text != "")
                {
                    s = s + " and [Anime_ID] = '" + comboBox4.SelectedValue.ToString() + "'";
                }
                if (comboBox5.Text != "")
                {
                    s = s + " and[Manga_ID] = '" + comboBox5.SelectedValue.ToString() + "'";
                }
                s = s + "order by [Character_Name]";
                DbConnection read = new DbConnection();
                DataTable dt = read.Select(s);
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Character_Name";
                listBox1.ValueMember = "Character_ID";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                bool check = false;
                int p = listBox2.Items.Count;
                for (int i = 0; i < p; i++)
                {
                    if (((DataRowView)listBox2.Items[i])["Anime_ID"].ToString() == comboBox1.SelectedValue.ToString())
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    MessageBox.Show("Anime already in List");
                }
                else
                {
                    listBox2.Items.Add(comboBox1.SelectedItem);

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
            {
                bool check = false;
                int p = listBox3.Items.Count;
                for (int i = 0; i < p; i++)
                {
                    if (((DataRowView)listBox3.Items[i])["Manga_ID"].ToString() == comboBox2.SelectedValue.ToString())
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    MessageBox.Show("Anime already in List");
                }
                else
                {
                    listBox3.Items.Add(comboBox2.SelectedItem);

                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DOB.Format = DateTimePickerFormat.Short;

        }

        private void DOB_ValueChanged(object sender, EventArgs e)
        {
            DOB.Format = DateTimePickerFormat.Short;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox7.SelectedIndex > -1)
            {

                updatestr.Add("delete from Animes_has_Characters where Anime_ID = "
                    + ((DataRowView)listBox7.SelectedItem)["Anime_ID"].ToString() +
                  " and Character_ID = " + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());
                listBox7.Items.Remove(listBox7.SelectedItem);
                listBox7.SelectedIndex = -1;

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox6.SelectedIndex > -1)
            {

                updatestr.Add("delete from Mangas_has_Characters where Manga_ID = "
                    + ((DataRowView)listBox6.SelectedItem)["Manga_ID"].ToString() +
                  " and Character_ID = " + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());
                listBox6.Items.Remove(listBox6.SelectedItem);
                listBox6.SelectedIndex = -1;

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Permenantly delete this Character ?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DbConnection del = new DbConnection();
                del.Inserts("delete from Characters where [Character_id] = " + ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString());
                panelView.Visible = false;
                listBox1.DataSource = null;
                MessageBox.Show("Character Deleted");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox9.SelectedIndex > -1)
            {
                bool check = false;
                int p = listBox7.Items.Count;
                for (int i = 0; i < p; i++)
                {
                    if (((DataRowView)listBox7.Items[i])["Anime_ID"].ToString() == comboBox9.SelectedValue.ToString())
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    MessageBox.Show("Anime already in List");
                }
                else
                {
                    listBox7.Items.Add(comboBox9.SelectedItem);
                    updatestr.Add("insert into Animes_has_Characters values(" +
                        ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString() + "," +
                        comboBox9.SelectedValue.ToString() + ")");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox8.SelectedIndex > -1)
            {
                bool check = false;
                int p = listBox6.Items.Count;
                for (int i = 0; i < p; i++)
                {
                    if (((DataRowView)listBox6.Items[i])["Manga_ID"].ToString() == comboBox8.SelectedValue.ToString())
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    MessageBox.Show("Manga already in List");
                }
                else
                {
                    listBox6.Items.Add(comboBox8.SelectedItem);
                    updatestr.Add("insert into Mangas_has_Characters values(" +
                        ((DataRowView)listBox1.SelectedItem)["Character_ID"].ToString() + "," +
                        comboBox8.SelectedValue.ToString() + ")");
                }
            }
        }

        private void panelView_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
