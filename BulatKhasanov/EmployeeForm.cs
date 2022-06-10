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
    public partial class Form6 : Form
    {
        Model1Container _db;
        Form backForm;
        User currentUser;
        Employee currentEmployee;
        string search = "";
        public Form6(Model1Container db, Form form, User user)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
            this.currentUser = user;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            UpdateList();
            if (currentUser.Post != "Директор")
            {
                button1.Enabled = false;
            }
            button2.Enabled = false;
            button3.Enabled = false;
        }

        public void UpdateList()
        {
            listBox1.Items.Clear();
            foreach (Employee employee in _db.EmployeeSet)
            {
                if (employee.Surname.Contains(search))
                {
                    listBox1.Items.Add(employee);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentEmployee = listBox1.SelectedItem as Employee;
                if (currentEmployee != null)
                {
                    textBox1.Text = currentEmployee.Name;
                    textBox2.Text = currentEmployee.Surname;
                    textBox3.Text = currentEmployee.Patronymic;
                    textBox4.Text = currentEmployee.DateOfBirth;
                    textBox7.Text = currentEmployee.Classification;
                    textBox6.Text = currentEmployee.PhoneNumber;

                    if (currentUser.Post == "Директор")
                    {
                        button2.Enabled = true;
                        button3.Enabled = true;
                    }
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                _db.EmployeeSet.Remove(currentEmployee);
                _db.SaveChanges();
                UpdateList();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox7.Text = "";
                textBox6.Text = "";
                currentEmployee = null;
                button2.Enabled = false;
                button3.Enabled = false;
                MessageBox.Show("Успешно удалено!");
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                currentEmployee.Name = textBox1.Text;
                currentEmployee.Surname = textBox2.Text;
                currentEmployee.Patronymic = textBox3.Text;
                currentEmployee.DateOfBirth = textBox4.Text;
                currentEmployee.PhoneNumber = textBox6.Text;
                currentEmployee.Classification = textBox7.Text;
                _db.SaveChanges();
                UpdateList();
                MessageBox.Show("Успешно изменено!");
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных!");
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            search = (sender as TextBox).Text;
            UpdateList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployeeForm addEmployeeForm = new AddEmployeeForm(_db, this);
            addEmployeeForm.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }
    }
}
