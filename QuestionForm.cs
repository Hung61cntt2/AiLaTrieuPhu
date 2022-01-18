using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.OleDb;

namespace AiLaTrieuPhu
{
    public partial class QuestionForm : Form
    {
        SQLiteConnection connection;
        SQLiteCommand command;
        string str = "Data Source=QuestionDataBase.db; Version = 3; New = True; Compress = True; ";
        SQLiteDataAdapter adapter = new SQLiteDataAdapter();
        DataTable table = new DataTable();

        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Questions";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            DGV.DataSource = table;
        }

        public QuestionForm()
        {
            InitializeComponent();
        }

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(str);
            connection.Open();
            LoadData();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Hiển thị khi kích đúp vào các cột dữ liệu
            int i;
            i = DGV.CurrentRow.Index;
            txbID.Text = DGV.Rows[i].Cells[0].Value.ToString();
            txbQuestion.Text = DGV.Rows[i].Cells[1].Value.ToString();
            txbOptionA.Text = DGV.Rows[i].Cells[2].Value.ToString();
            txbOptionB.Text = DGV.Rows[i].Cells[3].Value.ToString();
            txbOptionC.Text = DGV.Rows[i].Cells[4].Value.ToString();
            txbOptionD.Text = DGV.Rows[i].Cells[5].Value.ToString();
            txbAnswer.Text = DGV.Rows[i].Cells[6].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Questions VALUES('"+ txbID.Text + "' , '" + txbQuestion.Text + "', '" + txbOptionA.Text + "', '" + txbOptionB.Text + "', '" + txbOptionC.Text + "', '" + txbOptionD.Text + "' , '" + txbAnswer.Text + "')";
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm câu hỏi thành công");
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Questions WHERE ID = '" + txbID.Text + "'";
            command.ExecuteNonQuery();
            MessageBox.Show("Xóa câu hỏi thành công");
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "UPDATE Questions SET Question = '" + txbQuestion.Text + "', OptionA = '" + txbOptionA.Text + "', OptionB = '" + txbOptionB.Text + "', OptionC = '" + txbOptionC.Text + "', OptionD = '" + txbOptionD.Text + "', Answer = '" + txbAnswer.Text + "' WHERE ID = '" + txbID.Text + "'";
            command.ExecuteNonQuery();
            MessageBox.Show("Sửa câu hỏi thành công");
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txbID.Text = "";
            txbQuestion.Text = "";
            txbOptionA.Text = "";
            txbOptionB.Text = "";
            txbOptionC.Text = "";
            txbOptionD.Text = "";
            txbAnswer.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Login Form = new Login();
            Form.Show();
            this.Hide();
        }
    }
}
