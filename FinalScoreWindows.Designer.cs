﻿
namespace AiLaTrieuPhu
{
    partial class FinalScoreWindows
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Button();
            this.lblPrizeAmount = new System.Windows.Forms.Button();
            this.btnReturnToForm1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Indigo;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(175)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(68, 64);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(389, 99);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Giải thưởng của bạn:";
            this.lblTitle.UseVisualStyleBackColor = false;
            // 
            // lblPrizeAmount
            // 
            this.lblPrizeAmount.BackColor = System.Drawing.Color.Indigo;
            this.lblPrizeAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrizeAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(175)))), ((int)(((byte)(55)))));
            this.lblPrizeAmount.Location = new System.Drawing.Point(137, 191);
            this.lblPrizeAmount.Name = "lblPrizeAmount";
            this.lblPrizeAmount.Size = new System.Drawing.Size(268, 87);
            this.lblPrizeAmount.TabIndex = 1;
            this.lblPrizeAmount.UseVisualStyleBackColor = false;
            this.lblPrizeAmount.Click += new System.EventHandler(this.lblPrizeAmount_Click);
            // 
            // btnReturnToForm1
            // 
            this.btnReturnToForm1.BackColor = System.Drawing.Color.Indigo;
            this.btnReturnToForm1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnToForm1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(175)))), ((int)(((byte)(55)))));
            this.btnReturnToForm1.Location = new System.Drawing.Point(137, 310);
            this.btnReturnToForm1.Name = "btnReturnToForm1";
            this.btnReturnToForm1.Size = new System.Drawing.Size(268, 87);
            this.btnReturnToForm1.TabIndex = 2;
            this.btnReturnToForm1.Text = "Chơi lại";
            this.btnReturnToForm1.UseVisualStyleBackColor = false;
            // 
            // FinalScoreWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(513, 450);
            this.Controls.Add(this.btnReturnToForm1);
            this.Controls.Add(this.lblPrizeAmount);
            this.Controls.Add(this.lblTitle);
            this.Name = "FinalScoreWindows";
            this.Text = "FinalScoreWindows";
            this.Load += new System.EventHandler(this.FinalScoreWindows_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lblTitle;
        private System.Windows.Forms.Button lblPrizeAmount;
        private System.Windows.Forms.Button btnReturnToForm1;
    }
}