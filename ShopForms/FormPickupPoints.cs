using ShopBusinessLogic.BusinessLogic;
using ShopContracts.BindingModels;
using ShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopForms
{
    public partial class FormPickupPoints : Form
    {
        private readonly PickupPointLogic pickupPointLogic;
        List<PickupPointViewModel> list;

        public FormPickupPoints(PickupPointLogic pickupPointLogic)
        {
            InitializeComponent();
            this.pickupPointLogic = pickupPointLogic;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    list.Add(new PickupPointViewModel());
                    dataGridView1.DataSource = new List<PickupPointViewModel>(list);
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
                    return;
                }
                if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value != null)
                {
                    list.Add(new PickupPointViewModel());
                    dataGridView1.DataSource = new List<PickupPointViewModel>(list);
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
                    return;
                }
            }
            if (e.KeyData == Keys.Delete)
            {
                if (MessageBox.Show("Вы действительно хотите удалить?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    pickupPointLogic.Delete(new PickupPointBindingModel() { Id = (int)dataGridView1.CurrentRow.Cells[0].Value });
                    list = pickupPointLogic.Read(null);
                    dataGridView1.DataSource = new List<PickupPointViewModel>(list);
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (String.IsNullOrEmpty(((DataGridView)sender).CurrentCell.EditedFormattedValue.ToString()))
            {

                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    var listDBM = pickupPointLogic.Read(new PickupPointBindingModel() { Id = (int)dataGridView1.CurrentRow.Cells[0].Value });
                    dataGridView1.CurrentRow.Cells[1].Value = ((PickupPointViewModel)listDBM[0]).Name;
                }

            }
            else
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    pickupPointLogic.CreateOrUpdate(new PickupPointBindingModel()
                    {
                        Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
                        Name = (string)dataGridView1.CurrentRow.Cells[1].EditedFormattedValue
                    });
                }
                else
                {
                    pickupPointLogic.CreateOrUpdate(new PickupPointBindingModel()
                    {
                        Name = (string)dataGridView1.CurrentRow.Cells[1].EditedFormattedValue
                    });
                }
            }
            LoadData();
        }

        private void FormPickupPoints_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                list = pickupPointLogic.Read(null);
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
