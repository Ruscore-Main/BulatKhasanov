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
    public partial class AddEmployeeForm : Form
    {
        Model1Container _db;
        Form6 backForm;

        public AddEmployeeForm(Model1Container db, Form6 form)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void AddEmployeeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Employee employee = new Employee()
                {
                    Name = textBox1.Text,
                    Surname = textBox2.Text,
                    Patronymic = textBox3.Text,
                    DateOfBirth = textBox4.Text,
                    Classification = textBox5.Text,
                    PhoneNumber = textBox6.Text
                };


                _db.EmployeeSet.Add(employee);
                _db.SaveChanges();
                backForm.UpdateList();
                MessageBox.Show("Успешно добавлено!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных");
            }
        }
    }
}
