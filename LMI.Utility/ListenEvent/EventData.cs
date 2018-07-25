using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMI.Utility
{
    public class EventData
    {
        private object data;

        public EventData()
        {
            this.data = "";
        }

        /// <summary>
        /// Object
        /// </summary>
        /// <param name="obj"></param>
        public EventData(object obj)
        {
            this.data = "";
            this.data = obj;
        }

        /// <summary>
        /// Data
        /// </summary>
        public object Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public static explicit operator List<object>(EventData v)
        {
            throw new NotImplementedException();
        }
    }
}
