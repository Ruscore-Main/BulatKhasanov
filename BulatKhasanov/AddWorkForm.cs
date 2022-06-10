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
    public partial class AddWorkForm : Form
    {
        Model1Container _db;
        WorkPlanForm backForm;
        public AddWorkForm(Model1Container db, WorkPlanForm form)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
        }

        private void AddWorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Work work = new Work()
                {
                    WorkName = textBox13.Text,
                    Price = Convert.ToInt32(textBox14.Text),
                    Scale = Convert.ToInt32(textBox8.Text),
                    IsDone = false,
                    Plan = comboBox1.SelectedItem as Plan,
                    Employee = comboBox2.SelectedItem as Employee
                };


                _db.WorkSet.Add(work);
                _db.SaveChanges();
                backForm.UpdateLists();
                backForm.UpdateWorkList();
                MessageBox.Show("Успешно добавлено!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных");
            }
        }

        private void AddWorkForm_Load(object sender, EventArgs e)
        {
            foreach (Plan plan in _db.PlanSet)
            {
                comboBox1.Items.Add(plan);
            }

            foreach (Employee employee in _db.EmployeeSet)
            {
                comboBox2.Items.Add(employee);
            }
        }
    }
}
