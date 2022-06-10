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
    public partial class AddBuildObjectForm : Form
    {
        Model1Container _db;
        BuildObjectsForm backForm;
        public AddBuildObjectForm(Model1Container db, BuildObjectsForm form)
        {
            InitializeComponent();
            this.backForm = form;
            this._db = db;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BuildObject building = new BuildObject()
                {
                    Name = textBox1.Text,
                    Address = textBox2.Text,
                    ContactPerson = textBox3.Text,
                    CustomerNumber = textBox4.Text,
                };


                _db.BuildObjectSet.Add(building);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void AddBuildObjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }
    }
}
