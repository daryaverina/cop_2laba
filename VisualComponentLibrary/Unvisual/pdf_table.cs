using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisualComponentLibrary.Unvisual.HelperModels;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace VisualComponentLibrary.Unvisual
{
    public partial class pdf_table : Component
    {
        public pdf_table()
        {
            InitializeComponent();
        }

        public pdf_table(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void SaveTable<T>(string folder, string docTitle, List<PdfColumnInfo> columns,
           PdfRowInfo[] rows, List<T> objList)
        {

            foreach (PdfColumnInfo column in columns)
            {
                if (column.Column_name == null)
                {
                    throw new Exception("columns empty");
                }
            }

            PdfPTable table = CreateTable(columns, rows, objList);
            FileStream fs = new FileStream(folder, FileMode.Create);
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            document.Add(new Paragraph(docTitle));
            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }

        private PdfPTable CreateTable<T>(List<PdfColumnInfo> columns, PdfRowInfo[] rows, List<T> objList)
        {
            float[] widths = new float[columns.Count];
           // Console.WriteLine(widths.ToString());
            float[] widths1 = new float[] { 20, 20, 20 , 10, 20};
            Console.WriteLine(widths1);
            bool widthsExist = true;
            foreach (var column in columns)
            {
                if (column.Width == null)
                {
                    widthsExist = false;
                }
            }
            if (widthsExist)
            {
                int index = 0;
                foreach (var column in columns)
                {
                    widths[index] = (float)column.Width;
                    Console.WriteLine(widths[index]);

                    index++;
                }
            }
            bool heightsExist = true;
            foreach (var row in rows)
            {
                Console.WriteLine(row.Height);

                if (row.Height == null)
                {
                    heightsExist = false;
                }
            }

            PdfPTable table = new PdfPTable(columns.Count);

            if (widthsExist)
            {
                table.SetWidths(widths);
            }

            BaseFont cellsFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font headerFont = new Font(cellsFont, 16, Font.BOLD);
            columns[0].Width = 20;
            //top headers
            foreach (var column in columns)
            {
                Console.WriteLine(column.Title);
                PdfPCell cell = new PdfPCell(new Phrase(column.Title, headerFont));
                if (heightsExist) cell.FixedHeight = (float)rows[0].Height;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
            }

            int rowNumber = 1;
            //remaining rows
            foreach (var item in objList)
            {
                foreach (var column in columns)
                {    
                    PropertyInfo propertyInfo = item.GetType().GetProperty(column.Column_name);
                    
                    string value = propertyInfo.GetValue(item).ToString();
                    //Console.WriteLine("propinfo " + propertyInfo);
                    PdfPCell cell = new PdfPCell(new Phrase(value));
                    if (heightsExist) cell.FixedHeight = (float)rows[rowNumber].Height;
                    table.AddCell(cell);
                }
                rowNumber++;
            }

            //left headers
            foreach (var row in table.Rows)
            {
                PdfPCell cell = row.GetCells()[0];
                string text = cell.Phrase.Content;
                PdfPCell newcell = new PdfPCell(new Phrase(text, headerFont));
                newcell.BackgroundColor = BaseColor.LIGHT_GRAY;
                Console.WriteLine(text);
                row.GetCells()[0] = newcell;
            }

            return table;
        }
    }
}
