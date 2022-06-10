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
    public partial class BuildObjectsForm : Form
    {
        Model1Container _db;
        Form backForm;
        User currentUser;
        BuildObject currentBuilding;
        string search = "";
        public BuildObjectsForm(Model1Container db, Form form, User user)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
            this.currentUser = user;
        }

        private void BuildObjectsForm_Load(object sender, EventArgs e)
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
            foreach (BuildObject building in _db.BuildObjectSet)
            {
                if (building.Name.Contains(search))
                {
                    listBox1.Items.Add(building);
                }
            }
        }

        private void BuildObjectsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentBuilding = listBox1.SelectedItem as BuildObject;
                if (currentBuilding != null)
                {
                    textBox1.Text = currentBuilding.Name;
                    textBox2.Text = currentBuilding.Address;
                    textBox3.Text = currentBuilding.ContactPerson;
                    textBox4.Text = currentBuilding.CustomerNumber;
                    if (currentUser.Post == "Директор") {
                        button2.Enabled = true;
                        button3.Enabled = true;
                    }
                }
            }
            catch { }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            search = (sender as TextBox).Text;
            UpdateList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddBuildObjectForm addBuildingForm = new AddBuildObjectForm(_db, this);
            addBuildingForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                _db.BuildObjectSet.Remove(currentBuilding);
                _db.SaveChanges();
                UpdateList();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                currentBuilding = null;
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
                currentBuilding.Name = textBox1.Text;
                currentBuilding.Address = textBox2.Text;
                currentBuilding.ContactPerson = textBox3.Text;
                currentBuilding.CustomerNumber = textBox4.Text;

                _db.SaveChanges();
                UpdateList();
                MessageBox.Show("Успешно изменено!");
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных!");
            }
        }
    }
}
