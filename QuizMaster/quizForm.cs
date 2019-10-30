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
using System.Data.SqlClient;
using System.Threading;

namespace QuizMaster
{
    public partial class quizForm : Form
    {
        public quizForm()
        {
            InitializeComponent();
            loadcombo();
        }
        SpeechSynthesizer speak = new SpeechSynthesizer();
        Database_Connectivity con = new Database_Connectivity();
        int questId = 0;
        int subjectID = 0;
        int classID = 0;
        int grpID = 0;
        string question = null;

        private void getQuestion()
        {
            string query = "Select Questions from Question where Subject_Id=@param and Class_Id=@param1 and Quest_Id=@param2 ";
            SqlCommand command = new SqlCommand(query, con.connect);
            command.Parameters.AddWithValue("@param", subjectID);
            command.Parameters.AddWithValue("@param1", classID);
            command.Parameters.AddWithValue("@param2", questId);
            con.connect.Open();
            try
            {
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    question = read[0].ToString();
                }
                read.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            MessageBox.Show("Question from DB: " + question+"subj is "+subjectID+" clsId is "+classID+" qId is "+questId);
            con.connect.Close();
        }
        private void loadcombo()
        {
            string query = "Select * from Subject";
            DataTable tbl = new DataTable();
            SqlCommand command = new SqlCommand(query, con.connect);
            if (comboBox1.Items.Count > 0)
                comboBox1.Items.Clear();
            con.connect.Open();
            try
            {
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    comboBox1.Items.Add(read[1].ToString());
                }
                read.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

            con.connect.Close();
        }
        private void btnQFStart_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTakeQuiz_Click(object sender, EventArgs e)
        {
            takeQuizPane.Visible = true;
        }

        private void btnBonus_Click(object sender, EventArgs e)
        {
            if (question ==null)//Checks that a question is picked
            {
                MessageBox.Show("Please Take a Question First");
            }
            else
            {
                using (Bonus bonus = new Bonus())
                {
                    bonus.question = question;
                    bonus.txtBonQuestID.Text = questId.ToString();
                    bonus.txtBonGroup.Text = txtTQgroup.Text;
                    bonus.txtBonClass.Text = txtTQclass.Text;
                    bonus.txtBonSubject.Text = comboBox1.SelectedItem.ToString();
                    bonus.ShowDialog();
                }
            }
           

        }

        private void btnTQgetQuestion_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Choose Subject"||comboBox1.SelectedItem.ToString()=="")
            {
                MessageBox.Show("Please choose Subject");
            }
            else
            {
               // question = null;
                getQuestion();
                speak.SpeakAsync("Please Listen Carefully for your Question");
                tqQuestionPane.Enabled = false;
                txtTQanswer.Visible = true;
                btnTQSubmit.Visible = true;
                btnTQSubmit.Enabled = true;
                btnTQgetQuestion.Visible = false;
                if (question != null) {
                  
                speak.SpeakAsync(question);
                }
                else
                {
                    speak.SpeakAsync("Invalid Question Number Please select another Question");
                }
            }
            

        }
        private void btnTQSubmit_Click(object sender, EventArgs e)
        {
            tqQuestionPane.Enabled = true;
            txtTQanswer.Visible = false;
            btnTQgetQuestion.Visible = true;
            btnTQSubmit.Visible = false;
        }

        private void questNumbers(object sender, EventArgs e)
        {
            Button quest = (Button)sender;
            quest.BackColor = Color.Green;
            questId = int.Parse(quest.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTQclass.Text == "" && txtTQgroup.Text == "")
            {
                MessageBox.Show("Please Enter Class and Class Group");
                comboBox1.Items.Clear();
                
                loadcombo();
                comboBox1.Text = "Choose Subject";
            }
            else
            {
                switch (txtTQclass.Text.Trim().ToLower())
                {
                    case "primary 1":
                        classID = 1;
                        break;
                    case "primary 2":
                        classID = 2;
                        break;
                    case "primary 3":
                        classID = 3;
                        break;
                    case "primary 4":
                        classID = 4;
                        break;
                    case "primary 5":
                        classID = 5;
                        break;
                    case "primary 6":
                        classID = 6;
                        break;
                }
                switch (txtTQgroup.Text.Trim().ToLower())
                {
                    case "gold":
                        grpID = 1;
                        break;
                    case "pink":
                        grpID =3;
                        break;
                    case "violet":
                        grpID = 2;
                        break;
                    case "green":
                        grpID = 4;
                        break;
                }
                string subject = comboBox1.SelectedItem.ToString();
                if (subject.ToLower() == "english language")
                {
                    subjectID = 1;
                }
                tqQuestionPane.Enabled = true;
            }
           
        }

        private void btnTQrepeat_Click(object sender, EventArgs e)
        {
            //Code for Repeating a question
            if (question != null)//Checks that question isn't null
            {
                speak.SpeakAsync(question);
            }
            else
            {
                speak.SpeakAsync("Please select another Question");
            }
        }
    }
}
