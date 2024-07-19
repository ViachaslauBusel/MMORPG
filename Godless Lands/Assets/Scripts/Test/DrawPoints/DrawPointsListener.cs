using Network.Core;
using Protocol;
using Protocol.MSG.Game.Test;
using RUCP;
using System;

namespace Test.DrawPoints
{
    internal class DrawPointsListener : IDisposable
    {
        private NetworkManager _networkManager;
        private DrawPointsModel _drawPointsModel;

        public DrawPointsListener(NetworkManager networkManager, DrawPointsModel drawPointsModel)
        {
            _networkManager = networkManager;
            _drawPointsModel = drawPointsModel;

            _networkManager.RegisterHandler(Opcode.MSG_DRAW_POINTS, OnDrawPoints);
        }

        private void OnDrawPoints(Packet packet)
        {
            packet.Read(out MSG_DRAW_POINTS msg);
            _drawPointsModel.UpdatePoints(msg.Points);
        }

        public void Dispose()
        {
           _networkManager.UnregisterHandler(Opcode.MSG_DRAW_POINTS);
        }
    }
}
