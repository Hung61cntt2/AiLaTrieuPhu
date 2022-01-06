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
        private Form1 mainMenuForm = null;
        public FinalScoreWindows(string score)
        {
            InitializeComponent();
            lblPrizeAmount.Text = score.Substring(2, score.Length - 2);
        }

        private void FinalScoreWindows_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 179, 179, 179));

            // Draw Line For Title
            pen.Width = 2f;
            e.Graphics.DrawLine(pen, 0, 83, Width, 83);

            // Draw Line For prize amount and button
            pen.Width = 2f;
            e.Graphics.DrawLine(pen, 0, 179, Width, 179);
            e.Graphics.DrawLine(pen, 0, 262, Width, 262);

            pen.Dispose();
        }

        private void btnReturnToForm1_Click(object sender, EventArgs e)
        {
            mainMenuForm.Show();
            this.Dispose();
        }

        public void setMainMenuForm(Form1 form)
        {
            this.mainMenuForm = form;
        }

        private void FinalScoreWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FinalScoreWindows_Load(object sender, EventArgs e)
        {

        }

        private void lblPrizeAmount_Click(object sender, EventArgs e)
        {

        }
    }
}
