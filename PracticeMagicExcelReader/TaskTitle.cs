using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeMagicExcelReader
{
    class TaskTitle
    {
        private readonly string taskNo;

        private readonly string parentTaskNo;

        private readonly string taskName;

        public TaskTitle(string taskNo, string taskName)
        {
            this.taskNo = taskNo;
            this.taskName = taskName;
            this.parentTaskNo = SubstringParentNo(taskNo);
        }

        private string SubstringParentNo(string taskNo)
        {
            int lastDot = taskNo.LastIndexOf(".");

            // 最上位の親の場合はnullを返す。
            if (lastDot == -1)
            {
                return null;
            }

            return taskNo.Substring(0, lastDot);
        }

        public string GetTaskNo()
        {
            return this.taskNo;
        }

        public string GetParentTaskNo()
        {
            return this.parentTaskNo;
        }

        public string GetTaskName()
        {
            return this.taskName;
        }

        public string GetTaskNoAndName()
        {
            return this.taskNo + "：" + this.taskName;
        }

        public int CompareTo(TaskTitle taskTitle)
        {
            return this.taskNo.CompareTo(taskTitle.taskNo);
        }
    }
}
