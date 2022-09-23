using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace VisualComponentLibrary
{
    public partial class OutputUserControl : UserControl
    {
        private event EventHandler _event;
        public Type typeOflist;
        public FieldInfo[] nodesName;

        public OutputUserControl()
        {
            InitializeComponent();
            treeView.AfterSelect += (sender, e) => _event?.Invoke(sender, e);
        }

        public event EventHandler SelectedDateChanged
        {
            add { _event += value; }
            remove { _event -= value; }
        }

        public int SelectItemProperty
        {
            get
            {
                if (treeView.SelectedNode != null) return treeView.SelectedNode.Index;
                else return -1;
            }
            set
            {
                if (value > -1 && value < treeView.Nodes.Count)
                {
                    treeView.SelectedNode = treeView.Nodes[value];
                    return;
                }
            }
        }

        public void CreateTree<T>(List<T> list) where T : class, new ()
        {
            typeOflist = list[0].GetType();
            nodesName = typeOflist.GetFields();
            int countLevel = nodesName.Length;
            
            foreach (var node in list)
            {
                var nodeLevels = treeView.Nodes;
                int currentLevel = 1;
                for (int i = 0; i < countLevel; i++)
                {
                    var fieldValue = nodesName[i].GetValue(node).ToString();

                    if (!nodeLevels.ContainsKey(fieldValue))
                    {
                        if (currentLevel == nodesName.Length)
                        {
                            nodeLevels.Add(fieldValue);
                        }
                        else
                            nodeLevels.Add(fieldValue, fieldValue);
                    }
                    if (currentLevel != nodesName.Length)
                        nodeLevels = nodeLevels.Find(fieldValue, false)[0].Nodes;

                    currentLevel++;
                }
            }
        }

        public T GetSelectedNode<T>() where T : class, new()
        {
            var curNode = treeView.SelectedNode;
            if (curNode.Nodes.Count > 0)
            {
                MessageBox.Show("you have not selected the leaf element of the tree", "treeView", MessageBoxButtons.OK);
                return null;
            }

            var listWithFields = new List<string>();
            while (curNode != null)
            {
                listWithFields.Add(curNode.Text);
                curNode = curNode.Parent;
            }
            listWithFields.Reverse();

            var elem = new T();
            var count = elem.GetType().GetProperties().Length;
            for (int i = 0; i < nodesName.Length; ++i)
            {
                var finfo = elem.GetType().GetField(nodesName[i].Name);
                if (finfo != null)
                    finfo.SetValue(elem, Convert.ChangeType(listWithFields[i], finfo.FieldType));
            }
            return elem;
        }
        public void Clear()
        {
            treeView.Nodes.Clear();
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MessageBox.Show("Congrats! You selected the element");
        }
    }
}
