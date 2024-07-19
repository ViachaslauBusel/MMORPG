using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class ItemDrop : ItemBundle
    {
        [SerializeField, Range(0, 100)]
        private float _chance;

        public float Chance => _chance;

        public ItemDrop(int id, int count, float chance) : base(id, count)
        {
            _chance = chance;
        }
    }
}
