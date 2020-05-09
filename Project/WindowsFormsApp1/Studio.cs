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
    public partial class Studio : Form
    {
        string viewId;
        List<string> updateString = new List<string>();
        public Studio()
        {
            InitializeComponent();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = true;
            panelSearch.Visible = false;
            panelView.Visible = false;
        }

        private void Studio_Load(object sender, EventArgs e)
        {
            panelAdd.Location = new Point(202, 120);
            panelAdd.Visible = false;
            panelSearch.Visible = false;
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bView_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                updateString.Clear();
                panelAdd.Visible = false;
                panelSearch.Visible = false;
                panelView.Visible = true;
                panelView.Location = new Point(202, 120);

                DbConnection read = new DbConnection();
                string a = listBox1.SelectedValue.ToString();
                viewId = a;
                DataTable dt = read.Select("select [Studio].[Studio_ID], [Studio].[Name] as SName,[Animes].Anime_ID, [Animes].[Name] from [Studio], [Animes_has_Studio],[Animes] where [Studio].Studio_ID = [Animes_has_Studio].Studio_ID and [Animes_has_Studio].Anime_ID = [Animes].Anime_ID and [Animes_has_Studio].Studio_ID = " + a +" order by Name");
                DbConnection read2 = new DbConnection();
                DataTable dt2 = read2.Select("select Studio_ID, Name from [Studio] where Studio_ID = " + a);
                textBox2.Text = dt2.Rows[0][1].ToString();
                DbConnection read3 = new DbConnection();
                DataTable dt3 = read3.Select("select * from Animes order by Name");
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Anime_ID";
                listBox2.Items.Clear();
                for(int i = 0; i < dt.Rows.Count; i ++)
                {
                    listBox2.Items.Add(dt.DefaultView[i]);
                }

                listBox2.DisplayMember = "Name";
                listBox2.ValueMember = "Anime_ID";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("No Name of Studio");
            }
            else
            {
                DbConnection write = new DbConnection();
                write.Inserts("Insert into Studio([Name]) values('" + tbName.Text + "')");
                tbName.Text = "";
                MessageBox.Show("Studio Added Succesfully");
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (stbName.Text != "")
            {
                DbConnection read = new DbConnection();
                DataTable tRead = read.Select("select * from Studio where [Name] like '%" + stbName.Text + "%' order by Name");
                listBox1.DataSource = tRead;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Studio_ID";
            }
            else
            {
                MessageBox.Show("No Search Criteria");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "")
            {
                MessageBox.Show("Name cannnot be empty");
            }
            else
            {
                DbConnection db = new DbConnection();
                db.Inserts("update [studio] set [Name] = '" + textBox2.Text + "' where studio_id = " + viewId);
                if (updateString.Count > 0)
                {
                    foreach (string a in updateString)
                    {
                        db.Inserts(a);
                    }
                    updateString.Clear();

                    /*    DbConnection read = new DbConnection();
                        DataTable dt = read.Select("select [Studio].[Studio_ID], [Studio].[Name] as SName,[Animes].Anime_ID, [Animes].[Name] from [Studio], [Animes_has_Studio],[Animes] where [Studio].Studio_ID = [Animes_has_Studio].Studio_ID and [Animes_has_Studio].Anime_ID = [Animes].Anime_ID and [Animes_has_Studio].Studio_ID = " + viewId);
                        listBox2.DataSource = dt; */

                }
                MessageBox.Show("Applied !");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool check = false;
            int p = listBox2.Items.Count;
            for (int i = 0; i<p; i++)
            {
                if (((DataRowView)listBox2.Items[i])["Anime_ID"].ToString() == comboBox1.SelectedValue.ToString())
                {
                    check = true;
                    break;
                }
            }
            if(check)
            {
                MessageBox.Show("Anime already in List");
            }
            else
            {
               // listBox2.DataBindings.Add((DataBinding)comboBox1.Items[comboBox1.SelectedIndex]);
//                MessageBox.Show(viewId + " a " + comboBox1.SelectedValue.ToString());
                updateString.Add("insert into Animes_has_Studio values(" + comboBox1.SelectedValue.ToString() + "," + viewId + ")");
                listBox2.Items.Add(comboBox1.SelectedItem);
                MessageBox.Show("Anime Queued for addition!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox2.SelectedIndex > -1)
            { 
                updateString.Add("delete from Animes_has_Studio where Animes_has_Studio.Anime_ID = " + ((DataRowView)listBox2.Items[listBox2.SelectedIndex])["Anime_ID"].ToString() + " and Animes_has_Studio.Studio_ID = " + viewId);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                listBox2.SelectedIndex = -1;
                MessageBox.Show("Anime queued for deletion!");
            }
            else
            {
                MessageBox.Show("No item selected");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Permenantly delete this studio ?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DbConnection del = new DbConnection();
                del.Inserts("delete from Studio where Studio.Studio_ID = " + viewId);
                panelView.Visible = false;
                listBox1.DataSource = null;
                MessageBox.Show("Studio Deleted");
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
