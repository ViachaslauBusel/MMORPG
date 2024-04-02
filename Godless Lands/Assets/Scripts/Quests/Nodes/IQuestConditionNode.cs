using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.Nodes
{
    internal interface IQuestConditionNode
    {
        
        bool CheckQuestCondition();
    }
}
