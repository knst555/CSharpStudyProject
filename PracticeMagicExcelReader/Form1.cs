using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeMagicExcelReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<TaskTitle> list = CreateTaskTitleList();
            list.Sort((a, b) => a.CompareTo(b));

            foreach (TaskTitle task in list)
            {
                Console.WriteLine(task.GetTaskNo());
                Console.WriteLine("親タスク："  + task.GetParentTaskNo());  ;
            }

            sideTreeView.Nodes.Add(CreateTreeNode(list));
            sideTreeView.ExpandAll();

            // タブ
            AddTabControll(list[0].GetTaskName());
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Left)
            {
                Console.WriteLine("コントロールと←が押されたよ！");
            }
        }

        private List<TaskTitle> CreateTaskTitleList()
        {
            List<TaskTitle> taskTitleList = new List<TaskTitle>();

            taskTitleList.Add(new TaskTitle("1841.1", "子タスク"));
            taskTitleList.Add(new TaskTitle("1841.2", "子タスク"));
            taskTitleList.Add(new TaskTitle("1841.1.1", "孫タスク"));
            taskTitleList.Add(new TaskTitle("1841", "親タスク"));

            return taskTitleList;
        }

        private TreeNode CreateTreeNode(List<TaskTitle> taskTitleList)
        {
            taskTitleList.Sort((a, b) => a.CompareTo(b));

            Dictionary<string, TreeNode> treeNodeDictionary = new Dictionary<string, TreeNode>();

            TreeNode firstParentNode = new TreeNode();

            foreach (TaskTitle taskTitle in taskTitleList)
            {
                // 最上位の親の場合はDictionaryに追加のみ。
                if (taskTitle.GetParentTaskNo() == null)
                {
                    firstParentNode = new TreeNode(taskTitle.GetTaskNoAndName());
                    treeNodeDictionary.Add(taskTitle.GetTaskNo(), firstParentNode);
                    continue;
                }
                
                // 親持ち
                TreeNode parentNode = treeNodeDictionary[taskTitle.GetParentTaskNo()];
                TreeNode treeNode = new TreeNode(taskTitle.GetTaskNoAndName());
                parentNode.Nodes.Add(treeNode);

                treeNodeDictionary.Add(taskTitle.GetTaskNo(), treeNode);
            }

            return firstParentNode;
        }

        private void AddTabControll(string taskName)
        {
            tabControl1.TabPages.Clear();

            TabPage myTabPage = new TabPage(taskName);
            tabControl1.TabPages.Add(myTabPage);
            tabControl1.TabPages[0].Controls.Add(CreateDataGridView());

            TabPage myTabPage2 = new TabPage("tab2");
            tabControl1.TabPages.Add(myTabPage2);
        }

        private DataGridView CreateDataGridView()
        {
            DataTable table = new DataTable("Table");

            // カラム名の追加
            table.Columns.Add("教科");
            table.Columns.Add("点数", Type.GetType("System.Int32"));
            table.Columns.Add("氏名");
            table.Columns.Add("クラス名");

            // Rows.Addメソッドを使ってデータを追加
            table.Rows.Add("国語", 90, "田中　一郎", "A");
            table.Rows.Add("数学", 50, "鈴木　二郎", "A");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");
            table.Rows.Add("英語", 90, "佐藤　三郎", "B");

            DataGridView dataGridView = new DataGridView();
            dataGridView.DataSource = table;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dataGridView.Width = tabControl1.Width;
            dataGridView.Height = tabControl1.Height;
            dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            dataGridView.ReadOnly = true;
            dataGridView.Name = "logicDataGridView";
            return dataGridView;
        }

        private void DataGridView_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = tabControl1.SelectedTab.Controls[0] as DataGridView;
            MessageBox.Show(dataGridView.CurrentCell.Value as string);
        }

        private void sideTreeView_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            AddTabControll(e.Node.Text);
        }
    }
}
