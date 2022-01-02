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
        // Attributes
        private List<Question> questions = new List<Question>();
        private Question lifeLineSwapQuestion = null;
        private databaseHelper databaseHelper = null;

        // Constructor to retrieve and accordingly set questions
        public QuestionBank()
        {
            databaseHelper = new databaseHelper();
            setQuestions();
            setLifeLineSwapQuestion();

        }

        // Set the main 15 questions
        public void setQuestions()
        {
            SQLiteDataReader dataset = databaseHelper.importNQuestions(15);

            while (dataset.Read())
            {
                this.questions.Add(new Question(dataset.GetString(1), dataset.GetString(2), dataset.GetString(3), dataset.GetString(4), dataset.GetString(5), dataset.GetString(6)));

            }
        }

        // Set question for the swap lifeline
        public void setLifeLineSwapQuestion()
        {
            SQLiteDataReader dataset = databaseHelper.importNQuestions(1);
            dataset.Read();
            this.lifeLineSwapQuestion = new Question(dataset.GetString(1), dataset.GetString(2), dataset.GetString(3), dataset.GetString(4), dataset.GetString(5), dataset.GetString(6));
        }

        // Retrieve a question
        public Question getQuestion(int questionNumber)
        {
            return questions[questionNumber];
        }

    }
}
