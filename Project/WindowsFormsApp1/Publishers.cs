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
    public partial class Publishers : Form
    {
        string viewId;
        List<string> updateString = new List<string>();

        public Publishers()
        {
            InitializeComponent();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
            panelView.Visible = false;

            panelAdd.Visible = true;
        }

        private void Publishers_Load(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
            panelAdd.Visible = false;
            panelView.Location = new Point(198, 112);
            panelView.Visible = false;
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            panelSearch.Visible = true;
            panelView.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bView_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1)
            {
                panelAdd.Visible = false;
                panelSearch.Visible = false;
                panelView.Visible = true;
                DbConnection read = new DbConnection();
                string a = listBox1.SelectedValue.ToString();
                viewId = a;
                DataTable dt = read.Select("select [Publishers].[Publisher_ID], [Publishers].[Name] as PName,[Mangas].Manga_ID, [Mangas].[Name] from [Publishers], [Published_By],[Mangas] where [Publishers].Publisher_ID = [Published_By].Publisher_ID and [Published_By].Manga_ID = [Mangas].Manga_ID and [Published_By].Publisher_ID = " + a+"order by Name");
                DbConnection read2 = new DbConnection();
                DataTable dt2 = read2.Select("select * from [Publishers] where Publisher_ID = " + a);
                textBox1.Text = dt2.Rows[0][2].ToString();
                textBox2.Text = dt2.Rows[0][1].ToString();
                DbConnection read3 = new DbConnection();
                DataTable dt3 = read3.Select("select * from Mangas order  by Name");
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Manga_ID";
                listBox2.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBox2.Items.Add(dt.DefaultView[i]);
                }
                listBox2.DisplayMember = "Name";
                listBox2.ValueMember = "Manga_ID";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("No Publisher Name");
            }
            else
            {
                DbConnection write = new DbConnection();
                write.Inserts("insert into Publishers([Name], [Based_In]) values('" + tbName.Text + "', '" + tbBasedIn.Text + "')");
                tbName.Text = "";
                tbBasedIn.Text = "";
                MessageBox.Show("Publisher Added Succesfully");
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (stbName.Text == "" && stbBasedIn.Text == "")
            {
                MessageBox.Show("No Criteria Specified");
            }
            else
            {
                string s = "select * from Publishers where 1=1";
                if (stbName.Text != "")
                {
                    s = s + " and [Name] like '%" + stbName.Text + "%'";
                }
                if (stbBasedIn.Text != "")
                {
                    s = s + " and [Based_In] like '%" + stbBasedIn.Text + "%'";
                }
                s = s + " order by [Publishers].Name";
                DbConnection read = new DbConnection();
                DataTable dt = read.Select(s);
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Publisher_ID";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool check = false;
            int p = listBox2.Items.Count;
            for (int i = 0; i < p; i++)
            {
                if (((DataRowView)listBox2.Items[i])["Manga_ID"].ToString() == comboBox1.SelectedValue.ToString())
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
                // listBox2.DataBindings.Add((DataBinding)comboBox1.Items[comboBox1.SelectedIndex]);
                //                MessageBox.Show(viewId + " a " + comboBox1.SelectedValue.ToString());
                updateString.Add("insert into Published_By values("+viewId+","+ comboBox1.SelectedValue.ToString() + ")");
                listBox2.Items.Add(comboBox1.SelectedItem);
                MessageBox.Show("Manga Queued for addition!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {
                updateString.Add("delete from Published_by where Published_by.Manga_ID = " + ((DataRowView)listBox2.Items[listBox2.SelectedIndex])["Manga_ID"].ToString() + " and Published_By.Publisher_ID = " + viewId);
                MessageBox.Show("Manga queued for deletion!");
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                listBox2.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("No item selected");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Name cannot be empty!");
            }
            else
            {
                DbConnection read = new DbConnection();
                read.Inserts("update [publishers] set [Name] = '" + textBox1.Text + "' where publisher_id = " + viewId);
                if (updateString.Count > 0)
                {
                    foreach (string a in updateString)
                    {
                        read.Inserts(a);
                    }
                /*    DataTable dt = read.Select("select [Publishers].[Publisher_ID], [Publishers].[Name] as PName,[Mangas].Manga_ID, [Mangas].[Name] from [Publishers], [Published_By],[Mangas] where [Publishers].Publisher_ID = [Published_By].Publisher_ID and [Published_By].Manga_ID = [Mangas].Manga_ID and [Published_By].Publisher_ID = " + viewId);
                    listBox2.DataSource = dt; */
                    updateString.Clear();
                }
                if(textBox2.Text != "")
                {
                    read.Inserts("update [publishers] set [Based_in] = '"+textBox2.Text+"' where publisher_id = "+viewId);
                }
                MessageBox.Show("Update Complete");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Permenantly delete this Publisher ?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DbConnection del = new DbConnection();
                del.Inserts("delete from Publishers where [publisher_id] = " + viewId);
                panelView.Visible = false;
                listBox1.DataSource = null;
                MessageBox.Show("Publisher Deleted");
            }
        }
    }
}
