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
    public partial class quizForm : Form
    {
        public quizForm()
        {
            InitializeComponent();
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
            using (Bonus bonus = new Bonus())
             {
                bonus.ShowDialog();
            }

        }

        private void btnTQgetQuestion_Click(object sender, EventArgs e)
        {
            tqQuestionPane.Enabled = false;
            txtTQanswer.Visible = true;
            btnTQSubmit.Visible = true;
            btnTQSubmit.Enabled = true;
            btnTQgetQuestion.Visible = false;
        }
        private void btnTQSubmit_Click(object sender, EventArgs e)
        {
            tqQuestionPane.Enabled = true;
            txtTQanswer.Visible = false;
            btnTQgetQuestion.Visible = true;
            btnTQSubmit.Visible = false;
        }
    }
}
