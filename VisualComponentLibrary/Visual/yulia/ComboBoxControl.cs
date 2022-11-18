using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualComponentLibrary.Visual.yulia
{
    public partial class ComboBoxControl : UserControl
    {
        private event EventHandler _selected;

        public event EventHandler ComboBoxSelectedElementChanged
        {
            add { _selected += value; }
            remove { _selected -= value; }
        }

        // свойство для установки и получения выбранного значения
        public string GetSelectedItem
        {
            get
            {
                if (comboBox.SelectedIndex == -1) { return null; }
                else { return comboBox.SelectedItem.ToString(); }
            }
            set
            {
                if (value != null)
                {
                    if (comboBox.Items.Contains(value))
                    {
                        comboBox.SelectedItem = value;
                    }
                }
            }
        }

        public ComboBoxControl()
        {
            InitializeComponent();
        }

        // метод заполнения списка строковыми значениями
        public void FillComboBox(List<string> strings)
        {
            foreach (string s in strings)
            {
                comboBox.Items.Add(s);
            }
        }

        // метод очистки списка
        public void DisposeComboBox()
        {
            comboBox.Items.Clear();
            comboBox.SelectedIndex = -1;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selected?.Invoke(sender, e);
        }
    }
}
