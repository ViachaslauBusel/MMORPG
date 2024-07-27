using Protocol.Data.Items;
using UnityEngine;

namespace Items.Data
{
    public class ElixirItemData : ItemData
    {
        [Header("Elixir Item Data")]
        [SerializeField]
        private int _hpRestore;
        [SerializeField]
        private int _mpRestore;
        [SerializeField]
        private int _staminaRestore;

        public int HpRestore => _hpRestore;
        public int MpRestore => _mpRestore;
        public int StaminaRestore => _staminaRestore;

        public override ItemInfo ToServerData()
        {
            return new ElixirItemInfo(ID, IsStackable, Weight, HpRestore, MpRestore, StaminaRestore);
        }
    }
}
