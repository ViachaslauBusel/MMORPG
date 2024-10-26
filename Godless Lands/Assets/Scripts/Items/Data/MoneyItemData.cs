using Protocol.Data.Items.Types;

namespace Items.Data
{
    public class MoneyItemData : ItemData
    {
        public override ItemSData ToServerData()
        {
            return new MoneyItemSData(ID, IsStackable, Weight, Price);
        }
    }
}
