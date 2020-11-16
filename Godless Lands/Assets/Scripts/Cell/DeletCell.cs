using RUCP;
using RUCP.Network;
using RUCP.Packets;
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
            icon.enabled = true;
        }
        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if (cell.GetType() != typeof(ItemCell)) return;
            ItemCell itemCell = cell as ItemCell;

            SelectQuantity.Instance.Subscribe(
            "Сколько штук переместить?",
            (count) =>
            {
                             Confirm.Instance.Subscribe(
                             "Вы действительно хотите удалить предмет?",
                             () =>
                             {
                             Packet nw = new Packet(Channel.Reliable);
                             nw.WriteType(Types.DeletItem);
                             nw.WriteInt(itemCell.GetObjectID());
                             nw.WriteInt(itemCell.GetItem().id);
                             nw.WriteInt(count);
                             NetworkManager.Send(nw);
                             },
                             () => { } );
            },
            () => { }
            );

            
        }

    }
}