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
    public partial class InputUserControl : UserControl
    {
        public DateTime? MaxDate { get; set; }
        public DateTime? MinDate { get; set; }
        private DateTime TempDate { get; set; }
        private event EventHandler _event;

        public InputUserControl()
        {
            InitializeComponent();
            dateTimePicker.ValueChanged += (sender, e) => _event?.Invoke(sender, e);
        }

        public event EventHandler SelectedDateChanged
        {
            add { _event += value; }
            remove { _event -= value; }
        }

        public DateTime? SelectItemProperty
        {
            get 
            {
                if (!MinDate.HasValue || !MaxDate.HasValue || dateTimePicker.Value < MinDate || dateTimePicker.Value > MaxDate)
                {
                    labelErorr.Text = "You are out of range!";
                    return null;
                }
                labelErorr.Text = "";
                return dateTimePicker.Value;
            }
            set 
            {
                if (!MinDate.HasValue || !MaxDate.HasValue || value < MinDate || value > MaxDate)
                {
                    labelErorr.Text = "You are out of range!";
                    return;
                }
                labelErorr.Text = "";
                dateTimePicker.Value = value.Value;
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            SelectItemProperty = dateTimePicker.Value;
            if (TempDate != SelectItemProperty && SelectItemProperty != null)
            {
                MessageBox.Show("Today " + SelectItemProperty, "dateTimePicker", MessageBoxButtons.OK);
                TempDate = SelectItemProperty.Value;
            }
        }
    }
}
