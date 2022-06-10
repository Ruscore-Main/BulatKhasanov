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
    public partial class RegistrationForm : Form
    {
        Model1Container _db;
        Form backForm;
        public RegistrationForm(Model1Container db, Form form)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var validateResult = ValidateTest.ValidateRegistration(textBox4.Text, textBox5.Text, textBox1.Text, textBox2.Text);
            if (validateResult == "Успешно")
            {
                User admin = new User()
                {
                    Name = textBox1.Text,
                    Surname = textBox2.Text,
                    Patronymic = textBox3.Text,
                    Login = textBox4.Text,
                    Password = textBox5.Text,
                    Post = Convert.ToString(comboBox1.SelectedItem)
                };

                _db.UserSet.Add(admin);
                _db.SaveChanges();
                MessageBox.Show("Успешная регистрация!");
                this.Close();
            }
            else
            {
                MessageBox.Show(validateResult);
            }
            
        }

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }
    }
}
