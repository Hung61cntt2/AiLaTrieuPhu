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
            buttons.Add(btnoptionD);
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
        
        // Xử lý sự kiện nút Chơi!
        private void button1_Click(object sender, System.EventArgs e)
        {
            // Hiển thị form final score window và đóng form1
            if (Play.Text == "Đi đến giải thưởng")
            {
                FinalScoreWindows final;
                // Nếu vượt qua được checkpoint, thì đó là giải thưởng cuối cùng, ngược lại giải thưởng cuối cùng là 0 VND
                if (lastCheckpoint != null)
                {
                    final = new FinalScoreWindows(lastCheckpoint.getPrize().Text);
                    
                }
                else
                {
                    final = new FinalScoreWindows("0VND");
                    
                }
                // Chuyển tới final score window và đóng form1
                final.Show();
                this.Hide();

            }
            else if (Play.Text == "Câu hỏi tiếp theo" || Play.Text == "Chơi!")
            {
                // Mở các nút đáp án
                enableOptionButtons();
                //chartPollResults.Visible = false;

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
                        ob.Button.Text = ob.Button.Text.Remove(2, ob.Button.Text.Length - 2).Insert(2, ob.Option);
                    }
                }

                // Đặt lại bộ đếm và bắt đầu đếm
                settimer1();
                timer1.Start();

                // Hiển thị số đếm câu hỏi
                Play.Text = "Câu hỏi thứ " + (questionNo + 1);
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
                selectedOption.BackgroundImage = Properties.Resources.correct;
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
                selectedOption.BackgroundImage = Properties.Resources.wrong;

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
                            button.BackgroundImage = Properties.Resources.correct;
                        }
                    }
                }

                Play.Text = "Đi đến giải thưởng";
            }

            disableOptionButtons();

        }

        // Kiểm tra đáp án A
        private void btnoptionA_Click(object sender, System.EventArgs e)
        {
            answerCheck(btnoptionA);
        }

        // Kiểm tra đáp án B
        private void btnoptionB_Click(object sender, System.EventArgs e)
        {
            answerCheck(btnoptionB);
        }

        // Kiểm tra đáp án C
        private void btnoptionC_Click(object sender, System.EventArgs e)
        {
            answerCheck(btnoptionC);
        }

        // Kiểm tra đáp án D
        private void btnoptionD_Click(object sender, System.EventArgs e)
        {
            answerCheck(btnoptionD);
        }

        // Đặt lại nền
        private void resetButtonBackgrounds()
        {
            foreach (Button button in buttons)
            {
                button.BackgroundImage = Properties.Resources.button;
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

        // Mở các đáp án 
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


        // Tạo kết quả ngẫu nhiên
        private List<int> generateRandomPollResults()
        {

            List<int> randomNumbers = new List<int>();

            // Nếu nút trợ giúp 50/50 chưa được dùng thì tạo 4 số ngẫu nhiên, ngược lại thì tạo 2 số ngẫu nhiên
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

            // Sắp xếp lại các số ngẫu nhiên và trả về
            randomNumbers.Sort();
            return randomNumbers;

        }

        // Đặt bộ đếm thời gian
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

        // Bộ đếm thời gian
        private void timer1_Tick(object sender, EventArgs e)
        {

            // Nếu giá trị trên bộ đếm khác 0, giảm dần, ngược lại thì ngừng bộ đếm
            if (i != 0)
            {
                i--;
                Time.Text = i.ToString();
            }

            else
            {
                timer1.Stop();

                Play.Text = "Đi đến giải thưởng";
                // Hiển thị đáp án đúng khi hết giờ
                foreach (Button button in buttons)
                {
                    if (button.Enabled == true)
                    {
                        if (currentQuestion.checkAnswer(button.Text))
                        {
                            button.BackgroundImage = Properties.Resources.correct;
                        }
                    }
                }
                // Vô hiệu hóa các đáp án
                disableOptionButtons();
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnAudience_Click(object sender, EventArgs e)
        {
            btnAudience.BackgroundImage = Properties.Resources.Audience_used;
        }

        private void btnPhone_Click(object sender, EventArgs e)
        {
            btnPhone.BackgroundImage = Properties.Resources.Phone_used;
        }

    }
}
