using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentVariablesMaster
{
    public enum CaseFormVarEditorStart
    {
        editlistViewUserItem,
        editlistViewSystemItem,
        addlistViewUserItem,
        addlistViewSystemItem
    }

    public class GlobalData
    {
        // 这个类扮演着中转站的角色，用于储存（需要在各窗体之间传递的）值

        // 这个变量帮助Form2中的保存按钮确定应该执行什么操作
        public static CaseFormVarEditorStart whatCaseFormVarEditorStart;

        // 这个变量帮助Form1中的保存按钮确定有哪些变更需要保存
        public static bool listViewUserChanged;
        public static bool listViewSystemChanged;

    }
}
