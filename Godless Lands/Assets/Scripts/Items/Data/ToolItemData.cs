using Protocol.Data.Items;
using Protocol.Data.Items.Types;
using UnityEngine;

namespace Items.Data
{
    public class ToolItemData : ItemData
    {
        [SerializeField]

        private ToolType _toolType;

        public override ItemSData ToServerData()
        {
            return new ToolItemSData(ID, IsStackable, Weight, Price, _toolType);
        }
    }
}
