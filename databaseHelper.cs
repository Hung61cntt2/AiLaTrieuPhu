using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AiLaTrieuPhu
{
    public class databaseHelper
    {
        private OleDbConnection connection = null;

        // Kết nối
        private SQLiteConnection connect()
        {
            // Tạo mới 1 csdl
            SQLiteConnection connection = new SQLiteConnection("Data Source=QuestionDataBase.db; Version = 3; New = True; Compress = True; ");
            // Kết nối
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return connection;
        }

        // Nhập n câu hỏi từ csdl
        public SQLiteDataReader importNQuestions(int n)
        {
            // Kết nối đến csdl và tạo mới lệnh
            SQLiteConnection connection = connect();
            SQLiteCommand command = connection.CreateCommand();

            // Tạo hàng chờ và xử lý
            command.CommandText = "SELECT * FROM Question WHERE ID IN (SELECT ID FROM Question ORDER BY RANDOM() LIMIT " + n + ")";
            SQLiteDataReader dataReader = command.ExecuteReader();

            // Ngắt kết nối và trả dữ liệu về csdl
            disconnect();
            return dataReader;

        }

        // Nếu kết nối không được mở
        private void disconnect()
        {
            if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        }
    }
}
