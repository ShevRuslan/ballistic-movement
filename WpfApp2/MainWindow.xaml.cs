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
        List<ChartsLineSeries> InformationCharts = new List<ChartsLineSeries>();
        private int count = 0;//count lineseries
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void math_Click(object sender, RoutedEventArgs e)
        {
            List<double> X = new List<double>();//x - coordinates
            List<double> Y = new List<double>();//y - coordinates
            DataContext = this;
            Brush color = PickBrush();// get random color
            Brush fillColor = color;
            fillColor.Opacity = .05;//make opacity fill color

            try
            {
                double _speed = Convert.ToDouble(speed.Text);//get speed from tb
                double _angle = Convert.ToDouble(angle.Text);//get angle from tb
                double _time  = Convert.ToDouble(time.Text);//get time from tb
                double maxRange = Math.Round(_speed * Math.Cos(_angle * Math.PI / 180) * _time);//make maxrange
                double maxHeight = Math.Round(Math.Pow(_speed, 2) * Math.Pow(Math.Sin(_angle * Math.PI / 180), 2) / 20);//make maxheight

                for (int i = 0; i <= _time; i++)
                {
                    var x = _speed * Math.Cos(_angle * Math.PI / 180) * i;//get x coordinates
                    var y = _speed * Math.Sin(_angle * Math.PI / 180) * i - (10 * Math.Pow(i, 2) / 2);//get y coordinates

                    X.Add(Math.Round(x));//add x coordinates to list 
                    Y.Add(Math.Round(y));//add y coordinates to list
                }
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();//value (x and y coordinates)

                for (int i = 0; i < X.Count; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = X[i],
                        Y = Y[i]
                    });
                }

                var _ch =(CartesianChart)LogicalTreeHelper.FindLogicalNode(TestGrid, "ch1");//find element by parent and name
                count++;// make 1 line series
                if (TestGrid.Children.Count == 0  || _ch == null)//if parend dont have children or element does not exist
                {
                    CartesianChart ch = new CartesianChart();
                    LineSeries line = MakeLine(List1Points, fillColor, count, color);//get lineseries by custom function
                    ch.Name = "ch1";//make name
                    ch.Series = new SeriesCollection
                    {
                        line
                    };
                    DefaultLegend legend = new DefaultLegend();
                    ch.ChartLegend = legend;
                    ch.LegendLocation = LegendLocation.Right;
                    TestGrid.Children.Add(ch);//add cartesian to grid
                }
                else
                {
                    _ch.Series.Add(MakeLine(List1Points, fillColor, count, color));//if element exiist - add new line series by custom function
                }
                MessageBox.Show("Максимальная дальность: " + maxRange + "\n" + "Максимальная высота: " + maxHeight);//output information about charts
                InformationCharts.Add(new ChartsLineSeries { Title = "График " + count.ToString(), Angle = _angle.ToString(), Height = maxHeight.ToString(), Range = maxRange.ToString(), Speed = _speed.ToString(), Time = _time.ToString()});
            }
            catch(Exception)
            {
                MessageBox.Show("Введите валидные данные!");// if angle or speed or time == null
            }
        }
        //random colors pick
        private Brush PickBrush()
        {
            Random r = new Random();
            Brush brush = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
            return brush;
        }
        //make new line series
        private LineSeries MakeLine(ChartValues<ObservablePoint> values, Brush fillColor, int count, Brush color)
        {
            return new LineSeries
            {
                Title = "График " + count,
                Values = values,
                PointGeometry = DefaultGeometries.Diamond,
                PointGeometrySize = 15,
                Fill = fillColor,
                LineSmoothness = 1,
                Name = "series" + count,
                Stroke = color,
            };
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TableInfromation _form = new TableInfromation();
            _form.Show();
            _form.dataGrid.ItemsSource = InformationCharts;
        }
    }
}

