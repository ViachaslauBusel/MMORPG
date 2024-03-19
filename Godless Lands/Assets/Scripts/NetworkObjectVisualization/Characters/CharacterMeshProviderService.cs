using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NetworkObjectVisualization.Characters
{
    public class CharacterMeshProviderService
    {
        private ItemsFactory _itemsFactory;

        public CharacterMeshProviderService(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        public GameObject GetMesh(int id)
        {
           return _itemsFactory.GetItemData(id)?.prefab;
        }
    }
}
