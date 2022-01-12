using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AiLaTrieuPhu
{
    public partial class FinalScoreWindows : Form
    {
        public FinalScoreWindows(string score)
        {
            InitializeComponent();
            lblPrizeAmount.Text = score.Substring(0, score.Length );
        }

        private void btnReturnToForm1_Click(object sender, EventArgs e)
        {
            Form1 MainForm = new Form1();
            MainForm.Show();
            this.Hide();
        }

        private void FinalScoreWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        
    }
}
