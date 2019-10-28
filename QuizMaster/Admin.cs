using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizMaster
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnadminReg_Click(object sender, EventArgs e)
        {
            scrollPane.Top = btnadminReg.Top;
            scrollPane.Height = btnadminReg.Height;

            registerPane.Visible = true;
            loadQuestionpane.Visible = false;
            viewPane.Visible = false;
        }

        private void btnAdminLoadQuest_Click(object sender, EventArgs e)
        {
            scrollPane.Top = btnAdminLoadQuest.Top;
            scrollPane.Height = btnAdminLoadQuest.Height;
            scrollPane.Visible = true;

            registerPane.Visible = false;
            loadQuestionpane.Visible = true;
            viewPane.Visible = false;

        }

        private void btnAdminViewStudent_Click(object sender, EventArgs e)
        {
            scrollPane.Top = btnAdminViewStudent.Top;
            scrollPane.Height = btnAdminViewStudent.Height;
            scrollPane.Visible = true;

            registerPane.Visible = false;
            loadQuestionpane.Visible = false;
            viewPane.Visible = true;
            lblSubject.Visible = false;
            txtViewSubject.Visible = false;
            txtViewClass.Clear();
            if (adminViewDataGrid.Visible) { adminViewDataGrid.Visible = false; }
        }

        private void btnAdminViewQuest_Click(object sender, EventArgs e)
        {
            scrollPane.Top = btnAdminViewQuest.Top;
            scrollPane.Height = btnAdminViewQuest.Height;
            scrollPane.Visible = true;

            registerPane.Visible = false;
            loadQuestionpane.Visible = false;
            viewPane.Visible = true;
            lblSubject.Visible = true;
            txtViewClass.Text = "";
            txtViewSubject.Visible = true;
            if (adminviewStudentGrid.Visible) { adminviewStudentGrid.Visible = false; }
        }
        Database_Connectivity con = new Database_Connectivity();
        private void btnAdminQUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnAdminQDelete_Click(object sender, EventArgs e)
        {

        }
        //code load questions into database
        private void btnAdminQSave_Click(object sender, EventArgs e)
        {
            string query = "insert into Question values(@param,@param1,@param2,@param3)";

            
            
                int Subject_Id = 0;
                int class_Id = 0;
                switch (txtLQsubject.Text.Trim().ToLower())//Gets the correct subject id
                {
                    case "english": case "english language":
                        Subject_Id = 1;
                        break;
                    case "maths": case "mathematics":
                        Subject_Id = 2;
                        break;
                }
                switch (txtLQclass.Text.Trim().ToLower())//Gets the correct class id for question being uploaded
                {
                    case "primary 1":
                        class_Id = 1;
                        break;
                    case "primary 2":
                        class_Id = 2;
                        break;
                    case "primary 3":
                        class_Id = 3;
                        break;
                    case "primary 4":
                        class_Id = 4;
                        break;
                    case "primary 5":
                        class_Id = 5;
                        break;
                    case "primary 6":
                        class_Id = 6;
                        break;
                }
            //database query for inserting questions 
                SqlCommand command = new SqlCommand(query, con.connect);
                command.Parameters.AddWithValue("@param",Subject_Id);
                command.Parameters.AddWithValue("@param1",txtLQquestion.Text.Trim());
                command.Parameters.AddWithValue("@param2",class_Id);
                command.Parameters.AddWithValue("@param3",txtLQmark.Text.Trim());
                con.connect.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Question Uploaded successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occured! Question not loaded", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.connect.Close();    
        }
        //load Question end
        private void btnAdminView_Click(object sender, EventArgs e)
        {
            if (txtViewClass.Text != "" && txtViewSubject.Text != "")
            {
               con.tblclassquest.Clear();//clears the data table 
                adminViewDataGrid.Refresh();
                con.viewClassquestions(txtViewClass.Text.Trim().ToLower(),txtViewSubject.Text.Trim().ToLower());
                adminViewDataGrid.DataSource = con.tblclassquest;
                adminviewStudentGrid.Visible = false;
                adminViewDataGrid.Visible = true;
            }
            //if (txtViewClass.Text == "")
            //{
            //    con.tbl.Clear();//clears the data table 
            //    adminViewDataGrid.Refresh();
            //    con.viewQuestions();
            //    adminViewDataGrid.DataSource = con.tbl;
            //    adminviewStudentGrid.Visible = false;
            //}
            else
            {
                con.tblviewStudent.Clear();//clears the data table 
                adminviewStudentGrid.Refresh();
                con.viewStudent(txtViewClass.Text.Trim().ToLower());//Invoke view student method
                adminviewStudentGrid.DataSource = con.tblviewStudent;

                adminviewStudentGrid.Visible = true;

            }

        }

        private void btnAdminViewAll_Click(object sender, EventArgs e)
        {
            if (txtViewSubject.Visible == false)//gets all Students irrespective of class
            {
               con.tblParticipant.Clear();//clears the data table 
                adminviewStudentGrid.Refresh();
                con.viewParticipants();
                adminviewStudentGrid.DataSource = con.tblParticipant;

                adminviewStudentGrid.Visible = true;
            }
            if (txtViewSubject.Visible)//Gets all questions from the database irrespective of class
            {
                con.tblviewQuest.Clear();//clears the data table 
                adminViewDataGrid.Refresh();
                con.viewQuestions();
                adminViewDataGrid.DataSource = con.tblviewQuest;
                adminViewDataGrid.Visible = true;
                adminviewStudentGrid.Visible = false;
            }
            
        }
        //View Participants and questions end
        private void btnAddParticipant_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateParticipant_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteParticipant_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)//display current time
        {
            lblQDate.Text = DateTime.Now.ToLongDateString();
            lblQTime.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
