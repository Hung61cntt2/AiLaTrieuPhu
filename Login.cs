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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Kết nối đến csdl và tạo mới lệnh
            SQLiteConnection connection = new SQLiteConnection("Data Source=TaiKhoanDataBase.db; Version = 3; New = True; Compress = True; ");
            try
            {
                connection.Open();
                string TK = txbTaikhoan.Text;
                string MK = txbMatkhau.Text;
                string sqlite = "SELECT * FROM TaiKhoan WHERE TaiKhoan='" + TK + "' AND MatKhau='" + MK + "'";
                SQLiteCommand cmd = new SQLiteCommand(sqlite, connection);
                SQLiteDataReader data = cmd.ExecuteReader();
                if(data.Read() == true)
                {
                    QuestionForm Form = new QuestionForm();
                    Form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }

        }

        private void btnRule_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Người chơi phải trả lời 15 câu hỏi." +
                             " Có ba mốc quan trọng là câu số 5, câu số 10 và câu số 15." +
                             " Vượt qua tất cả các mốc này, họ chắc chắn có được số tiền thưởng tương ứng của các câu hỏi đó." +
                             " Nếu chơi tiếp mà trả lời sai, cuộc chơi khép lại và người chơi nhận số tiền thưởng tương ứng với mốc quan trọng gần nhất." +
                             " Nếu trả lời sai khi chưa qua câu số 5, người chơi sẽ không nhận được tiền thưởng." +
                             " Trả lời đúng tất cả các câu hỏi, người chơi sẽ trở thành TRIỆU PHÚ và nhận được tiền thưởng tương ứng với câu hỏi cuối cùng." +
                             " Người chơi có 3 quyền trợ giúp và có thể sử dụng bất cứ lúc nào nếu không biết câu trả lời hoặc chưa chắc chắn với suy nghĩ của mình." +
                             " Trong một câu hỏi, người chơi có quyền dùng nhiều quyền trợ giúp, nhưng tất cả quyền trợ giúp chỉ được sử dụng một lần duy nhất.");
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Form1 MainForm = new Form1();
            MainForm.Show();
            this.Hide();

        }

        
    }
}
