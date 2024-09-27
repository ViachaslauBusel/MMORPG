using Protocol.Data.Units.CraftingStation;

namespace Workbench.UI
{
    public interface IWorkbenchWindow
    {
        CraftingStationType StationType { get; }
        void Open(bool isReadyForWork);
        void Close();
        void SetStatus(bool isReadyForWork);
    }
}
