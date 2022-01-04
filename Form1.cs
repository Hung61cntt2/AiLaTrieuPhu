﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Security.Policy;

namespace AiLaTrieuPhu
{
    public partial class Form1 : Form
    {
        private Form1 mainMenuForm = null;
        // Thuộc tính câu hỏi
        private int questionNo = 0;
        private QuestionBank bank = null;
        private Question currentQuestion = null;
        // Danh sách phần thưởng
        List<Button> buttons = new List<Button>();
        LinkedList prizeList = new LinkedList();

        LinkedListNode currentPrize = null;
        LinkedListNode lastCheckpoint = null;

        // Bộ sinh số ngẫu nhiên (RNG)
        Random randomNumberGenerator = new Random();

        public Form1()
        {
            InitializeComponent();

            // Xuất câu hỏi từ CSDL
            bank = new QuestionBank();

            // Thêm các đáp án
            buttons.Add(btnoptionA);
            buttons.Add(btnoptionB);
            buttons.Add(btnoptionC);
            buttons.Add(btnoptionC);
            disableOptionButtons();

            // Thêm giải thưởng vào danh sách liên kết
            prizeList.addToList(new LinkedListNode(prize1, false));
            prizeList.addToList(new LinkedListNode(prize2, false));
            prizeList.addToList(new LinkedListNode(prize3, false));
            prizeList.addToList(new LinkedListNode(prize4, false));
            prizeList.addToList(new LinkedListNode(prize5, false));
            prizeList.addToList(new LinkedListNode(prize6, false));
            prizeList.addToList(new LinkedListNode(prize7, false));
            prizeList.addToList(new LinkedListNode(prize8, false));
            prizeList.addToList(new LinkedListNode(prize9, false));
            prizeList.addToList(new LinkedListNode(prize10, false));
            prizeList.addToList(new LinkedListNode(prize11, false));
            prizeList.addToList(new LinkedListNode(prize12, false));
            prizeList.addToList(new LinkedListNode(prize13, false));
            prizeList.addToList(new LinkedListNode(prize14, false));
            prizeList.addToList(new LinkedListNode(prize15, true));
        }

