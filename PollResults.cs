using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLaTrieuPhu
{
    class PollResults
    {
        // Thuộc tính
        private char optionKey;
        private int value;

        // Cấu trúc để cài đặt giá trị cho thuộc tính
        public PollResults(String optionKey, int value)
        {
            this.optionKey = Convert.ToChar(optionKey);
            this.value = value;
        }

        // Lấy giá trị tùy chọn thăm dò tương ứng
        public char getoptionKey()
        {
            return this.optionKey;
        }

        // Lấy giá trị từ thăm dò
        public int getValue()
        {
            return this.value;
        }
    }
}
