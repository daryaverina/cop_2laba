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
    public partial class TableExcelComponent : Component
    {
        public TableExcelComponent()
        {
            InitializeComponent();
        }

        public TableExcelComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateExcelFileTable<T>(string filename, string filetitle, Dictionary<string, int[]> rowMergeInfo, int[] rowHeight, string[] tableHeader, List<T> listWithData)
        {
            if (listWithData != null && tableHeader.Length != 0 && typeof(T).GetFields().Length == tableHeader.Length)
            {
                Excel.Application xlApp = new Excel.Application();
                object misValue = System.Reflection.Missing.Value;
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Name = filetitle;

                int columnCellInd = 1;
                int startRowCell = 1;

                var propList = typeof(T).GetFields();

                var tmpList = new List<int>();
                //лист со всеми номерками строк, которые надо объединить вертикально
                foreach (var value in rowMergeInfo.Values)
                {
                    tmpList.AddRange(value);
                }

                //объединяем две соседние ячейки по горизонтали, если они не в группке
                int k = startRowCell;
                for (int i = 0; i < tableHeader.Length; i++)
                {
                    if (!tmpList.Contains(k))
                    {
                        xlWorkSheet.Range[xlWorkSheet.Cells[k, columnCellInd], xlWorkSheet.Cells[k, columnCellInd + 1]].Merge(misValue);
                        xlWorkSheet.Range[xlWorkSheet.Cells[k, columnCellInd], xlWorkSheet.Cells[k, columnCellInd + 1]].BorderAround(true);
                    }
                    k++;
                }

                //объединяем по группкам, заполняем группки в шапке (1 столбик)
                foreach (var item in rowMergeInfo)
                {
                    xlWorkSheet.Range[xlWorkSheet.Cells[item.Value[0], columnCellInd], xlWorkSheet.Cells[item.Value[0] + item.Value.Length - 1, columnCellInd]].Merge(misValue);
                    xlWorkSheet.Range[xlWorkSheet.Cells[item.Value[0], columnCellInd], xlWorkSheet.Cells[item.Value[0] + item.Value.Length - 1, columnCellInd]].Value = item.Key;
                    xlWorkSheet.Range[xlWorkSheet.Cells[item.Value[0], columnCellInd], xlWorkSheet.Cells[item.Value[0] + item.Value.Length - 1, columnCellInd]].BorderAround(true);
                }

                //заполняем второй столбик шапки
                for (int i = 0; i < tableHeader.Length; i++)
                {
                    if (!tmpList.Contains(i + 1))
                    {
                        xlWorkSheet.Cells[i + 1, columnCellInd] = tableHeader[i];
                    }
                    else
                    {
                        xlWorkSheet.Cells[i + 1, columnCellInd + 1] = tableHeader[i];
                        xlWorkSheet.Cells[i + 1, columnCellInd + 1].BorderAround(true);
                    } 
                }

                //шапка жирным шрифтом и высота строчек
                var headerRange = xlWorkSheet.Range[xlWorkSheet.Cells[startRowCell, columnCellInd], xlWorkSheet.Cells[startRowCell + tableHeader.Length - 1, columnCellInd + 1]];
                headerRange.Font.Bold = true;
                for (int i = 0; i < rowHeight.Length; i++)
                {
                    xlWorkSheet.Cells[i + startRowCell, columnCellInd].RowHeight = rowHeight[i];
                    xlWorkSheet.Cells[i + startRowCell, columnCellInd].BorderAround(true);
                }

                //заполняем столбцы таблицы данными
                int j = columnCellInd + 2;
                k = startRowCell;
                foreach (var item in listWithData)
                {
                    foreach (var prop in propList)
                    {
                        xlWorkSheet.Cells[k, j] = prop.GetValue(item).ToString();
                        k++;
                    }
                    j++;
                    k = startRowCell;
                }

                var tableRange = xlWorkSheet.Range[xlWorkSheet.Cells[startRowCell, columnCellInd], xlWorkSheet.Cells[startRowCell + propList.Length - 1, columnCellInd + listWithData.Count + 1]];
                tableRange.Columns.AutoFit();

                xlWorkBook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Workbooks.Close();
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
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
