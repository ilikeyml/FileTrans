using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMI.Utility
{
    public class ListenEventArgs : EventArgs
    {
        private EventData iData;

        public ListenEventArgs(EventData iData)
        {
            this.iData = iData;
        }

        public EventData IData
        {
            get
            {
                return this.iData;
            }
            set
            {
                this.iData = value;
            }
        }
    }
}
