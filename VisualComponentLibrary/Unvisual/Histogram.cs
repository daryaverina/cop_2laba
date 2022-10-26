using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.Charting;
using ceTe.DynamicPDF.PageElements.Charting.Axes;
using ceTe.DynamicPDF.PageElements.Charting.Series;
using VisualComponentLibrary.Unvisual.HelperModels;
using System;
using System.ComponentModel;
using Page = ceTe.DynamicPDF.Page;

namespace VisualComponentLibrary.Unvisual
{
    public partial class Histogram : Component
    {
        public Histogram()
        {
            InitializeComponent();
        }

        public Histogram(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public void SaveHistogram(string folder, string docTitle, string histTitle, LegendLocation location, Dictionary<string, int>  dictionary)
        {

            Document document = new Document();
            Page page = new Page();
            document.Pages.Add(page);

            Chart chart = new Chart(0, 100, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Label label = new Label(docTitle, 0, 0, 500, 30, Font.TimesRoman, 18, TextAlign.Center);
            page.Elements.Add(label);

            Title histogramTitle = new Title(histTitle);
            chart.HeaderTitles.Add(histogramTitle);

            IndexedBarSeries barSeries = null;

            foreach (var item in dictionary)
            {
                if (String.IsNullOrEmpty(item.Value.ToString()))
                {
                    throw new Exception("Input data is empty!");
                }
                barSeries = new IndexedBarSeries(item.Key);
                barSeries.Values.Add(item.Value);
                plotArea.Series.Add(barSeries);
                Console.WriteLine(item.Key);
            }
            barSeries.YAxis.Labels.Visible = false;

            //расположение легенды
            switch (location)
            {
                case LegendLocation.Top:
                    chart.Legends.Placement = LegendPlacement.TopCenter;
                    break;
                case LegendLocation.Bottom:
                    chart.Legends.Placement = LegendPlacement.BottomCenter;
                    break;
                case LegendLocation.Left:
                    chart.Legends.Placement = LegendPlacement.LeftCenter;
                    break;
                case LegendLocation.Right:
                    chart.Legends.Placement = LegendPlacement.RightCenter;
                    break;
            }

            page.Elements.Add(chart);
            document.Draw(folder);
        }
    }
}
