using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Section = MigraDoc.DocumentObjectModel.Section;

namespace VisualComponentLibrary.Unvisual
{
    public partial class pdf_tables : Component
    {
        public pdf_tables()
        {
            InitializeComponent();
        }

        public pdf_tables(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        [Obsolete]
        public void SaveTables(string folder, string docTitle, List<string[,]> tables)
        {
            if (!IsFull(tables))
            {
                throw new Exception("Таблица не заполнена");
            }

            var document = new Document();

            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(docTitle);
            paragraph.Format.SpaceAfter = 15;
            paragraph.Format.Alignment = ParagraphAlignment.Left;

            foreach (string[,] strings in tables)
            {
                var table = document.LastSection.AddTable();
                table.Borders.Width = 1;
                //столбцы
                for (int i = 0; i < strings.GetLength(1); i++)
                {
                    table.AddColumn();
                }
                //строки
                for (int i = 0; i < strings.GetLength(0); i++)
                {
                    var row = table.AddRow();
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        row.Cells[j].AddParagraph(strings[i, j]);
                    }
                }
                Paragraph prg = section.AddParagraph();
                prg.Format.SpaceAfter = 15;
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(folder);
            Console.WriteLine("Table created");
        }

        private Boolean IsFull(List<string[,]> tables)
        {
            foreach (string[,] table in tables)
            {
                foreach (string str in table)
                {
                   // Console.WriteLine(str);
                    if (str == null) return false;
                }
            }
            return true;
        }
    }
}
