using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AiLaTrieuPhu
{
    public class LinkedList
    {
        // Đầu danh sách
        private LinkedListNode head;

        // Thiết lập đầu danh sách
        public LinkedList()
        {
            this.head = null;

        }

        // Thêm 1 Node đến cuối cùng danh sách
        public void addToList(LinkedListNode next)
        {
            if (head == null)
            {
                head = next;
            }
            else
            {

                LinkedListNode node = head;
                while (node.getNext() != null)
                {
                    node = node.getNext();
                }

                node.setNext(next);
            }
        }

        // Quay lại đầu danh sách
        public LinkedListNode getHead()
        {
            return head;
        }
    }
}
