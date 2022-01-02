using System;
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
        private MainMenu mainMenuForm = null;
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

            //
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

        // Draw lines on question panel
        private void Questionpanel_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 179, 179, 179));

            // Draw Line For Question
            pen.Width = 3;
            e.Graphics.DrawLine(pen, 0, 64, Questionpanel.Width, 64);

            // Draw Line For Options
            pen.Width = 2f;
            e.Graphics.DrawLine(pen, 0, 146, Questionpanel.Width, 146);
            e.Graphics.DrawLine(pen, 0, 202, Questionpanel.Width, 202);

            pen.Dispose();
        }


        // Draw lines on prize panel on right of screen
        private void Prizepanel_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 179, 179, 179));

            // Draw lines for each prize
            pen.Width = 2f;
            int position = 40;

            for (int control = 0; control <= 15; control++)
            {
                e.Graphics.DrawLine(pen, 0, position, Prizepanel.Width, position);
                position += 42;
            }

            // Draw border around prize panel;
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
            // Show final score window form and close this form
            if (Play.Text == "Continue")
            {
                FinalScoreWindows window;

                // If checkpoint has been passed, that is the final score else final score is £0
                if (lastCheckpoint != null)
                {
                    window = new FinalScoreWindows(lastCheckpoint.getPrize().Text);
                }
                else
                {
                    window = new FinalScoreWindows("0. 0 NVD");
                }

                // Pass main menu form to final score window, show final score window and dispose of current open window
                window.setMainMenuForm(this.mainMenuForm);
                window.Show();
                this.Dispose();
            }
            else if (Play.Text == "Next Question" || Play.Text == "START")
            {
           
                if (questionNo < 15)
                {
                    // Reset buttons backgrounds and get next question
                    resetButtonBackgrounds();
                    currentQuestion = bank.getQuestion(questionNo);
                    Console.WriteLine(currentQuestion.answer);
                    lblQuestion.Text = currentQuestion.getQuestionText();

                    // Set option button texts
                    var optionsAndButtons = currentQuestion.getOptions().Zip(buttons, (option, button) => new { Option = option, Button = button });

                    foreach (var ob in optionsAndButtons)
                    {
                        ob.Button.Text = ob.Button.Text.Remove(3, ob.Button.Text.Length - 3).Insert(3, ob.Option);
                    }
                }

                // Set text of countdown timer and start timer.
                settimer1();
                timer1.Start();

                // Show question number in next question button
                Play.Text = "Question " + (questionNo + 1);
            }

        }

        // Check answer selected
        private void answerCheck(Button selectedOption)
        {

            // Stop countdown
            timer1.Stop();

            // If correct answer selected
            if (currentQuestion.checkAnswer(selectedOption.Text))
            {
                // Set background image of option selected to correct
                selectedOption.BackColor = Color.LimeGreen;
                // If first question, current prize is head of prize linked list
                if (questionNo == 0)
                {
                    currentPrize = prizeList.getHead();
                }

                // Else reset background of current prize and current prize assigned to next node in list
                else
                {
                    currentPrize.resetBackground();
                    currentPrize = currentPrize.getNext();
                }

                // Set prize background of current prize
                currentPrize.setPrizeBackground();

                // If current prize is checkpoint, set lastcheckpoint accordingly
                if (currentPrize.getCheckpoint())
                {
                    lastCheckpoint = currentPrize;
                }

                // Increment question number
                questionNo += 1;

                // Set next question button to exit if final question answered correctly
                if (questionNo == 15)
                {
                    Play.Text = "Continue";
                }
                else
                {
                    Play.Text = "Next Question";
                }
            }

            // If selected option incorrect
            else
            {
                // Set selected option background color to incorrect color
                selectedOption.BackColor = Color.DarkOrange;

                // If checkpoint has been passed set prize background of checkpoint
                if (lastCheckpoint != null)
                {
                    lastCheckpoint.setPrizeBackground();
                }

                // Set wrong background to current prize and disable option buttons
                if (currentPrize != null)
                {
                    currentPrize.setWrongBackground();
                }
                else
                {
                    prizeList.getHead().setWrongBackground();
                }

                // Find correct answer to show player
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

        // Reset button backgrounds
        private void resetButtonBackgrounds()
        {
            foreach (Button button in buttons)
            {
                button.BackColor = Color.Indigo;
            }
        }

        // Disable option buttons
        private void disableOptionButtons()
        {
            foreach (Button button in buttons)
            {
                button.Enabled = false;
            }
        }

        // Enable option buttons
        private void enableOptionButtons()
        {
            foreach (Button button in buttons)
            {
                button.Enabled = true;
            }
        }

        // 50/50 lifeline button
        private void btn5050_Click(object sender, EventArgs e)
        {
            // Generate number between 0 and 3 based on which option to remove
            int firstOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);

            // If the number generated is for the correct answer, generate a new number
            while (currentQuestion.checkAnswer(buttons[firstOptionToRemove].Text))
            {
                firstOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);
            }

            // Disable removed option button
            buttons[firstOptionToRemove].Enabled = false;

            // Generate number between 0 and 3 based on which option to remove
            int secondOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);

            // If the number generated is for the correct answer, generate a new number
            while (currentQuestion.checkAnswer(buttons[secondOptionToRemove].Text) || secondOptionToRemove == firstOptionToRemove)
            {
                secondOptionToRemove = randomNumberGenerator.Next(0, buttons.Count);
            }

            // Disable removed option button
            buttons[secondOptionToRemove].Enabled = false;

            // Set background of 50/50 lifeline buttons and disable
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

        public void setMainMenuForm(MainMenu form)
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
