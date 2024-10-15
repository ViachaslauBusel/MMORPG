using Network.Core;
using Protocol.MSG.Game.Workbench;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CraftingStations
{
    public class CraftingStationController
    {
        private NetworkManager _networkManager;

        public CraftingStationController(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void CloseWindow()
        {
            MSG_WORKBENCH_CLOSE_WINDOW msg = new MSG_WORKBENCH_CLOSE_WINDOW();
            _networkManager.Client.Send(msg);
        }

        public void CreateItem(int recipeId, List<long> components, List<long> fuel)
        {
            MSG_WORKBENCH_CREATE_ITEM msg = new MSG_WORKBENCH_CREATE_ITEM();
            msg.RecipeID = recipeId;
            msg.Components = components;
            msg.Fuel = fuel;
            _networkManager.Client.Send(msg);
        }
    }
}
