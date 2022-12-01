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
    public partial class FormClient : Form
    {
        private readonly ClientLogic clientLogic;
        private readonly PickupPointLogic pickupPointLogic;
        public int Id { set { id = value; } }
        private int? id;
        public FormClient(ClientLogic clientLogic, PickupPointLogic pickupPointLogic)
        {
            InitializeComponent();
            this.clientLogic = clientLogic;
            this.pickupPointLogic = pickupPointLogic;
            inputUserControl.MaxDate = new DateTime(2023, 9, 22, 23, 59, 59);
            inputUserControl.MinDate = new DateTime(2019, 9, 17, 23, 59, 59);
            inputUserControl.SelectItemProperty = DateTime.Now;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxBuy1.Text))
            {
                MessageBox.Show("Введите сумму заказа клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxBuy2.Text))
            {
                MessageBox.Show("Введите сумму заказа клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxBuy3.Text))
            {
                MessageBox.Show("Введите сумму заказа клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxBuy4.Text))
            {
                MessageBox.Show("Введите сумму заказа клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBoxControl.GetSelectedItem))
            {
                MessageBox.Show("Выберите пункт выдачи клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (inputUserControl.SelectItemProperty == null)
            {
                MessageBox.Show("Выберите последние 3 года!!!!! ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                clientLogic.CreateOrUpdate(new ClientBindingModel
                {
                    Id = id,
                    Name = textBoxFIO.Text,
                    Order1 = textBoxBuy1.Text,
                    Order2 = textBoxBuy2.Text,
                    Order3 = textBoxBuy3.Text,
                    Order4 = textBoxBuy4.Text,
                    PickupPoint = comboBoxControl.GetSelectedItem,
                    RegistrationDate = (DateTime)inputUserControl.SelectItemProperty
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var view = clientLogic.Read(new ClientBindingModel { Id = id })?[0];
            if (view != null)
            {
                try
                {
                    if (textBoxFIO.Text != view.Name || textBoxBuy1.Text != view.Order1 || textBoxBuy2.Text != view.Order2 || textBoxBuy3.Text != view.Order3 || textBoxBuy4.Text != view.Order4 || comboBoxControl.GetSelectedItem != view.PickupPoint)
                    {

                        if (MessageBox.Show("Остались не сохраненные данные", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            List<PickupPointViewModel> list = pickupPointLogic.Read(null);
            var listStr = new List<String>();
            foreach (var name in list)
            {
                listStr.Add(name.Name);
            }

            comboBoxControl.FillComboBox(listStr);
            if (id.HasValue)
            {
                try
                {
                    var view = clientLogic.Read(new ClientBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxFIO.Text = view.Name;
                        textBoxBuy1.Text = view.Order1;
                        textBoxBuy2.Text = view.Order2;
                        textBoxBuy3.Text = view.Order3;
                        textBoxBuy4.Text = view.Order4;
                        // textBoxFeedback.Text = view.ClientFeedback;
                        comboBoxControl.GetSelectedItem = view.PickupPoint;
                      //  textTypeControl.Number = view.AmountOfPurchases;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
