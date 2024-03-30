using Protocol.Data.Workbenches;
using Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench.UI
{
    public interface IWorkbenchWindow
    {
        WorkbenchType WorkbenchType { get; }
        void Open(bool isReadyForWork);
        void Close();
        void SetStatus(bool isReadyForWork);
        void SelectRecipe(Recipe recipe);
    }
}
