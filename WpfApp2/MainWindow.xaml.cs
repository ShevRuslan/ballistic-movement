using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Reflection;
using System.Windows.Shapes;
using System.Globalization;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ChartsLineSeries> InformationCharts = new List<ChartsLineSeries>();
        private uint count = 0;//count lineseries


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void math_Click(object sender, RoutedEventArgs e)
        {
            List<double> X = new List<double>();//x - coordinates
            List<double> Y = new List<double>();//y - coordinates
            Brush color = PickBrush();// get random color
            Brush fillColor = (SolidColorBrush)(new BrushConverter().ConvertFromString(color.ToString()));
            fillColor.Opacity = .08;

            try
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();//value (x and y coordinates)
                double _speed = Convert.ToDouble(speed.Text, System.Globalization.CultureInfo.InvariantCulture);//get speed from tb
                double _angle = Convert.ToDouble(angle.Text, System.Globalization.CultureInfo.InvariantCulture);//get angle from tb
                double _time  = Convert.ToDouble(time.Text, System.Globalization.CultureInfo.InvariantCulture);//get time from tb
                double maxRange = Math.Round(_speed * Math.Cos(_angle * Math.PI / 180) * _time);//make maxrange
                double maxHeight = Math.Round(Math.Pow(_speed, 2) * Math.Pow(Math.Sin(_angle * Math.PI / 180), 2) / 20);//make maxheight
                var _ch = (CartesianChart)LogicalTreeHelper.FindLogicalNode(TestGrid, "ch1");//find element by parent and name
                count++;// make 1 line series
                

                for (double i = 0; i <= _time; i++)
                {
                    var x = _speed * Math.Cos(_angle * Math.PI / 180) * i;//get x coordinates
                    var y = _speed * Math.Sin(_angle * Math.PI / 180) * i - (10 * Math.Pow(i, 2) / 2);//get y coordinates

                    X.Add(Math.Round(x));//add x coordinates to list 
                    Y.Add(Math.Round(y));//add y coordinates to list
                }
                for (int i = 0; i < X.Count; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = X[i],
                        Y = Y[i]
                    });
                }

                if (TestGrid.Children.Count == 0  || _ch == null)//if parend dont have children or element does not exist
                {
                    CartesianChart ch = new CartesianChart();
                    LineSeries line = MakeLine(List1Points, fillColor, (int) count, color);//get lineseries by custom function
                    ch.Name = "ch1";//make name
                    ch.Series = new SeriesCollection
                    {
                        line
                    };
                    DefaultLegend legend = new DefaultLegend();
                    ch.ChartLegend = legend;
                    ch.LegendLocation = LegendLocation.Bottom;
                    TestGrid.Children.Add(ch);//add cartesian to grid
                }
                else
                {
                    _ch.Series.Add(MakeLine(List1Points, fillColor, (int) count, color));//if element exiist - add new line series by custom function
                }
                InformationCharts.Add(new ChartsLineSeries("График " + count.ToString(), _speed.ToString(), _angle.ToString(), _time.ToString(), maxHeight.ToString(), maxRange.ToString(), color));
            }
            catch(Exception )
            {
                MessageBox.Show("Произошла ошибка! Проверье данные на валидность.");// if angle or speed or time == null
            }
        }
        //random colors pick
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
        //make new line series
        private LineSeries MakeLine(ChartValues<ObservablePoint> values, Brush fillColor, int count, Brush color)
        {
            return new LineSeries
            {
                Title = "График " + count,
                Values = values,
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 15,
                Fill = fillColor,
                LineSmoothness = 0,
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

