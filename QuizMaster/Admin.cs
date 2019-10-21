using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            txtViewSubject.Visible = true;
        }
        Database_Connectivity con = new Database_Connectivity();
        private void btnAdminQUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnAdminQDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnAdminQSave_Click(object sender, EventArgs e)
        {

        }
        //load Question end
        private void btnAdminView_Click(object sender, EventArgs e)
        {
            con.tbl.Clear();
           adminViewDataGrid.Refresh();
            con.viewStudent(txtViewClass.Text.Trim().ToLower());
            adminViewDataGrid.DataSource = con.tbl;
        }

        private void btnAdminViewAll_Click(object sender, EventArgs e)
        {
            con.viewQuestions();
            adminViewDataGrid.DataSource = con.tbl;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblQDate.Text = DateTime.Now.ToLongDateString();
            lblQTime.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
