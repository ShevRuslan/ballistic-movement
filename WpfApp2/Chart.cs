using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class Chart
    {
        public uint Count { get; private set; } = 0;
        private readonly List<ChartsLineSeries> InformationCharts = new List<ChartsLineSeries>();

        public void CreateChart(double speed, double angle, double time, CartesianChart parent)
        {
            Count++;
            ChartsLineSeries chartsLineSeries = new ChartsLineSeries(speed, angle, time);
            parent.Series.Add(chartsLineSeries.CreateChartLine("График " + Count, "series" + Count ));//if element exiist - add new line series by custom function
            InformationCharts.Add(chartsLineSeries);
        }
        public void ViewTableInformation()
        {
            TableInfromation _form = new TableInfromation();
            _form.Show();
            _form.dataGrid.ItemsSource = InformationCharts;
        }
    }
}
