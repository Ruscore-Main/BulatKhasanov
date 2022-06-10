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
    public partial class AddPlanForm : Form
    {
        Model1Container _db;
        WorkPlanForm backForm;
        public AddPlanForm(Model1Container db, WorkPlanForm form)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Plan plan = new Plan()
                {
                    ContractNumber = Convert.ToInt32(textBox7.Text),
                    WorkBeginning = textBox10.Text,
                    WorkEnding = textBox11.Text,
                    BuildObject = comboBox1.SelectedItem as BuildObject,
                    
                };


                _db.PlanSet.Add(plan);
                _db.SaveChanges();
                backForm.UpdateLists();
                MessageBox.Show("Успешно добавлено!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных");
            }
        }

        private void AddPlanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void AddPlanForm_Load(object sender, EventArgs e)
        {
            foreach (BuildObject buildObject in _db.BuildObjectSet)
            {
                if (buildObject.Plan == null)
                {
                    comboBox1.Items.Add(buildObject);
                }
            }
        }
    }
}
