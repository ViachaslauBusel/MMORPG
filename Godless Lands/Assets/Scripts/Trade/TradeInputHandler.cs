﻿using Items;
using Network.Core;
using Protocol.MSG.Game.Trade;
using Target;

namespace Trade
{
    internal class TradeInputHandler
    {
        private NetworkManager _networkManager;
        private TargetObjectProvider _targetObjectProvider;


        public TradeInputHandler(NetworkManager networkManager, TargetObjectProvider targetObjectProvider)
        {
            _networkManager = networkManager;
            _targetObjectProvider = targetObjectProvider;
        }

        private void SendRequest(int targetPlayerId)
        {
            MSG_TRADE_REQUEST_CS msg = new MSG_TRADE_REQUEST_CS();
            msg.TargetID = targetPlayerId;
            _networkManager.Client.Send(msg);
        }

        internal void RequestTrade()
        {
            if (_targetObjectProvider.TargetObjectID != 0)
            {
                SendRequest(_targetObjectProvider.TargetObjectID);
            }
        }

        internal void AddTradeItem(Item item, int index)
        {
            MSG_MOVE_ITEM_TO_TRADE_CS msg = new MSG_MOVE_ITEM_TO_TRADE_CS();
            msg.ItemUID = item.UniqueID;
            msg.Count = (short)item.Count;
            msg.ToSlot = (byte)index;
            _networkManager.Client.Send(msg);
        }

        public void AcceptTrade()
        {
            MSG_CONFIRM_TRADE msg = new MSG_CONFIRM_TRADE();
            _networkManager.Client.Send(msg);
        }

        internal void CancelTrade()
        {
            MSG_TRADE_CONTROL_COMMAND_CS msg = new MSG_TRADE_CONTROL_COMMAND_CS();
            msg.Command = TradeControlCommand.Cancel;
            _networkManager.Client.Send(msg);
        }
    }
}
