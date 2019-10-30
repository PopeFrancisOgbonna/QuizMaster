using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Speech.Synthesis;

namespace QuizMaster
{
    public partial class Bonus : Form
    {
        public Bonus()
        {
            InitializeComponent();
        }
        SpeechSynthesizer speak = new SpeechSynthesizer();
        public string studentClass;
        public string subject;
        public int questNo;
        public string group;
        public string question;
        string answrer = null;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBonusRepQuest_Click(object sender, EventArgs e)
        {
            speak.SpeakAsync("Please Listen Carefully to Your Bonus Question");
            speak.SpeakAsync(question);
        }

        private void btnBonusSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}
