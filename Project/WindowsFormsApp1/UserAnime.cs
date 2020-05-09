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
    public partial class UserAnime : Form
    {
        public UserAnime()
        {
            InitializeComponent();
            anime_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void UserAnime_Load(object sender, EventArgs e)
        {
            DbConnection db = new DbConnection();
            DataTable ans =  db.Select("select wa.Anime_ID, (select [Name] from Animes where Anime_ID = wa.Anime_ID) as [Name],"+
                "wa.Status, wa.Rating, wa.Favourite from Watched_Animes as wa where wa.User_Name = '"+User.userName+"' order by [name]");
            anime_list.DataSource = ans;
            anime_list.Columns[0].Visible = false;
            anime_list.ReadOnly = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void anime_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(anime_list.SelectedRows[0].Cells[0].Value.ToString());
            UserSearchAnimes a = new UserSearchAnimes(anime_list.SelectedRows[0].Cells[0].Value.ToString());
            this.Hide();
            a.ShowDialog();
            this.Show();
            DbConnection db = new DbConnection();
            DataTable ans = db.Select("select wa.Anime_ID, (select [Name] from Animes where Anime_ID = wa.Anime_ID) as [Name]," +
                "wa.Status, wa.Rating, wa.Favourite from Watched_Animes as wa where wa.User_Name = '" + User.userName + "' order by [name]");
            anime_list.DataSource = ans;
            anime_list.Columns[0].Visible = false;
            anime_list.ReadOnly = true;
        }
    }
}
