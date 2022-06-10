using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulatKhasanov
{
    public partial class Form1 : Form
    {
        Model1Container _db = new Model1Container();
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm(_db, this);
            registrationForm.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = _db.UserSet.FirstOrDefault(el => el.Login == textBox1.Text && el.Password == textBox4.Text);
            if (user != null)
            {
                MenuForm menuForm = new MenuForm(_db, this, user);
                this.Hide();
                menuForm.Show();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
