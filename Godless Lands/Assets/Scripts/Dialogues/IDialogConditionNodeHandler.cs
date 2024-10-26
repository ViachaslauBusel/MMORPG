using Dialogues.Data;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogues
{
    public interface IDialogConditionNodeHandler
    {
        Node CheckCondition(IConditionNode conditionNode);
    }
}
