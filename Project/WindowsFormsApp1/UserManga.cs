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
    public partial class UserManga : Form
    {
        public UserManga()
        {
            InitializeComponent();
            manga_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserManga_Load(object sender, EventArgs e)
        {
            DbConnection db = new DbConnection();
            DataTable ans = db.Select("select manga_id, (select [Name] from Mangas where Manga_ID = wa.Manga_ID) as [Name]," +
                "wa.Status, wa.Rating, wa.Favourite from Read_Mangas as wa where wa.User_Name = '" + User.userName + "' order by [name]");
            manga_list.DataSource = ans;
            manga_list.Columns[0].Visible = false;
            manga_list.ReadOnly = true;
        }

        private void manga_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(manga_list.SelectedRows[0].Cells[0].Value.ToString());
            UserSearchMangas a = new UserSearchMangas(manga_list.SelectedRows[0].Cells[0].Value.ToString());
            this.Hide();
            a.ShowDialog();
            this.Show();
            DbConnection db = new DbConnection();
            DataTable ans = db.Select("select manga_id, (select [Name] from Mangas where Manga_ID = wa.Manga_ID) as [Name]," +
                "wa.Status, wa.Rating, wa.Favourite from Read_Mangas as wa where wa.User_Name = '" + User.userName + "' order by [name]");
            manga_list.DataSource = ans;
            manga_list.Columns[0].Visible = false;
            manga_list.ReadOnly = true;

        }
    }
}
