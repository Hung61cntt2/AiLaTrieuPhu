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
        private Form1 MainForm = null;
        public FinalScoreWindows(string score)
        {
            InitializeComponent();
            lblPrizeAmount.Text = score.Substring(2, score.Length - 2);
        }

        private void btnReturnToForm1_Click(object sender, EventArgs e)
        {
            MainForm.Show();
            this.Dispose();
        }

        public void setMainForm(Form1 form)
        {
            this.MainForm = form;
        }

        private void FinalScoreWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
