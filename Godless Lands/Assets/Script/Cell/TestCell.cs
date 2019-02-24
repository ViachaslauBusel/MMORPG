using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells {
    public class TestCell : MonoBehaviour
    {
        public ItemsList itemsList;
        public ItemCell itemCell_0;
        public ItemCell itemCell_1;
        public TradeCell itemCell_2;

        private void Start()
        {
            itemCell_0.PutItem(itemsList.GetItem(Random.Range(0, itemsList.Count)), 1);
            itemCell_1.PutItem(itemsList.GetItem(Random.Range(0, itemsList.Count)), 1);
            itemCell_2.PutItem(itemsList.GetItem(Random.Range(0, itemsList.Count)), 1);
        }
    }
}