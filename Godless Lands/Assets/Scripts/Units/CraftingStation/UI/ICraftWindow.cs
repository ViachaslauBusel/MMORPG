using Protocol.Data.Units.CraftingStation;

namespace CraftingStations.UI
{
    public interface ICraftWindow
    {
        CraftingStationType StationType { get; }
        void Open(bool isReadyForWork);
        void Close();
        void SetStatus(bool isReadyForWork);
    }
}
