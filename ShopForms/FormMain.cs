using ShopBusinessLogic.BusinessLogic;
using ShopContracts.BindingModels;
using ShopContracts.ViewModels;
using Unity;
using VisualComponentLibrary.Unvisual.HelperModels;
using VisualComponentLibrary.Unvisual;


using VisualComponentLibrary;
using Xceed.Document.NET;
//using ComponentsLibrary.NonVisualComponents;
//using ComponentsLibrary.NonVisualComponents.HelperModels;
//using ComponentsLibrary;


namespace ShopForms
{
    public partial class FormMain : Form
    {
        private readonly ClientLogic clientLogic;
        private readonly PickupPointLogic pickupPointLogic;
        public FormMain(ClientLogic clientLogic, PickupPointLogic pickupPointLogic)
        {
            InitializeComponent();
            this.clientLogic = clientLogic;
            this.pickupPointLogic = pickupPointLogic;
        }
        private void пунктВыдачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPickupPoints form = Program.Container.Resolve<FormPickupPoints>();
            form.ShowDialog();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateClient();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
     
            outputListControl.AddTemplate("Идентификатор {Id}, Имя {Name}, Пункт выдачи {PickupPoint}, Дата регистрации {RegistrationDate}", '{', '}');
            try
            {
                List<ClientViewModel> list = clientLogic.Read(null);
                outputListControl.ClearListBox();
                foreach (ClientViewModel client in list)
                {
                    outputListControl.AddObjectToListBox<ClientViewModel>(new ClientViewModel
                    {
                        Id = (int)client.Id,
                        Name = client.Name,
                        PickupPoint = client.PickupPoint,
                        RegistrationDate = client.RegistrationDate,
                        Order1 = client.Order1,
                        Order2 = client.Order2,
                        Order3 = client.Order3,
                        Order4 = client.Order4,
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateClient();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteClient();
        }

        private void CreateClient()
        {
            FormClient form = Program.Container.Resolve<FormClient>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void UpdateClient()
        {
            try
            {
                ClientOutput product = outputListControl.GetItem<ClientOutput>();
                FormClient form = Program.Container.Resolve<FormClient>();
                form.Id = product.Id;
                form.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void DeleteClient()
        {
            if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
             MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ClientOutput product = outputListControl.GetItem<ClientOutput>();
                    clientLogic.Delete(new ClientBindingModel { Id = product.Id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void wORDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDocumentWord();
        }
        private void CreateDocumentWord()
        {

            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var listPoints = pickupPointLogic.Read(null);
                        var listClients = clientLogic.Read(null);

                        List<string> axes = new List<string>();
                        List<TestData> data = new List<TestData>();
                       
                        foreach (var client in listClients)
                        {
                            if (!axes.Contains(client.RegistrationDate.Year.ToString()))
                            {
                                axes.Add(client.RegistrationDate.Year.ToString());
                            }                            
                        }
                        axes.Sort();



                        foreach(var point in listPoints)
                        {
                            int[] counter = new int[axes.Count];
                            int i = 0;

                            foreach (var axis in axes)
                            {
                                counter[i] = listClients.Count(x => (x.RegistrationDate.Year.ToString() == axis.ToString()) && (x.PickupPoint == point.Name));
                                i++;
                            }
                            data.Add(new TestData { name = point.Name, value = counter });
                        }
                       
                        List<Year_count> yearArray = new List<Year_count>();
                      

                        WordLinearDiagrammComponent word = new WordLinearDiagrammComponent();
                        word.Report(fileName: dialog.FileName, titleDoc: "Статистика пунктов выдачи", nameDiagram: "Линейная диаграмма", data, axes);
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var listClients = clientLogic.Read(null);
                    if (listClients.Count == 5)
                    {
                        List<string[,]> list = new List<string[,]>() {
                        new string[6,5]{ { "Id","1 order","2 order", "3order", "4 order"}, {"", "", "", "", ""  }, {"", "", "", "","" }, { "", "", "", "", "" }, { "", "", "", "", "" }, { "", "", "", "", "" } }
                        };
                        int row = 1;

                        foreach (var client in listClients)
                        {
                            list[0][row, 0] = client.Id.ToString();
                            list[0][row, 1] = client.Order1.ToString();
                            list[0][row, 2] = client.Order2.ToString();
                            list[0][row, 3] = client.Order3.ToString();
                            list[0][row, 4] = client.Order4.ToString();
                            row++;
                        }
                        pdf_tables tc = new pdf_tables();
                        // word.Report(fileName: dialog.FileName, titleDoc: "Статистика пунктов выдачи", nameDiagram: "Линейная диаграмма", data, axes);

                        tc.SaveTables(dialog.FileName, "Покупки", list);
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (listClients.Count == 6)
                    {
                        List<string[,]> list = new List<string[,]>()
                        {
                            new string[7, 5] { { "Id", "1 order", "2 order", "3order", "4 order" }, { "", "", "", "", "" }, { "", "", "", "", "" }, { "", "", "", "", "" }, { "", "", "", "", "" }, {"", "", "", "", ""}, { "", "", "", "", "" } }
                        };
                        int row = 1;

                        foreach (var client in listClients)
                        {
                            list[0][row, 0] = client.Id.ToString();
                            list[0][row, 1] = client.Order1.ToString();
                            list[0][row, 2] = client.Order2.ToString();
                            list[0][row, 3] = client.Order3.ToString();
                            list[0][row, 4] = client.Order4.ToString();
                            row++;
                        }
                        pdf_tables tc = new pdf_tables();
                        tc.SaveTables(dialog.FileName, "Покупки", list);
                    }
                    else if (listClients.Count == 4)
                    {
                        List<string[,]> list = new List<string[,]>()
                        {
                            new string[5, 5] { { "Id", "1 order", "2 order", "3order", "4 order" }, { "", "", "", "", "" }, { "", "", "", "", "" }, { "", "", "", "", "" },  { "", "", "", "", ""  } }
                        };
                        int row = 1;

                        foreach (var client in listClients)
                        {
                            list[0][row, 0] = client.Id.ToString();
                            list[0][row, 1] = client.Order1.ToString();
                            list[0][row, 2] = client.Order2.ToString();
                            list[0][row, 3] = client.Order3.ToString();
                            list[0][row, 4] = client.Order4.ToString();
                            row++;
                        }
                        pdf_tables tc = new pdf_tables();
                        tc.SaveTables(dialog.FileName, "Покупки", list);
                    }
                    else if (listClients.Count == 3)
                    {
                        List<string[,]> list = new List<string[,]>()
                        {
                            new string[4, 5] { { "Id", "1 order", "2 order", "3order", "4 order" }, { "", "", "", "", "" }, { "", "", "", "", "" }, { "", "", "", "", "" } }
                        };
                        int row = 1;

                        foreach (var client in listClients)
                        {
                            list[0][row, 0] = client.Id.ToString();
                            list[0][row, 1] = client.Order1.ToString();
                            list[0][row, 2] = client.Order2.ToString();
                            list[0][row, 3] = client.Order3.ToString();
                            list[0][row, 4] = client.Order4.ToString();
                            row++;
                        }
                        pdf_tables tc = new pdf_tables();
                        tc.SaveTables(dialog.FileName, "Покупки", list);
                    }
                    else if (listClients.Count == 2)
                    {
                        List<string[,]> list = new List<string[,]>()
                        {
                            new string[3, 5] { { "Id", "1 order", "2 order", "3order", "4 order" }, { "", "", "", "", "" }, { "", "", "", "", "" } }
                        };
                        int row = 1;

                        foreach (var client in listClients)
                        {
                            list[0][row, 0] = client.Id.ToString();
                            list[0][row, 1] = client.Order1.ToString();
                            list[0][row, 2] = client.Order2.ToString();
                            list[0][row, 3] = client.Order3.ToString();
                            list[0][row, 4] = client.Order4.ToString();
                            row++;
                        }
                        pdf_tables tc = new pdf_tables();
                        tc.SaveTables(dialog.FileName, "Покупки", list);
                    }


                    /*   foreach (string[,] key in list)
                       {
                           key[2, 2] = "as";
                       }*/
                    //  Console.WriteLine(list);
                    //   Console.Write(list);
                    //    list[0][0,0] = "a";
                    //  list[0].
                    //list.Add(list[1]);
                }
            }
        }

        private void eXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xls|*.xls" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var listClients = clientLogic.Read(null);
                    var rowMergeInfo = new Dictionary<string, int[]>();
                    rowMergeInfo.Add("Магазин", new int[] { 3, 4 });
                    var rowHeight = new int[] { 20, 70, 20, 20 };
                    var tableHeader = new string[] { "Ид", "Имя", "Дата регистрации", "Пункт выдачи" };
                    var clientList = new List<Client>();
                    foreach (var client in listClients)
                    {
                        clientList.Add(new Client((int)client.Id, client.Name, client.RegistrationDate, client.PickupPoint));
                    }

                    TableExcelComponent comp3 = new TableExcelComponent();
                    comp3.CreateExcelFileTable(dialog.FileName, "Книги", rowMergeInfo, rowHeight, tableHeader, clientList);
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
        }
    }
}