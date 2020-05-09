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
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox7.PasswordChar = '*';
            textBox2.MaxLength = 20;
            textBox8.PasswordChar = '*';
            textBox8.MaxLength = 20;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Name cannot be empty");
            }
            else if(textBox2.Text == "")
            {
                MessageBox.Show("UserName cannot be empty");

            }
            else if(textBox7.Text == "" || textBox8.Text == "" || textBox7.Text != textBox8.Text)
            {
                MessageBox.Show("Password Mismatch");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Gender cannot be empty");
            }
            else if (dateTimePicker1.Value.ToString("MM/yy/dd") == DateTime.Today.ToString("MM/yy/dd"))
            {
                MessageBox.Show("DOB must be less than todays date");
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Location cannot be empty");
            }
            else
            {
                DbConnection db = new DbConnection();
                DataTable dt = db.Select("select * from users where user_name =  '" + textBox2.Text+"'");
                if(dt.Rows.Count > 0)
                {
                    MessageBox.Show("UserName already registered");

                }
                else
                {
                    db.Inserts("insert into users(user_name, name, gender, DOB, location, about, date_of_join," +
                        "Password, isAdmin) values('"+textBox2.Text+"','"+textBox1.Text+"','"+textBox3.Text+"',Convert(datetime,'"
                        +dateTimePicker1.Value.ToString("MM/dd/yy")+"'), '"+textBox5.Text+"','"+textBox6.Text+"',convert(datetime,'"+
                        DateTime.Today.ToString()+"',103),'"+textBox7.Text+"',0)");
                    MessageBox.Show("Registered Succesfully");
                    this.Close();
                }
            }
        }
    }
}
