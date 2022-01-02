using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLaTrieuPhu
{
    class PollResults
    {
        // Attributes
        private char optionKey;
        private int value;

        // Constructor to set values of attributes
        public PollResults(String optionKey, int value)
        {
            this.optionKey = Convert.ToChar(optionKey);
            this.value = value;
        }

        // Get option value poll corresponds to
        public char getoptionKey()
        {
            return this.optionKey;
        }

        // Get value for poll
        public int getValue()
        {
            return this.value;
        }
    }
}
