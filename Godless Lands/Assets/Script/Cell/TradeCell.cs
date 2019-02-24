using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class TradeCell : ItemCell
    {


        public override void Use()
         {
            if (IsEmpty()) return;
            StartCoroutine(IEOffer());
         }

        private IEnumerator IEOffer()
        {
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.ItemTrade);
            nw.write(item.id);
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

            if (item.stack)
            {
                count -= input_count;
                if (count < 1) PutItem(null, 0);
            }
            else
            {
                PutItem(null, 0);
            }
        }

        public void PutItemCell(ItemCell itemCell)
        {
            index = itemCell.GetIndex();
            PutItem(itemCell.GetItem(), itemCell.GetCount());
        }

        public override void Put(Cell cell)
        {
        }
    }
}