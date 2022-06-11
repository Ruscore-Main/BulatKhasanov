using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace BulatKhasanov
{
    public partial class WorkPlanForm : Form
    {
        Model1Container _db;
        Form backForm;
        User currentUser;
        Plan currentPlan = null;
        BuildObject currentBuilding;
        Work currentWork;
        string searchBuildingText = "";
        string searchWorkText = "";
        bool desk = false;
        public string printContent = "";
        public WorkPlanForm(Model1Container db, Form form, User user)
        {
            InitializeComponent();
            this._db = db;
            this.backForm = form;
            this.currentUser = user;
        }

        private void WorkPlanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backForm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void WorkPlanForm_Load(object sender, EventArgs e)
        {
            UpdateLists();
            button7.Enabled = false;


            foreach (Employee employee in _db.EmployeeSet)
            {
                comboBox2.Items.Add(employee);
            }


            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

        }

        public void UpdateLists()
        {
            listBox1.Items.Clear();

            // Сортировка
            List<BuildObject> listBuildings = new List<BuildObject>();
            if (desk)
            {
                listBuildings = _db.BuildObjectSet.OrderByDescending(el => el.Name).ToList();
            }
            else
            {
                listBuildings = _db.BuildObjectSet.OrderBy(el => el.Name).ToList();
            }

            listBuildings = listBuildings.Where(el => el.Name.Contains(searchBuildingText)).ToList();

            foreach (BuildObject build in listBuildings)
            {
                listBox1.Items.Add(build);
            }

            listBox3.Items.Clear();
            foreach (Plan plan in _db.PlanSet)
            {
                listBox3.Items.Add(plan);
            }
        }

        public void UpdateWorkList()
        {
            listBox2.Items.Clear();

            if (currentPlan != null)
            {
                int sum = 0;
                foreach (Work work in currentPlan.Work)
                {
                    if (work.WorkName.Contains(searchWorkText))
                    {
                        sum += work.Price * work.Scale;
                        listBox2.Items.Add(work);
                    }
                }
                textBox9.Text = $"{sum}";
            }
        }


        
        // Добавление нового плана
        private void button1_Click(object sender, EventArgs e)
        {
            AddPlanForm addPlanForm = new AddPlanForm(_db, this);
            addPlanForm.Show();
            this.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) // Изменение текущего строительного объектов
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
                    if (currentBuilding.Plan != null)
                    {
                        button7.Enabled = true;
                        currentPlan = currentBuilding.Plan;
                        textBox5.Text = $"{currentPlan.ContractNumber}";
                        textBox7.Text = $"{currentPlan.ContractNumber}";

                        button2.Enabled = true;
                        button3.Enabled = true;
                        listBox2.Items.Clear();

                        // Расчет суммы всех товаров: цена за 1 кв. м * площадь помещения
                        int sum = 0;
                        foreach (Work work in currentPlan.Work)
                        {
                            sum += work.Price * work.Scale;
                            listBox2.Items.Add(work);
                        }
                        textBox9.Text = $"{sum}";

                        textBox10.Text = currentPlan.WorkBeginning;
                        textBox11.Text = currentPlan.WorkEnding;


                    }
                    else
                    {
                        textBox5.Text = "";
                        textBox7.Text = "";

                        button2.Enabled = false;
                        button3.Enabled = false;
                        listBox2.Items.Clear();

                        textBox9.Text = "";

                        textBox10.Text = "";
                        textBox11.Text = "";
                        MessageBox.Show("У выбранного объекта нет плана работы. Добавьте его!");
                    }
                }
            }
            catch { }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e) // Изменение текущего плана работы
        {
            try
            {
                currentPlan = listBox3.SelectedItem as Plan;
                if (currentPlan != null)
                {
                    button7.Enabled = true;
                    listBox2.Items.Clear();
                    button2.Enabled = true;
                    button3.Enabled = true;
                    // Расчет суммы всех товаров: цена за 1 кв. м * площадь помещения
                    int sum = 0;
                    foreach (Work work in currentPlan.Work)
                    {
                        sum += work.Price * work.Scale;
                        listBox2.Items.Add(work);
                    }
                    textBox7.Text = $"{currentPlan.ContractNumber}";
                    textBox9.Text = $"{sum}";

                    textBox10.Text = currentPlan.WorkBeginning;
                    textBox11.Text = currentPlan.WorkEnding;

                    currentBuilding = currentPlan.BuildObject;
                    if (currentBuilding != null)
                    {
                        textBox1.Text = currentBuilding.Name;
                        textBox2.Text = currentBuilding.Address;
                        textBox3.Text = currentBuilding.ContactPerson;
                        textBox4.Text = currentBuilding.CustomerNumber;
                    }
                    else
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        MessageBox.Show("У выбранного плана нет строительного объекта. Добавьте его!");
                    }
                }
            }
            catch { }
        }

        public void ClearFields()
        {
            currentPlan = null;
            currentBuilding = null;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            button2.Enabled = false;
            button3.Enabled = false;
            listBox2.Items.Clear();

            textBox9.Text = "";

            textBox10.Text = "";
            textBox11.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                _db.PlanSet.Remove(currentPlan);
                _db.SaveChanges();
                UpdateLists();
                ClearFields();
                MessageBox.Show("Успешно удалено!");
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                currentPlan.ContractNumber = Convert.ToInt32(textBox7.Text);
                currentPlan.WorkBeginning = textBox10.Text;
                currentPlan.WorkEnding = textBox11.Text;

                _db.SaveChanges();
                MessageBox.Show("Успешно изменено!");
            }
            catch
            {

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked)
            {
                desk = true;
            }
            else
            {
                desk = false;
            }
            UpdateLists();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            searchBuildingText = (sender as TextBox).Text;
            UpdateLists();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            searchWorkText = (sender as TextBox).Text;
            UpdateWorkList();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentWork = listBox2.SelectedItem as Work;


                if (currentWork != null)
                {
                    if (currentWork.Plan != null)
                    {
                        textBox12.Text = currentWork.Plan.ToString();
                    }
                    textBox13.Text = currentWork.WorkName;
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(currentWork.Employee);
                    textBox14.Text = $"{currentWork.Price}";
                    textBox8.Text = $"{currentWork.Scale}";
                    checkBox1.Checked = currentWork.IsDone;

                    button4.Enabled = true;
                    button5.Enabled = true;
                }
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddWorkForm addWorkForm = new AddWorkForm(_db, this);
            addWorkForm.Show();
            this.Hide();
        }

        // Обновление работы
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                currentWork.WorkName = textBox13.Text;
                currentWork.Employee = comboBox2.SelectedItem as Employee;
                currentWork.Price = Convert.ToInt32(textBox14.Text);
                currentWork.Scale = Convert.ToInt32(textBox8.Text);
                currentWork.IsDone = checkBox1.Checked;
                _db.SaveChanges();
                UpdateLists();
                UpdateWorkList();
                MessageBox.Show("Успешно изменено!");
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                _db.WorkSet.Remove(currentWork);
                _db.SaveChanges();
                currentWork = null;
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox8.Text = "";
                checkBox1.Checked = false;
                UpdateLists();
                UpdateWorkList();
                MessageBox.Show("Успешно удалено!");
            }
            catch { }
        }

        // ЧАСТЬ ПЕЧАТИ СМЕТЫ

        private DocX outputDoc()
        {
            string pathDocument = "otchet.docx";
            DocX document = DocX.Create(pathDocument);
            document.MarginLeft = 60.0f;
            document.MarginRight = 60.0f;
            document.MarginTop = 60.0f;
            document.MarginBottom = 60.0f;


            Paragraph p1 = document.InsertParagraph("Смета").Bold().Font("Times New Roman").FontSize(16);
            p1.Alignment = Alignment.center;

            Paragraph p2 = document.InsertParagraph($"По проделанным работам на объекте: {currentBuilding.Name}\n").FontSize(14);
            p2.Alignment = Alignment.center;

            Table table = document.AddTable(currentPlan.Work.Count() + 1, 6);
            table.Alignment = Alignment.center;
            table.Design = TableDesign.TableGrid;

            table.Rows[0].Cells[0].Paragraphs[0].Append("№ п.п.").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Наименование работ").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[2].Paragraphs[0].Append("Ед. изм").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[3].Paragraphs[0].Append("Цена за 1 ед. изм").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[4].Paragraphs[0].Append("Кол-во ед. изм").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[5].Paragraphs[0].Append("Цена, руб").Font("Times New Roman").FontSize(12).Bold();


            int row = 1;
            int sum = 0;
            foreach (Work work in currentPlan.Work)
            {
                table.Rows[row].Cells[0].Paragraphs[0].Append($"{row}").Font("Times New Roman").FontSize(12);
                table.Rows[row].Cells[1].Paragraphs[0].Append(work.WorkName).Font("Times New Roman").FontSize(12);
                table.Rows[row].Cells[2].Paragraphs[0].Append("м^2").Font("Times New Roman").FontSize(12);
                table.Rows[row].Cells[3].Paragraphs[0].Append($"{work.Price}").Font("Times New Roman").FontSize(12);
                table.Rows[row].Cells[4].Paragraphs[0].Append($"{work.Scale}").Font("Times New Roman").FontSize(12);
                table.Rows[row].Cells[5].Paragraphs[0].Append($"{work.Scale*work.Price}").Font("Times New Roman").FontSize(12);
                sum += work.Scale * work.Price;
                row++;
            }

            table.AutoFit = AutoFit.Contents;


            document.InsertParagraph().InsertTableAfterSelf(table);

            Paragraph p3 = document.InsertParagraph($"\nОбщая сумма: {sum}").FontSize(14);
            p3.Alignment = Alignment.right;

            document.Save();

            return document;

        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(printContent, new System.Drawing.Font("Arial", 16), Brushes.Black, 0, 0);
        }

        private void button7_Click(object sender, EventArgs e) // Печать
        {
            DocX docs = outputDoc();

            printContent = docs.Text;
            PrintDocument printDocument = new PrintDocument();
            PrintDialog printDialog = new PrintDialog();
            printDocument.PrintPage += PrintPageHandler;
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.Print();
            }
        }
    }
}
