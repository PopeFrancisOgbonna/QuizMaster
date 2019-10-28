using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Threading;


namespace QuizMaster
{
    public partial class QuizMasterHome : Form
    {

        public QuizMasterHome()
        {

            InitializeComponent();
        }
  
        SpeechSynthesizer speaking = new SpeechSynthesizer();
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();
            lblQTime.Text = DateTime.Now.ToLongTimeString();
            lblQDate.Text = DateTime.Now.ToLongDateString();
            i++;
            if (i <= 15)
            {
                animationImageAC.Visible = true;
                animationParad.Visible = false;
            }
            if (i >= 30)
            {
                animationImageAC.Visible = false;
                animationParad.Visible = true;
            }
            if (i >=61)
            {
                animationParad.Visible = false;
                animationPixCARS.Visible = true;
            }
            if (i >= 90)
            {
                animationPixCARS.Visible = false;
                animationImageAC.Visible = true;
                i = 0;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           
            sideBarPane.Visible = true;
            contentPane.Visible = true;
            btnAdmin.Visible = true;
        }
        Database_Connectivity connection = new Database_Connectivity();
        public void fillgrid()
        {
            
            connection.viewParticipants();
            participantGrid.DataSource = connection.tblParticipant;
           
        }

        private void btnTakeQuiz_Click(object sender, EventArgs e)
        {
            quizForm quiz = new quizForm();
            quiz.ShowDialog();
           
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.ShowDialog();
        }

        private void btnParticipant_Click(object sender, EventArgs e)
        {
            connection.tblParticipant.Clear();
            participantGrid.DataSource = connection.tblParticipant;
            participantPane.Visible = true;
            fillgrid();
        }
        // 


    }

      

}

