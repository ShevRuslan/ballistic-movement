using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;

namespace WpfApp2
{
    class ChartsLineSeries
    {
        public string Title { get; private set; }
        public string Name { get; private set; }
        public double Speed { get; private set; }
        public double Angle { get; private set; }
        public double Time { get; private set; }
        public double Height { get; private set; }
        public double Range { get; private set; }
        public Brush Ell { get; private set; }

        public ChartsLineSeries(double speed, double angle, double time)
        {
            Speed = speed;
            Angle = angle;
            Time = time;
            
        }
        public LineSeries CreateChartLine(string title, string name)
        {
            Title = title;
            Name = name;
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();
            Brush color = PickBrush();// get random color
            Ell = color;
            Brush fillColor = (SolidColorBrush)(new BrushConverter().ConvertFromString(color.ToString()));
            fillColor.Opacity = .08;

            Range = Math.Round(Speed * Math.Cos(Angle * Math.PI / 180) * Time);
            Height = Math.Round(Math.Pow(Speed, 2) * Math.Pow(Math.Sin(Angle * Math.PI / 180), 2) / 20);

            for (double i = 0; i <= Time; i++)
            {
                double x = Speed * Math.Cos(Angle * Math.PI / 180) * i;
                double y = Speed * Math.Sin(Angle * Math.PI / 180) * i - (10 * Math.Pow(i, 2) / 2);

                X.Add(Math.Round(x));
                Y.Add(Math.Round(y));
            }

            for (int i = 0; i < X.Count; i++)
            {
                List1Points.Add(new ObservablePoint
                {
                    X = X[i],
                    Y = Y[i]
                });
            }

            return new LineSeries
            {
                Title = title,
                Values = List1Points,
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 15,
                Fill = fillColor,
                LineSmoothness = 0,
                Name = name,
                Stroke = color,
            };
        }
        private Brush PickBrush()
        {
            Random rnd = new Random();
            Brush result = Brushes.Transparent;
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);
            return result;
        }
    }
}
