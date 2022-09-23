using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualComponentLibrary
{
    public partial class ChoiceUserControl : UserControl
    {
        private int checkedItem = 0;
        private event EventHandler _event;

        public ChoiceUserControl()
        {
            InitializeComponent();
            checkedListBox.SelectedIndexChanged += (sender, e) => _event?.Invoke(sender, e);
        }

        public event EventHandler SelectedIndexChanged
        {
            add { _event += value;}
            remove { _event -= value; }
        }

        public string SelectItemProperty
        {
            get
            {
                if (checkedListBox.SelectedItem == null)
                {
                    return string.Empty;
                }

                return checkedListBox.SelectedItem.ToString();
            }
            set
            {
                checkedListBox.SelectedItem = value;
            }
        }

        public void AddItems(List<string> items)
        {
            foreach (string item in items)
            {
                checkedListBox.Items.Add(item);
            }
        }

        public void Clear()
        {
            checkedListBox.Items.Clear();
        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox.SetItemChecked(checkedItem, false);
            checkedListBox.SetItemChecked(checkedListBox.SelectedIndex, true);

            MessageBox.Show("The weather is " + checkedListBox.SelectedItem.ToString() + " today!", "checkedListBox", MessageBoxButtons.OK);
            checkedItem = checkedListBox.SelectedIndex;
        }
    }
}
