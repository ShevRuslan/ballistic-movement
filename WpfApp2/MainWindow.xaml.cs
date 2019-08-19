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
        Chart chart = new Chart();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void math_Click(object sender, RoutedEventArgs e)
        {
            double _speed = Convert.ToDouble(speed.Text, System.Globalization.CultureInfo.InvariantCulture);//get speed from tb
            double _angle = Convert.ToDouble(angle.Text, System.Globalization.CultureInfo.InvariantCulture);//get angle from tb
            double _time = Convert.ToDouble(time.Text, System.Globalization.CultureInfo.InvariantCulture);//get time from tb

            var _ch = (CartesianChart)LogicalTreeHelper.FindLogicalNode(TestGrid, "ch1");//find element by parent and name

            if (TestGrid.Children.Count == 0 || _ch == null)//if parend dont have children or element does not exist
            {
                CartesianChart ch = new CartesianChart();
                //LineSeries line = MakeLine(List1Points, fillColor, (int) count, color);//get lineseries by custom function
                ch.Name = "ch1";//make name
                ch.Series = new SeriesCollection();
                DefaultLegend legend = new DefaultLegend();
                ch.ChartLegend = legend;
                ch.LegendLocation = LegendLocation.Bottom;
                TestGrid.Children.Add(ch);//add cartesian to grid
                _ch = ch;
            }
            chart.CreateChart(_speed, _angle, _time, _ch);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            chart.ViewTableInformation();
        }
    }
}

