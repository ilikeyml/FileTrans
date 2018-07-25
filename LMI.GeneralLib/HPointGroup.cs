using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace LMI.GeneralLib
{
    public class HPointGroup
    {

        HTuple rowGroup = new HTuple();
        HTuple columnGroup = new HTuple();
        readonly int count;

        public HPointGroup()
        {

        }

        public HPointGroup(HTuple rowValue, HTuple columnValue)
        {
            this.rowGroup.Append(rowValue);
            this.columnGroup.Append(columnGroup);
        }

        public HTuple RowGroup { get => rowGroup; set => rowGroup = value; }
        public HTuple ColumnGroup { get => columnGroup; set => columnGroup = value; }
        public int Count { get =>this.rowGroup.Length; }
    }
}
