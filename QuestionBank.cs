using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AiLaTrieuPhu
{
    public class QuestionBank
    {
        // Thuộc tính
        private List<Question> questions = new List<Question>();
        private databaseHelper databaseHelper = null;

        // Cấu trúc thay câu hỏi và phù hợp với bộ câu hỏi
        public QuestionBank()
        {
            databaseHelper = new databaseHelper();
            setQuestions();

        }

        // Đặt 15 câu hỏi chính
        public void setQuestions()
        {
            SQLiteDataReader dataset = databaseHelper.importNQuestions(15);

            while (dataset.Read())
            {
                this.questions.Add(new Question(dataset.GetString(1), dataset.GetString(2), dataset.GetString(3), dataset.GetString(4), dataset.GetString(5), dataset.GetString(6)));

            }
        }

        // Thay câu hỏi
        public Question getQuestion(int questionNumber)
        {
            return questions[questionNumber];
        }

    }
}
