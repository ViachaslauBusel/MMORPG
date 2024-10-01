using Protocol.Data.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.Export
{
    public interface IExportableNode
    {
        QuestSNode ToServerData();
    }
}
