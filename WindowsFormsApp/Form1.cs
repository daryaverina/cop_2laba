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

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //checkedList - choiceControl
            List<string> listForChoiceControl = new List<string>() { "sunny", "bright", "cloudy", "fine", "clear", "humid", "foggy", "overcast", "windy" };
            choiceUserControl.AddItems(listForChoiceControl);

            //dateTimePicker - inputControl
            inputUserControl.MaxDate = new DateTime(2022, 9, 22, 23, 59, 59); 
            inputUserControl.MinDate = new DateTime(2022, 9, 17, 23, 59, 59); 
            inputUserControl.SelectItemProperty = DateTime.Now;

            //treeView - outputControl
            List<Place> placeList = new List<Place>();
            placeList.Add(new Place("Russia", "Moscow", "Arbat"));
            placeList.Add(new Place("Russia", "Moscow", "Tverskoi"));
            placeList.Add(new Place("Russia", "Moscow", "Sokol"));
            placeList.Add(new Place("Great Britian", "London", "Greenwich"));
            placeList.Add(new Place("Great Britian", "London", "Kensington"));
            placeList.Add(new Place("USA", "New York", "Manhattan"));
            placeList.Add(new Place("USA", "New York", "Brooklyn"));
            outputUserControl.CreateTree(placeList);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var elem = outputUserControl.GetSelectedNode<Place>();
            if (elem != null) MessageBox.Show(elem.country + ", " + elem.city + ", " + elem.district, "treeView", MessageBoxButtons.OK);
        }

        private void buttonImage_Click(object sender, EventArgs e)
        {
            string[] images = new string[] { "F:\\cat-frog.jpg", "F:\\cherry.jpg" };

            ImageExcelComponent comp1 = new ImageExcelComponent();
            comp1.CreateExcelFileImage("C:\\Users\\Tamara\\Desktop\\ExcelImage.xls", "Изображения", images);
        }

        private void buttonDiagram_Click(object sender, EventArgs e)
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("Chamomile", 343);
            dictionary.Add("Lavender", 122);
            dictionary.Add("Cornflower", 10);
            dictionary.Add("Petunia", 102);
            dictionary.Add("Tulip", 88);

            DiagramExcelComponent comp2 = new DiagramExcelComponent();
            comp2.CreateExcelFileDiagram("C:\\Users\\Tamara\\Desktop\\ExcelDiagram.xls", "Цветы", "Количество цветов в букете", LegendLocation.Left, dictionary);
        }
    }
}
