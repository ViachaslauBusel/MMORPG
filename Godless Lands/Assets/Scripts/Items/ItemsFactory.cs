using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Items
{
    public class ItemsFactory : MonoBehaviour
    {
        [SerializeField]
        private ItemsContainer _itemsContainer;

        internal Item CreateItem(ItemNetworkData itemData)
        {
            Item item = CreateItem(itemData.ItemID);
            if (item == null) return null;
            item.objectID = itemData.UniqueID;
            item.count = itemData.Count;
            return item;
        }

        internal Item CreateItem(int id)
        {
           Item item = _itemsContainer.GetItem(id);
            if (item == null)
            {
                Debug.LogError("Item with id " + id + " not found");
                return null;
            }
            return item.Clone();
        }
    }
}
