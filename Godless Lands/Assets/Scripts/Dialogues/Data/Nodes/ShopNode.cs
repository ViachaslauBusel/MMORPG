using NodeEditor;
using NodeEditor.Attributes;
using NodeEditor.Data;
using Trading.Shop.Data;
using UnityEngine;

namespace Dialogues.Data.Nodes
{
    [NodeGroup(group: "Dialogues"), NodeDisplayStyle(NodeStyle.Style_2)]
    public class ShopNode : Node
    {
        [SerializeField]
        private TradingShopData _shopData;

        public TradingShopData ShopData => _shopData;
    }
}
