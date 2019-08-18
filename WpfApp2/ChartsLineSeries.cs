using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp2
{
    class ChartsLineSeries
    {
        public string Title { get; private set; }
        public string Speed { get; private set; }
        public string Angle { get; private set; }
        public string Time { get; private set; }
        public string Height { get; private set; }
        public string Range { get; private set; }
        public Brush Ell { get; private set; }

        public ChartsLineSeries(string title, string speed, string angle, string time, string height, string range, Brush ell)
        {
            Title = title;
            Speed = speed;
            Angle = angle;
            Time = time;
            Height = height;
            Range = range;
            Ell = ell;
        }
    }
}
