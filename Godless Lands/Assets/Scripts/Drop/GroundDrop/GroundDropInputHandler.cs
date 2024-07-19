using Helpers;
using Items;
using Network.Core;
using Protocol.MSG.Game.Drop;
using UI.ConfirmationDialog;
using UnityEngine;

namespace Drop.GroundDrop
{
    internal class GroundDropInputHandler
    {
        private ConfirmationDialogController _confirmationDialog;
        private SelectQuantityWindow _selectQuantityWindow;
        private NetworkManager _networkManager;


        public GroundDropInputHandler(ConfirmationDialogController confirmationDialog, SelectQuantityWindow selectQuantityWindow, NetworkManager networkManager)
        {
            _confirmationDialog = confirmationDialog;
            _selectQuantityWindow = selectQuantityWindow;
            _networkManager = networkManager;
        }

        internal void DropItem(Item item, Vector3 point)
        {
            if(item == null)
            {
                return;
            }

            if (item.Data.IsStackable)
            {
                _selectQuantityWindow.Subscribe(
                                       "How many pieces to move?",
                                                          (count) =>
                                                          {
                                                             DropItemRequest(item, count, point);
                                                          },
                                                          () => { }
                                                                         );
            }
            else
            {
                DropItemRequest(item, 1, point);
            }
        }

        private void DropItemRequest(Item item, int count, Vector3 point)
        {
            _confirmationDialog.AddRequest(
                              "Do you really want to drop the item?",
                                             () =>
                                             {
                                                 MSG_DROP_ITEM_CS msg = new MSG_DROP_ITEM_CS();
                                                 msg.ItemUID = item.UniqueID;
                                                 msg.Count = count;
                                                 msg.Position = point.ToNumeric();
                                                 _networkManager.Client.Send(msg);
                                             },
                                             () => { }
                                             );
        }
    }
}
