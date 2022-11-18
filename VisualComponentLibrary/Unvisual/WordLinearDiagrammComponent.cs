using VisualComponentLibrary.Unvisual.HelperModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace VisualComponentLibrary.Unvisual
{
    public partial class WordLinearDiagrammComponent : Component
    {
        public WordLinearDiagrammComponent()
        {
            InitializeComponent();
        }

        public WordLinearDiagrammComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        // Метод создания отчета
        public void Report(string fileName, string titleDoc, string nameDiagram, List<TestData> data, List<string> nameElem)
        {
            if (String.IsNullOrEmpty(fileName) || String.IsNullOrEmpty(titleDoc) || String.IsNullOrEmpty(nameDiagram) || data.Count == 0 || data[0].value.Length == nameElem.Count)
            {
              //    throw new Exception(nameDiagram);
            }
            CreateDoc(fileName, titleDoc, nameDiagram, data, nameElem);

        }

        // Создание документа
        private void CreateDoc(string fileName, string titleDoc, string nameDiagram, List<TestData> data, List<string> nameElem)
        {
            try
            {
                DocX document = DocX.Create(fileName);
                document.InsertParagraph(titleDoc);
                document.Paragraphs[0].Direction = Direction.RightToLeft;
                document.Paragraphs[0].Alignment = Alignment.center;
                document.Paragraphs[0].FontSize(20);
                document.Paragraphs[0].Bold();
                document.InsertChart(CreateLinearChart(ChartLegendPosition.Bottom, nameDiagram, data, nameElem));
                document.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Метод создания диаграммы
        private static Chart CreateLinearChart(ChartLegendPosition chartLegendPosition, string nameDiagram, List<TestData> data, List<string> nameElem)
        {
            // создаём линейную диаграмму
            LineChart lineChart = new LineChart();
            // добавляем легенду 
            lineChart.AddLegend(chartLegendPosition, false);
            Series series = new Series(nameDiagram);
            for (int i = 0; i < data.Count; i++)
            {
                // создаём набор данных
                series = new Series(data[i].name);
                // заполняем данными
                series.Bind(nameElem, data[i].value);
                // добавляем набор данных на диаграмму
                lineChart.AddSeries(series);
            }
            return lineChart;
        }
    }
}