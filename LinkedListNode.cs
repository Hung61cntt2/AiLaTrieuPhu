using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AiLaTrieuPhu
{
    public class LinkedListNode
    {
        // Thuộc tính giải thưởng, checkpoint 
        private Button prize;
        private Boolean checkpoint;

        // Node tiếp theo trong danh sách liên kết
        private LinkedListNode next = null;

        // Cấu trúc (không có Node tiếp theo)
        public LinkedListNode(Button prize, Boolean checkpoint)
        {
            this.prize = prize;
            this.checkpoint = checkpoint;

        }

        // Quay lại giải thưởng
        public Button getPrize()
        {
            return this.prize;
        }

        // Quay lại checkpoint
        public Boolean getCheckpoint()
        {
            return this.checkpoint;
        }

        // Quay lại Node tiếp theo
        public LinkedListNode getNext()
        {
            return this.next;
        }

        public void setNext(LinkedListNode next)
        {
            this.next = next;
        }

        public void resetBackground()
        {
            if (this.checkpoint)
            {
                this.prize.BackgroundImage = Properties.Resources.checkpoint;
            }
            else
            {
                this.prize.BackgroundImage = Properties.Resources.button;
            }

        }

        public void setPrizeBackground()
        {
            this.prize.BackgroundImage = Properties.Resources.prize;

        }

        public void setWrongBackground()
        {
            this.prize.BackgroundImage = Properties.Resources.wrong;

        }
    }
}
