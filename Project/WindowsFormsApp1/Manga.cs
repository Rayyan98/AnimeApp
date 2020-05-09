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

    public partial class Manga : Form
    {
        List<string> updatestr = new List<string>();
        public Manga()
        {
            InitializeComponent();
            dateTimePicker3.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            bool check = false;
            int p = studio_listbox.Items.Count;
            for (int i = 0; i < p; i++)
            {
                if (((DataRowView)studio_listbox.Items[i])["Publisher_ID"].ToString() == studio_combobox.SelectedValue.ToString())
                {
                    check = true;
                    break;
                }
            }
            if (check)
            {
                MessageBox.Show("Publisher already in List");
            }
            else
            {
                studio_listbox.Items.Add(studio_combobox.SelectedItem);
            }

        }


        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            studio_listbox.ValueMember = "Publisher_ID";
            studio_listbox.DisplayMember = "Name";
            sequel_listbox.ValueMember = "Manga_ID";
            sequel_listbox.DisplayMember = "Name";
            basedin_listbox.ValueMember = "Anime_ID";
            basedin_listbox.DisplayMember = "Name";
            panelAdd.Visible = true;
            panelSearch.Visible = false;
            panelView.Visible = false;
            DbConnection read = new DbConnection();
            DataTable dt = read.Select("select * from Mangas");
            seq_combobox.DataSource = dt;
            seq_combobox.DisplayMember = "Name";
            seq_combobox.ValueMember = "Manga_ID";
            DbConnection read1 = new DbConnection();
            DataTable dt1 = read1.Select("select * from Publishers");
            studio_combobox.DataSource = dt1;
            studio_combobox.DisplayMember = "Name";
            studio_combobox.ValueMember = "Publisher_ID";
            DbConnection read2 = new DbConnection();
            DataTable dt2 = read2.Select("select * from Animes");
            basedin_combobox.DataSource = dt2;
            basedin_combobox.DisplayMember = "Name";
            basedin_combobox.ValueMember = "Anime_ID";
            DbConnection read3 = new DbConnection();
            DataTable dt3 = read.Select("Select * from Genres");
            add_category_combobox.DataSource = dt3;
            add_category_combobox.DisplayMember = "Name";
            add_category_combobox.ValueMember = "Genre_ID";
            DOB.Format = DateTimePickerFormat.Custom;
            DOB.CustomFormat = " ";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";


        }


        private void Manga_Load(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            panelSearch.Visible = false;
            panelView.Visible = false;
            panelSearch.Location = new Point(202, 114);
            panelView.Location = new Point(202, 114);
            panelAdd.Location = new Point(202, 114);
        }

        private void bSearch_Click_1(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            panelView.Visible = false;
            panelSearch.Visible = true;
            DbConnection read = new DbConnection();
            DataTable dt1 = read.Select("select * from Genres");
            DataTable dt2 = read.Select("select * from Publishers");
            comboBox1.DataSource = dt1;
            comboBox2.DataSource = dt2;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Genre_ID";
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Publisher_ID";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (search_listbox.SelectedIndex > -1)
            {
                panelAdd.Visible = false;
                panelSearch.Visible = false;
                panelView.Visible = true;
                updatestr.Clear();
                view_name.Text = ((DataRowView)search_listbox.SelectedItem)["Manga_Name"].ToString();
                dateTimePicker3.Value = System.Convert.ToDateTime(((DataRowView)search_listbox.SelectedItem)["Publishing_Date"]);
                dateTimePicker2.Value = System.Convert.ToDateTime(((DataRowView)search_listbox.SelectedItem)["Date_Finished"]);
                DbConnection write = new DbConnection();
                DataTable dt = write.Select("Select * from Genres");
                view_category_combobox.DataSource = dt;
                view_category_combobox.DisplayMember = "Name";
                view_category_combobox.ValueMember = "Genre_ID";
                textBox3.Text = ((DataRowView)search_listbox.SelectedItem)["Volumes"].ToString();


                DataTable dt1 = write.Select("select * from Mangas order by Name");
                seq_combobox_view.DataSource = dt1;
                seq_combobox_view.DisplayMember = "Name";
                seq_combobox_view.ValueMember = "Manga_ID";
                DbConnection read1 = new DbConnection();
                DataTable dt2 = read1.Select("select * from Publishers  order by Name");
                studio_combobox_view.DataSource = dt2;
                studio_combobox_view.DisplayMember = "Name";
                studio_combobox_view.ValueMember = "Publisher_ID";
                DbConnection read2 = new DbConnection();
                DataTable dt3 = read2.Select("select * from Animes  order by Name");
                basedin_combobox_view.DataSource = dt3;
                basedin_combobox_view.DisplayMember = "Name";
                basedin_combobox_view.ValueMember = "Anime_ID";

                DataTable dtt = read1.Select("select * from Manga_Sequel a,Mangas b" +
                    " where a.Manga_ID2 = b.Manga_ID " +
                    "and a.Manga_ID1 = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString() + "");
                sequels_listbox_view.Items.Clear();
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    sequels_listbox_view.Items.Add(dtt.DefaultView[i]);
                }
                sequels_listbox_view.ValueMember = "Manga_ID";
                sequels_listbox_view.DisplayMember = "Name";
               
                DataTable dttt = read1.Select("select * from Published_By a,Publishers b" +
                    " where a.Publisher_ID = b.Publisher_ID " +
                    "and a.Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString() + "");
                studio_listbox_view.Items.Clear();
                for (int i = 0; i < dttt.Rows.Count; i++)
                {
                    studio_listbox_view.Items.Add(dttt.DefaultView[i]);
                }
                studio_listbox_view.ValueMember = "Publisher_ID";
                studio_listbox_view.DisplayMember = "Name";

                DataTable dtttt = read1.Select("select * from Anime_based_Manga a,Mangas b" +
                    " where a.Manga_ID = b.Manga_ID " +
                    "and a.Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString() + "");
                basedin_listbox_view.Items.Clear();
                for (int i = 0; i < dtttt.Rows.Count; i++)
                {
                    basedin_listbox_view.Items.Add(dtttt.DefaultView[i]);
                }
                basedin_listbox_view.ValueMember = "Anime_ID";
                basedin_listbox_view.DisplayMember = "Name";
            }
        }

        private void panelAdd_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void panelView_Paint(object sender, PaintEventArgs e)
        {

        }

        private void but_Search_Click(object sender, EventArgs e)
        {
            if (search_name.Text == "" && comboBox1.Text == "" && comboBox2.Text == "")
            {
                MessageBox.Show("No Criteria Specified");
            }
            else
            {
                string s = "select * from aview2 where 1=1";

                if (search_name.Text != "")
                {
                    s = s + " and [Manga_Name] like '%" + search_name.Text + "%'";
                }
                if (comboBox1.Text != "")
                {
                    s = s + " and [Genre_ID] = '" + comboBox1.SelectedValue.ToString() + "'";
                }
                if (comboBox2.Text != "")
                {
                    s = s + " and[Publisher_ID] = '" + comboBox2.SelectedValue.ToString() + "'";
                }
                s = s + "order by [Manga_Name]";
                DbConnection read = new DbConnection();
                DataTable dt = read.Select(s);
                search_listbox.DataSource = dt;
                search_listbox.DisplayMember = "Manga_Name";
                search_listbox.ValueMember = "Manga_ID";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (add_name.Text == "")
            {
                MessageBox.Show("No Name of Manga");
            }
            else
            {
                DbConnection write = new DbConnection();
                string s1 = "Insert into Mangas([Name]";
                string s2 = " values('" + add_name.Text + "'";
                string s3 = ")";


                if (episodenumber_add.Text != "")
                {
                    s1 += ",[Volumes]";
                    s2 += ",'" + episodenumber_add.Text + "'";
                }
                if (DOB.Format == DateTimePickerFormat.Short)
                {
                    s1 += ",[Publishing_Date]";
                    s2 += ",'" + DOB.Value.ToString() + "'";
                }
                if (dateTimePicker1.Format == DateTimePickerFormat.Short)
                {
                    s1 += ",[Date_Finished]";
                    s2 += ",'" + dateTimePicker1.Value.ToString() + "'";
                }
                string s = s1 + ")" + s2 + s3;
                write.Inserts(s);
                DataTable dt3 = write.Select("Select top 1 Manga_ID from Mangas order by Manga_ID desc");
                if (add_category_combobox.Text != "")
                {
                    write.Inserts("insert into Mangas_has_Genres(Genre_ID,Manga_ID) values(" + add_category_combobox.SelectedValue.ToString() + "," + dt3.Rows[0][0].ToString() + ")");
                }

                for (int a = 0; a < sequel_listbox.Items.Count; a++)
                {
                    write.Inserts("Insert into Manga_Sequel(Manga_ID1,Manga_ID2,Types_of_Sequel) values(" + dt3.Rows[0][0].ToString() + "," + (((DataRowView)sequel_listbox.Items[a])["Manga_ID"]).ToString() + ",0)");
                }

                for (int a = 0; a < studio_listbox.Items.Count; a++)
                {
                    write.Inserts("Insert into Published_By(Manga_ID,Publisher_ID) values(" + dt3.Rows[0][0].ToString() + "," + (((DataRowView)studio_listbox.Items[a])["Publisher_ID"]).ToString() + ")");
                }
                for (int a = 0; a < basedin_listbox.Items.Count; a++)
                {
                    write.Inserts("Insert into Anime_based_Manga(Manga_ID,Anime_ID) values(" + dt3.Rows[0][0].ToString() + "," + (((DataRowView)basedin_listbox.Items[a])["Anime_ID"]).ToString() + ")");
                }

                add_name.Text = "";
                MessageBox.Show("Manga Added Succesfully");
            }
        }

        private void but_Delete_Click(object sender, EventArgs e)
        {

        }

        private void but_Update_Click(object sender, EventArgs e)
        {

        }

        private void episodenumber_add_TextChanged(object sender, EventArgs e)
        {

        }

        private void seq_add_add_Click(object sender, EventArgs e)
        {
            bool check = false;
            int p = sequel_listbox.Items.Count;
            for (int i = 0; i < p; i++)
            {
                if (((DataRowView)sequel_listbox.Items[i])["Manga_ID"].ToString() == seq_combobox.SelectedValue.ToString())
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
                sequel_listbox.Items.Add(seq_combobox.SelectedItem);
            }

        }

        private void basedin_add_add_Click(object sender, EventArgs e)
        {
            bool check = false;
            int p = basedin_listbox.Items.Count;
            for (int i = 0; i < p; i++)
            {
                if (((DataRowView)basedin_listbox.Items[i])["Anime_ID"].ToString() == basedin_combobox.SelectedValue.ToString())
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
                basedin_listbox.Items.Add(basedin_combobox.SelectedItem);
            }

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void DOB_ValueChanged(object sender, EventArgs e)
        {
            DOB.Format = DateTimePickerFormat.Short;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Short;
        }

        private void panelSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void add_category_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void search_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void view_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void seq_add_view_Click(object sender, EventArgs e)
        {
            if (seq_combobox_view.SelectedIndex > -1)
            {
                bool check = false;
                int p = sequels_listbox_view.Items.Count;
                for (int i = 0; i < p; i++)
                {
                    if (((DataRowView)sequels_listbox_view.Items[i])["Manga_ID"].ToString() == seq_combobox_view.SelectedValue.ToString())
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
                    sequels_listbox_view.Items.Add(seq_combobox_view.SelectedItem);
                    updatestr.Add("insert into Manga_Sequel values(" +
                        ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString() + "," + 
                        seq_combobox_view.SelectedValue.ToString() + ",0)");
                }
            }
        }

        private void studio_add_view_Click(object sender, EventArgs e)
        {
            if (studio_combobox_view.SelectedIndex > -1)
            {
                bool check = false;
                int p = studio_listbox_view.Items.Count;
                for (int i = 0; i < p; i++)
                {
                    if (((DataRowView)studio_listbox_view.Items[i])["Publisher_ID"].ToString() == studio_combobox_view.SelectedValue.ToString())
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    MessageBox.Show("Publisher already in List");
                }
                else
                {
                    studio_listbox_view.Items.Add(studio_combobox_view.SelectedItem);
                    updatestr.Add("insert into Published_By(Manga_ID,Publisher_ID) " +
                        "values(" + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString() + ","
                        + studio_combobox_view.SelectedValue.ToString() + ") ");
                }
            }
        }

        private void basedin_add_view_Click(object sender, EventArgs e)
        {
            if (basedin_combobox_view.SelectedIndex > -1)
            {
                bool check = false;
                int p = basedin_listbox_view.Items.Count;
                for (int i = 0; i < p; i++)
                {
                    if (((DataRowView)basedin_listbox_view.Items[i])["Anime_ID"].ToString() == basedin_combobox_view.SelectedValue.ToString())
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
                    basedin_listbox_view.Items.Add(basedin_combobox_view.SelectedItem);
                    updatestr.Add("insert into Anime_based_Manga(Manga_ID,Anime_ID) " +
                        "values(" + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString() + ", " 
                        + basedin_combobox_view.SelectedValue.ToString() + ")");
                }
            }

        }

        private void seq_delete_Click(object sender, EventArgs e)
        {
            if (sequels_listbox_view.SelectedIndex > -1)
            {
                
                updatestr.Add("delete from Manga_Sequel where Manga_ID2 = " 
                    + ((DataRowView)sequels_listbox_view.SelectedItem)["Manga_ID"].ToString() +
                  " and Manga_ID1 = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                sequels_listbox_view.Items.Remove(sequels_listbox_view.SelectedItem);
                sequels_listbox_view.SelectedIndex = -1;

            }
        }

        private void studio_delete_Click(object sender, EventArgs e)
        {
            if (studio_listbox_view.SelectedIndex > -1)
            {
                updatestr.Add("delete from Published_By where Publisher_ID = " +
                    ((DataRowView)studio_listbox_view.SelectedItem)["Publisher_ID"].ToString()
                     + " and Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                studio_listbox_view.Items.Remove(studio_listbox_view.SelectedItem);
                
                studio_listbox_view.SelectedIndex = -1;
            }
             
        }

        private void basedin_delete_Click(object sender, EventArgs e)
        {
            if (basedin_listbox_view.SelectedIndex > -1)
            {
                updatestr.Add("delete from Anime_based_Manga where" +
                    " Anime_ID = " + ((DataRowView)basedin_combobox_view.SelectedItem)["Anime_ID"].ToString()
                    + " and Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                basedin_listbox_view.Items.Remove(basedin_listbox_view.SelectedItem);
                basedin_listbox_view.SelectedIndex = -1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Permenantly delete this Manga ?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DbConnection del = new DbConnection();
                del.Inserts("delete from Manga_sequel where [Manga_id1] = "
                    + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString()
                    + " or [Manga_id2] = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                del.Inserts("delete from Mangas where [Manga_id] = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                panelView.Visible = false;
                search_listbox.DataSource = null;
                MessageBox.Show("Manga Deleted");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (view_name.Text == "")
            {
                MessageBox.Show("Name cannot be empty!");
            }
            else
            {
                DbConnection read = new DbConnection();
                read.Inserts("update [Mangas] set [Name] = '" + view_name.Text + "' where Manga_id = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
             
                if (updatestr.Count > 0)
                {
                    foreach (string a in updatestr)
                    {
                        read.Inserts(a);
                    }

                    updatestr.Clear();
                }
                if (textBox3.Text != "")
                {
                    read.Inserts("update [Mangas] set [Volumes] = " + textBox3.Text + " where Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                }
                if (view_category_combobox.Text != "")
                {
                    read.Inserts("update Mangas_has_Genres set Genre_ID = " + view_category_combobox.SelectedValue + "where Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                }
                if (dateTimePicker3.Text != "")
                {
                    read.Inserts("update Mangas set Publishing_Date = '" + dateTimePicker3.Value.ToString() + "' where Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                }
                if (dateTimePicker2.Text != "")
                {
                    read.Inserts("update Mangas set Date_Finished =  '" + dateTimePicker2.Value.ToString() + "' where Manga_ID = " + ((DataRowView)search_listbox.SelectedItem)["Manga_ID"].ToString());
                }
                MessageBox.Show("Update Complete");
            }
        }

        private void search_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
