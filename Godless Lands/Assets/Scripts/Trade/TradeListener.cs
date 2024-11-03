using Network.Core;
using Protocol;
using Protocol.MSG.Game.CombatMode;
using Protocol.MSG.Game.Trade;
using RUCP;
using System;
using Trade.UI;
using UI.ConfirmationDialog;
using UnityEngine;

namespace Trade
{
    internal class TradeListener : IDisposable
    {
        private NetworkManager _networkManager;
        private ConfirmationDialogController _confirmationDialog;
        private TradeWindow _tradeWindow;
        private TradeModel _tradeModel;

        public TradeListener(NetworkManager networkManager, ConfirmationDialogController confirmationDialog, TradeWindow tradeWindow, TradeModel tradeModel)
        {
            _networkManager = networkManager;
            _confirmationDialog = confirmationDialog;
            _tradeWindow = tradeWindow;
            _tradeModel = tradeModel;

            _networkManager.RegisterHandler(Opcode.MSG_TRADE_REQUEST, OnTradeRequest);
            _networkManager.RegisterHandler(Opcode.MSG_OPEN_TRADE_WINDOW, OnOpenTradeWindow);
            _networkManager.RegisterHandler(Opcode.MSG_STATE_TRADE_WINDOW, OnStateSyncWindow);
            _networkManager.RegisterHandler(Opcode.MSG_SYNC_TRADE_WINDOW, OnSyncTradeWindow);
            _networkManager.RegisterHandler(Opcode.MSG_CLOSE_TRADE_WINDOW, OnCloseTradeWindow);
        }
     
        private void OnStateSyncWindow(Packet packet)
        {
            packet.Read(out MSG_STATE_TRADE_WINDOW_SC state);
            _tradeWindow.SyncState(state.PlayerLock, state.PartnerLock);
        }

        private void OnSyncTradeWindow(Packet packet)
        {
            packet.Read(out MSG_SYNC_TRADE_WINDOW_SC syncTradeWindow);
            //Debug.Log($"Trade ID: {syncTradeWindow.TradeID}, IsOwner: {syncTradeWindow.IsOwner}, BagSize: {syncTradeWindow.BagSize}, Items: {syncTradeWindow.Items}");
            _tradeModel.Update(syncTradeWindow.TradeID, syncTradeWindow.IsOwner, syncTradeWindow.BagSize, syncTradeWindow.Items);
        }

        private void OnOpenTradeWindow(Packet packet)
        {
            packet.Read(out MSG_OPEN_TRADE_WINDOW_SC tradeWindow);

            _tradeWindow.Open(tradeWindow.ApponentName);
        }

        private void OnCloseTradeWindow(Packet packet)
        {
            _tradeWindow.Close();
        }

        private void OnTradeRequest(Packet packet)
        {
            packet.Read(out MSG_TRADE_REQUEST_SC request);

            _confirmationDialog.AddRequest($"Trade request from {request.RequesterName}, do you want to accept?", () =>
            {
                // Accept trade
                // Send response to the server
                var response = new MSG_TRADE_REQUEST_RESPONSE_CS
                {
                    Result = true
                };
                _networkManager.Client.Send(response);
            }, () =>
            {
                // Decline trade
                // Send response to the server
                var response = new MSG_TRADE_REQUEST_RESPONSE_CS
                {
                    Result = false
                };
                _networkManager.Client.Send(response);
            },
            // Wait time for canceling the trade
            request.Time / 1_000f
            );
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_TRADE_REQUEST);
            _networkManager.UnregisterHandler(Opcode.MSG_OPEN_TRADE_WINDOW);
            _networkManager.UnregisterHandler(Opcode.MSG_CLOSE_TRADE_WINDOW);
            _networkManager.UnregisterHandler(Opcode.MSG_SYNC_TRADE_WINDOW);
            _networkManager.UnregisterHandler(Opcode.MSG_STATE_TRADE_WINDOW);
        }
    }
}
