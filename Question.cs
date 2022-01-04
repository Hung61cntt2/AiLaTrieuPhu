using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLaTrieuPhu
{
    public class Question
    {
        // Thuộc tính
        public string questionText;
        public string[] options;
        public string answer;

        // Cấu trúc câu hỏi, thêm các đáp án vào mảng và câu trả lời
        public Question(string questionText, string optionA, string optionB, string optionC, string optionD, string answer)
        {
            this.options = new string[4];
            this.questionText = questionText;
            this.options[0] = optionA;
            this.options[1] = optionB;
            this.options[2] = optionC;
            this.options[3] = optionD;
            this.answer = answer;
        }

        // Kiểm tra nếu đáp án được lựa chọn là đúng
        public bool checkAnswer(string selectedOption)
        {
            string answer = selectedOption.Substring(3, selectedOption.Length - 3);
            if (this.answer == answer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Thay câu hỏi
        public string getQuestionText()
        {
            return questionText;
        }

        //Thay 4 đáp án
        public string[] getOptions()
        {
            return options;
        }
    }
}