        // Kẻ đường cho bảng điều khiển câu hỏi
        private void Questionpanel_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 179, 179, 179));

            // Kẻ đường cho câu hỏi
            pen.Width = 3;
            e.Graphics.DrawLine(pen, 0, 64, Questionpanel.Width, 64);

            // Kẻ đường cho các đáp án
            pen.Width = 2f;
            e.Graphics.DrawLine(pen, 0, 146, Questionpanel.Width, 146);
            e.Graphics.DrawLine(pen, 0, 202, Questionpanel.Width, 202);

            pen.Dispose();
        }


        // Kẻ đường cho bảng điều khiển giải thưởng
        private void Prizepanel_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 179, 179, 179));

            // Kẻ đường cho mỗi mốc giải thưởng
            pen.Width = 2f;
            int position = 40;

            for (int control = 0; control <= 15; control++)
            {
                e.Graphics.DrawLine(pen, 0, position, Prizepanel.Width, position);
                position += 42;
            }

            // Vẽ khung xung quanh bảng điều khiên giải thưởng
            pen = new Pen(Color.FromArgb(255, 212, 175, 55), 5);

            Rectangle rect = Prizepanel.ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(pen, rect);

            pen.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            // Hiển thị form final score window và đóng form1
            if (Play.Text == "Continue")
            {
                FinalScoreWindows window;

                // Nếu vượt qua được checkpoint, thì đó là giải thưởng cuối cùng, ngược lại giải thưởng cuối cùng là 0 VND
                if (lastCheckpoint != null)
                {
                    window = new FinalScoreWindows(lastCheckpoint.getPrize().Text);
                }
                else
                {
                    window = new FinalScoreWindows("0. 0 NVD");
                }

                // Chuyển tới form final score window và đóng form1
                window.setMainMenuForm(this.mainMenuForm);
                window.Show();
                this.Dispose();
            }
            else if (Play.Text == "Next Question" || Play.Text == "START")
            {
           
                if (questionNo < 15)
                {
                    // Đặt lại nền của nút và chuyển đến câu hỏi tiếp theo
                    resetButtonBackgrounds();
                    currentQuestion = bank.getQuestion(questionNo);
                    Console.WriteLine(currentQuestion.answer);
                    lblQuestion.Text = currentQuestion.getQuestionText();

                    // Đặt lại các nút đáp án
                    var optionsAndButtons = currentQuestion.getOptions().Zip(buttons, (option, button) => new { Option = option, Button = button });

                    foreach (var ob in optionsAndButtons)
                    {
                        ob.Button.Text = ob.Button.Text.Remove(3, ob.Button.Text.Length - 3).Insert(3, ob.Option);
                    }
                }

                // Đặt lại bộ đếm và bắt đầu đếm
                settimer1();
                timer1.Start();

                // Hiển thị số đếm câu hỏi
                Play.Text = "Question " + (questionNo + 1);
            }

        }

        // Kiếm tra đáp án được lựa chọn
        private void answerCheck(Button selectedOption)
        {

            // Dừng bộ đếm
            timer1.Stop();

            // Nếu đáp án được chọn là đúng
            if (currentQuestion.checkAnswer(selectedOption.Text))
            {
                // Đặt cái ảnh nền của đáp án được lựa chọn là đúng
                selectedOption.BackColor = Color.LimeGreen;
                // Nếu là câu hỏi đầu tiên, thì giải thưởng sẽ là ở đầu danh sách liên kết
                if (questionNo == 0)
                {
                    currentPrize = prizeList.getHead();
                }

                // Ngược lại thì đặt lại nền của giải thưởng hiện tại và chuyển giải thưởng hiện tại đến Node tiếp theo trong danh sách
                else
                {
                    currentPrize.resetBackground();
                    currentPrize = currentPrize.getNext();
                }

                // Đặt lại nền của giải thưởng hiện tại
                currentPrize.setPrizeBackground();

                // Nếu giải thưởng hiện tại là checkpoint, đặt lại checkpoint cuối cùng
                if (currentPrize.getCheckpoint())
                {
                    lastCheckpoint = currentPrize;
                }

                // Tăng số đếm câu hỏi
                questionNo += 1;

                // Đặt nút "Câu hỏi tiếp theo" thành nút "Đi đến giải thưởng" nếu trả lời đúng câu hỏi cuối cùng (thứ 15)
                if (questionNo == 15)
                {
                    Play.Text = "Đi đến giải thưởng";
                }
                else
                {
                    Play.Text = "Câu hỏi tiếp theo";
                }
            }

            // Nếu đáp án được chọn là sai
            else
            {
                // Đặt màu nền của đáp án được chọn là sai
                selectedOption.BackColor = Color.DarkOrange;

                // Nếu vượt qua được checkpoint thì đặt nền của checkpoint là giải thưởng
                if (lastCheckpoint != null)
                {
                    lastCheckpoint.setPrizeBackground();
                }

                // Đặt nền sai vào giải thưởng hiện tại và vô hiệu hóa các đáp án
                if (currentPrize != null)
                {
                    currentPrize.setWrongBackground();
                }
                else
                {
                    prizeList.getHead().setWrongBackground();
                }

                // Hiển thị đáp án đúng cho người chơi
                foreach (Button button in buttons)
                {
                    if (button.Enabled == true)
                    {
                        if (currentQuestion.checkAnswer(button.Text))
                        {
                            button.BackColor = Color.LimeGreen;
                        }
                    }
                }

                Play.Text = "Continue";
            }

            disableOptionButtons();

        }

        // Đặt lại nền
        private void resetButtonBackgrounds()
        {
            foreach (Button button in buttons)
            {
                button.BackColor = Color.Indigo;
            }
        }

        // Vô hiệu hóa các đáp án
        private void disableOptionButtons()
        {
            foreach (Button button in buttons)
            {
                button.Enabled = false;
            }
        }

        // Bỏ vô hiệu hóa các đáp án 
        private void enableOptionButtons()
        {
            foreach (Button button in buttons)
            {
                button.Enabled = true;
            }
        }

        // Nút trợ giúp 50/50
        private void btn5050_Click(object sender, EventArgs e)
        {
            // Quay ngẫu nhiên số giữa 0 và 3 dựa trên đáp án để loại bỏ thứ nhất
            int firstOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);

            // Nếu số lấy ngẫu nhiên trùng với đáp án đúng, quay lại số ngẫu nhiên mới
            while (currentQuestion.checkAnswer(buttons[firstOptionToRemove].Text))
            {
                firstOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);
            }

            // Vô hiệu hóa đáp án bị loại bỏ thứ nhất
            buttons[firstOptionToRemove].Enabled = false;

            // Chọn ngẫu nhiên số giữa 0 và 3 dựa trên đáp án để loại bỏ thứ hai
            int secondOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);

            // Nếu số lấy ngẫu nhiên trùng với đáp án đúng hoặc trùng với đáp án loại bỏ thứ nhất, quay lại số ngẫu nhiên mới
            while (currentQuestion.checkAnswer(buttons[secondOptionToRemove].Text) || secondOptionToRemove == firstOptionToRemove)
            {
                secondOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);
            }

            // Vô hiệu hóa đáp án bị loại bỏ thứ hai
            buttons[secondOptionToRemove].Enabled = false;

            // Đặt lại nền của nút trợ giúp 50/50 và vô hiệu hóa
            btn5050.BackgroundImage = Properties.Resources._5050_used;
            btn5050.Enabled = false;

        }


        // Generate poll results
        private List<int> generateRandomPollResults()
        {

            List<int> randomNumbers = new List<int>();

            // If 50/50 has not been used generate 4 numbers else generate 2
            if (btn5050.Enabled == true)
            {
                randomNumbers.Add(randomNumberGenerator.Next(1, 25));

                randomNumbers.Add(randomNumberGenerator.Next(25, 50) - randomNumbers[0]);

                randomNumbers.Add(randomNumberGenerator.Next(50, 75) - randomNumbers[1]);

                randomNumbers.Add(100 - (randomNumbers[0] + randomNumbers[1] + randomNumbers[2]));
            }
            else
            {
                randomNumbers.Add(randomNumberGenerator.Next(1, 50));

                randomNumbers.Add(100 - randomNumbers[0]);
            }

            // Sort list of numbers and return
            randomNumbers.Sort();
            return randomNumbers;

        }
        int i;
        private void settimer1()
        {
            if (questionNo < 10)
            {
                i = 60;
            }
            else
            {
                i = 45;
            }
        }

        // Countdown timer
        private void timer1_Tick(object sender, EventArgs e)
        {

            // If visible timer is not 0, decrement value, else stop countdown
            if (i != 0)
            {
                i--;
            }

            else
            {
                timer1.Stop();
            }
        }

        private void lblQuestion_TextChanged(object sender, EventArgs e)
        {

            Control control = (sender as Control);
            control.Font = sizeTextToControl(control, this.CreateGraphics(), control.Padding.Right);

        }

        private Font sizeTextToControl(Control control, Graphics graphic, int padding)
        {
            // Create a small font
            Font font;
            font = new Font(control.Font.FontFamily, 6.0f, control.Font.Style);
            SizeF textSize = graphic.MeasureString(control.Text, font);

            // Loop until it fits perfect
            while ((textSize.Width < control.Width - padding) && (textSize.Height < control.Height - padding))
            {
                font = new Font(font.FontFamily, font.Size + 0.5f, font.Style);
                textSize = graphic.MeasureString(control.Text, font);
            }

            font = new Font(font.FontFamily, font.Size - 0.5f, font.Style);

            return font;
        }

        public void setMainMenuForm(Form1 form)
        {
            this.mainMenuForm = form;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Question_Click(object sender, EventArgs e)
        {

        }

        private void btn5050_Click_1(object sender, EventArgs e)
        {
            btn5050.BackgroundImage = Properties.Resources._5050;
        }

        private void btnAudience_Click(object sender, EventArgs e)
        {
            btnAudience.BackgroundImage = Properties.Resources.Audience;
        }

        private void btnPhone_Click(object sender, EventArgs e)
        {
            btnPhone.BackgroundImage = Properties.Resources.Phone;
        }
    }
}
