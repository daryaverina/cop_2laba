using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace VisualComponentLibrary.Unvisual
{
    public partial class DiagramExcelComponent : Component
    {
        public DiagramExcelComponent()
        {
            InitializeComponent();
        }

        public DiagramExcelComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateExcelFileDiagram(string filename, string filetitle, string diagramtitle, LegendLocation location, Dictionary<string, int> dictionary)
        {
            Excel.Application xlApp = new Excel.Application(); 
            object misValue = System.Reflection.Missing.Value;
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Name = filetitle;

            int row = 1;
            foreach (var item in dictionary)
            {
                xlWorkSheet.Cells[row, 1] = item.Key;
                xlWorkSheet.Cells[row, 2] = item.Value;
                row++;
            }

            Excel.ChartObjects xlDiagrams = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myDiagram = xlDiagrams.Add(120, 0, 300, 250);
            Excel.Chart diagramPage = myDiagram.Chart;
            diagramPage.ChartType = Excel.XlChartType.xlPie;
            diagramPage.HasLegend = true;
            diagramPage.HasTitle = true;
            diagramPage.ChartTitle.Text = diagramtitle;

            switch (location)
            {
                case LegendLocation.Top:
                    diagramPage.Legend.Position = Excel.XlLegendPosition.xlLegendPositionTop;
                    break;
                case LegendLocation.Bottom:
                    diagramPage.Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom;
                    break;
                case LegendLocation.Left:
                    diagramPage.Legend.Position = Excel.XlLegendPosition.xlLegendPositionLeft;
                    break;
                case LegendLocation.Right:
                    diagramPage.Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight;
                    break;
            }

            Excel.Range diagramRange = xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[dictionary.Count, 2]];
            diagramRange.Columns.AutoFit();
            diagramPage.SetSourceData(diagramRange, misValue);

            xlWorkBook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
