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
        }

        private void sideTreeView_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            AddTabControll(e.Node.Text);
        }
    }
}
