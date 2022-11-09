using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualComponentLibrary.Unvisual;
using VisualComponentLibrary.Unvisual.HelperModels;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public class MyObject
        {
            public string svodka { get; set; }
            public string last_name { get; set; }
            public string first_name { get; set; }
            public int age { get; set; }
            public double premium { get; set; }
            override
            public String ToString()
            {
                String obj = svodka + " " + last_name + " " + first_name;
                return obj;
            }
            // public int age { get; set; }
            //   public double premium { get; set; }

        }

        List<MyObject> TestData = new List<MyObject> {
            new MyObject{ svodka = "first",
            last_name = "bobrov",
            first_name = "ivan",
            age = 15,
            premium = 10.000
            },
            new MyObject{ svodka = "second",
            last_name = "Somov",
            first_name = "valera",
            age = 45,
            premium = 15.000
            },
            new MyObject{ svodka = "third",
            last_name = "Karpov",
            first_name = "Andrey",
            age = 20,
            premium = 5.000
            }
        };
        public Form1()
        {
            InitializeComponent();
        }

        


        private void button3_Click(object sender, EventArgs e)
        {
            List<string[,]> list = new List<string[,]>() {
                new string[,]{ { "first name","last name","age"},{ "dasha","verina","20"},{"vasev","vasya","13" }, {"bebov","vanya","40" } },
                new string[,]{ { "country","population","language"},{ "Russia", "144,1", "russian"},{"Usa", "329,5", "english" }, {"Sweden", "10,35", "swedish" } }
            };
            pdf_tables tc = new pdf_tables();
            tc.SaveTables("C:\\Users\\Дарья\\Desktop\\firstComponent.pdf", "Title", list);
        }
        private void button2_Click(object sender, EventArgs e)
        {

            pdf_table MyTable = new pdf_table();
            string folder = "C:\\Users\\Дарья\\Desktop\\secondComponent.pdf";
            string title = "Test";

            List<PdfColumnInfo> columns = new List<PdfColumnInfo>();
            PdfRowInfo[] rows = new PdfRowInfo[4];

            columns.Add(new PdfColumnInfo { Column_name = "svodka", Title = "Svodka", Width = 20});
            columns.Add(new PdfColumnInfo { Column_name = "last_name", Title = "Surname", Width = 20 });
            columns.Add(new PdfColumnInfo { Column_name = "first_name", Title = "Name", Width = 20 });
            columns.Add(new PdfColumnInfo { Column_name = "age", Title = "Age", Width = 10 });
            columns.Add(new PdfColumnInfo { Column_name = "premium", Title = "Premium", Width = 20 });

            rows[0] = new PdfRowInfo() { Height = 60 };
            rows[1] = new PdfRowInfo() { Height = 30 };
            rows[2] = new PdfRowInfo() { Height = 30 };
            rows[3] = new PdfRowInfo() { Height = 30 };

            Console.WriteLine(rows[0]);
            MyTable.SaveTable(folder, title, columns, rows, TestData);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string folder = "C:\\Users\\Дарья\\Desktop\\thirdComponent.pdf";
            string title = "Cities population file";
            string histTitle = "Population";

            Histogram hc = new Histogram();

            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("москва", 10);
            data.Add("Ulyanovsk", 25);
            data.Add("Samara", 19);
            data.Add("Yalta", 27);
            data.Add("Saratov", 45);
            data.Add("Bobrovo", 12);

            hc.SaveHistogram(folder, title, histTitle, LegendLocation.Right, data);
        }
    }
}
