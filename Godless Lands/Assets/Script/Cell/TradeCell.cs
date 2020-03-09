using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class TradeCell : ItemCell
    {


        /// <summary>
        /// Предложить предмет на обмен
        /// </summary>
        public override void Use()
         {
            if (IsEmpty()) return;
            StartCoroutine(IEOffer());
         }

        private IEnumerator IEOffer()
        {
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.ItemTrade);
            nw.write(item.objectID);
            int input_count = -1;
            if (item.stack)
            {
                while (input_count < 0)
                {
                    yield return null;
                    input_count = SelectCount.Count;//Открыть окно для ввода количества предметов
                }
                if (input_count < 1) yield break;
                nw.write(input_count);
            }
            NetworkManager.Send(nw);

            /*if (item.stack)
            {
                item.count -= input_count;
                if (item.count < 1) PutItem(null, 0);
                else PutItem(item, item.count);
            }
            else
            {
                PutItem(null, 0);
            }*/ 
            //TODO sync trade
        }

        public void PutItemCell(ItemCell itemCell)
        {
            index = itemCell.GetIndex();
            PutItem(itemCell.GetItem());
        }

        public override void Put(Cell cell)
        {
        }
    }
}