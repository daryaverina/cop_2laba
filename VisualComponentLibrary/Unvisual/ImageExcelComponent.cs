using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;

namespace VisualComponentLibrary.Unvisual
{
    public partial class ImageExcelComponent : Component
    {
        public ImageExcelComponent()
        {
            InitializeComponent();
        }

        public ImageExcelComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateExcelFileImage(string filename, string filetitle, string[] images)
        {
            Excel.Application xlApp = new Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Name = filetitle;
            int x = 0;
            int y = 0;
            float width;
            float height;
            foreach (var image in images)
            {
                Image newImage = Image.FromFile(image);
                width = newImage.Width;
                height = newImage.Height;
                xlWorkSheet.Shapes.AddPicture(image, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, x, y, width, height);
                y += (int)height;
            }

            xlWorkBook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlApp);
            releaseObject(xlWorkBook);
            releaseObject(xlWorkSheet);
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
