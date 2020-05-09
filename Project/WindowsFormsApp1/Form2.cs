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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 20;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbConnection db = new DbConnection();
            DataTable dt = db.Select("select * from Users where user_name = '" + textBox1.Text+"'");
//            if(dt.Rows.Count == 0 || dt.Rows[0][17].ToString() != textBox2.Text)
  //          {
    //            MessageBox.Show("Incorrect UserName / Password combination");
      //      }
        //    else
            {
                if(checkBox1.Checked == true)
                {
                    if (dt.Rows[0][18].ToString() == "0")
                    {
                        MessageBox.Show("Incorrect UserName / Password combination");
                    }
                    else
                    {
                        DashBoard ds = new DashBoard();
                        this.Hide();
                        ds.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    User u = new User(dt.Rows[0][18].ToString(), textBox1.Text);
                    this.Hide();
                    u.ShowDialog();
                    this.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegForm rg = new RegForm();
            this.Hide();
            rg.ShowDialog();
            this.Show();
        }
    }
}
