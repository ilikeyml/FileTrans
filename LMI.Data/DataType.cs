using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMI.Data
{
    public struct MeasurementsStuct
    {
        ushort id;
        double value;

        public MeasurementsStuct(ushort id, double value)
        {
            this.id = id;
            this.value = value;
        }

        public ushort Id { get => id; set => id = value; }
        public double Value { get => value; set => this.value = value; }
    };



}
