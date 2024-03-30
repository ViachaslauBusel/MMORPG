using Protocol.MSG.Game.Workbench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Workbench
{
    public class WorkbenchInputHandler : MonoBehaviour
    {
        private NetworkManager _networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
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
