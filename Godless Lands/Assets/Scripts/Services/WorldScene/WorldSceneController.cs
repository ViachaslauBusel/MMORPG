using Helpers;
using Loader;
using OpenWorld;
using Protocol;
using Protocol.Data;
using Protocol.MSG.Game;
using Protocol.MSG.Game.ToClient;
using Protocol.MSG.Game.ToServer;
using RUCP;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace WorldScene
{
    internal class WorldSceneController : MonoBehaviour
    {
        private NetworkManager m_networkManager;
        private MapLoader m_mapLoader;
        private SessionManagmentService m_sessionManagment;
        private LoadingScreenDisplay m_loadingScreenDisplay;

        [Inject]
        public void Construct(NetworkManager networkManager, MapLoader mapLoader, SessionManagmentService sessionManagment, LoadingScreenDisplay loadingScreenDisplay)
        {
            m_networkManager = networkManager;
            m_mapLoader = mapLoader;
            m_sessionManagment = sessionManagment;
            m_loadingScreenDisplay = loadingScreenDisplay;
        }

        private void Awake()
        {
            m_networkManager.RegisterHandler(Opcode.MSG_PREPARE_SCENE, PreapareScene);
        }

        private void Start()
        {
            // Send notification to server that we are ready to enter the world
            MSG_WORLD_ENTRANCE_CS entrance_request = new MSG_WORLD_ENTRANCE_CS();
            m_networkManager.Client.Send(entrance_request);
        }

        private async void PreapareScene(Packet packet)
        {
            m_loadingScreenDisplay.Show();
            packet.Read(out MSG_PREPARE_SCENE_SC prepareScene);

            m_sessionManagment.SetCharacterObjectID(prepareScene.GameObjectCharacterID);

            m_mapLoader.LoadMap(prepareScene.EntryPoint.ToUnity());

            while(m_mapLoader.isDone == false)
            {
                m_loadingScreenDisplay.Show(0.2f + 0.8f * m_mapLoader.progress);
                await Task.Yield();
            }

            MSG_SCENE_STATUS scene_status = new MSG_SCENE_STATUS();
            scene_status.Status = PlayerSceneStatus.ReadyForSync;
            m_networkManager.Client.Send(scene_status);

            m_loadingScreenDisplay.Hide();
        }

        private void OnDestroy()
        {
            m_networkManager.UnregisterHandler(Opcode.MSG_PREPARE_SCENE);
        }
    }
}
