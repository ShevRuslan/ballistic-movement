using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void math_Click(object sender, RoutedEventArgs e)
        {
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            DataContext = this;
            Brush color = PickBrush();
            Brush fillColor = color;
            fillColor.Opacity = .05;
            int count = 0;

            try
            {
                double _speed = Convert.ToDouble(speed.Text);
                double _angle = Convert.ToDouble(angle.Text);
                double _time = Convert.ToDouble(time.Text);
                double maxRange = Math.Round(_speed * Math.Cos(_angle * Math.PI / 180) * _time);
                double maxHeight = Math.Round(Math.Pow(_speed, 2) * Math.Pow(Math.Sin(_angle * Math.PI / 180), 2) / 20);

                for (int i = 0; i <= _time; i++)
                {
                    var x = _speed * Math.Cos(_angle * Math.PI / 180) * i;
                    var y = _speed * Math.Sin(_angle * Math.PI / 180) * i - (10 * Math.Pow(i, 2) / 2);

                    X.Add(Math.Round(x));
                    Y.Add(Math.Round(y));
                    count++;
                }
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < X.Count; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = X[i],
                        Y = Y[i]
                    });
                }
                var _ch =(CartesianChart)LogicalTreeHelper.FindLogicalNode(TestGrid, "ch1");

                if (TestGrid.Children.Count == 0  || _ch == null)
                {
                    CartesianChart ch = new CartesianChart();
                    LineSeries line = MakeLine(List1Points, fillColor, count, color);
                    ch.Name = "ch1";
                    ch.Series = new SeriesCollection
                    {
                        line
                    };
                    TestGrid.Children.Add(ch);
                }
                else
                {
                    _ch.Series.Add(MakeLine(List1Points, fillColor, count, color));
                }
                MessageBox.Show("Максимальная дальность: " + maxRange + "\n" + "Максимальная высота: " + maxHeight);
            }
            catch(Exception)
            {
                MessageBox.Show("Введите валидные данные!");
            }
        }

        private Brush PickBrush()
        {
            Random r = new Random();
            Brush brush = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
            return brush;
        }

        private LineSeries MakeLine(ChartValues<ObservablePoint> values, Brush fillColor, int count, Brush color)
        {
            return new LineSeries
            {
                Title = "Series 1",
                Values = values,
                Fill = fillColor,
                LineSmoothness = 1,
                Name = "series" + count,
                Stroke = color,
            };
        }
    }
}

