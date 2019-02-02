using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class DeletCell : Cell
    {
        private new void Start()
        {
            base.Start();
            ShowIcon();
        }
        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if (cell.GetType() != typeof(ItemCell)) return;
            StartCoroutine(IEConfirmDelet(cell as ItemCell));
        }

        public IEnumerator IEConfirmDelet(ItemCell itemCell)
        {
            if (itemCell.IsEmpty()) yield break;
            Confirm confirm = Confirm.instant;
            confirm.SetTitle("Вы действительно хотите удалить предмет?");
            int answer = confirm.IsConfirm();
            while (answer < 0)
            {
                yield return 0;
                answer = confirm.IsConfirm();
            }
            if (answer == 0)//No
            {
                yield break;
            }


            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.DeletItem);
       //     print("Del index: " + index);
            nw.write(itemCell.GetIndex());
            nw.write(itemCell.GetItem().id);
            if (itemCell.GetItem().stack)
            {
                int count = -1;
                while (count < 0)
                {
                    yield return null;
                    count = SelectCount.Count;
                }
                if (count < 1) yield break;
                nw.write(count);
            }

            NetworkManager.Send(nw);
        }
    }
}