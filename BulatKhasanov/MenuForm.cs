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
    public partial class MenuForm : Form
    {
        Model1Container _db;
        Form backForm;
        User currentUser;
        public MenuForm(Model1Container db, Form form, User user)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
            this.currentUser = user;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            label1.Text = $"ФИО: {currentUser.Surname} {currentUser.Name} {currentUser.Patronymic}";
            label2.Text = $"Должность: {currentUser.Post}";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }

        private void button1_Click(object sender, EventArgs e) // Build objects form
        {
            BuildObjectsForm buildObjectsForm = new BuildObjectsForm(_db, this, currentUser);
            buildObjectsForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e) // Employee Form
        {
            Form6 employeeForm = new Form6(_db, this, currentUser);
            employeeForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WorkPlanForm workPlanForm = new WorkPlanForm(_db, this, currentUser);
            workPlanForm.Show();
            this.Hide();
        }
    }
}
