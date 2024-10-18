using Helpers;
using Loader;
using Network;
using Network.Core;
using Network.Replication;
using OpenWorld;
using OpenWorldLegacy;
using Player.Controller;
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
        private NetworkManager _networkManager;
        private MapLoader _mapLoader;
        private SessionManagmentService _sessionManagment;
        private LoadingScreenDisplay _loadingScreenDisplay;
        private ReplicationService _replicationService;
        private PlayerMovementController _playerMovementController;

        [Inject]
        public void Construct(NetworkManager networkManager, MapLoader mapLoader,
            SessionManagmentService sessionManagment, LoadingScreenDisplay loadingScreenDisplay,
            ReplicationService replicationService, PlayerMovementController playerMovementController)
        {
            _networkManager = networkManager;
            _mapLoader = mapLoader;
            _sessionManagment = sessionManagment;
            _loadingScreenDisplay = loadingScreenDisplay;
            _replicationService = replicationService;
            _playerMovementController = playerMovementController;
        }

        private void Awake()
        {
            _networkManager.RegisterHandler(Opcode.MSG_PREPARE_SCENE, PreapareScene);
        }

        private void Start()
        {
            // Send notification to server that we are ready to enter the world
            MSG_WORLD_ENTRANCE_CS entrance_request = new MSG_WORLD_ENTRANCE_CS();
            _networkManager.Client.Send(entrance_request);
        }
        /// <summary>
        /// Handling the request to prepare the scene
        /// Loading the map
        /// </summary>
        /// <param name="packet"></param>
        private async void PreapareScene(Packet packet)
        {
            _loadingScreenDisplay.Show();
            packet.Read(out MSG_PREPARE_SCENE_SC prepareScene);

            _replicationService.Clear();
            _mapLoader.DestroyMap();
            _mapLoader.SetTarget(null);

            _sessionManagment.SetCharacterObjectID(prepareScene.GameObjectCharacterID);
            _sessionManagment.SetCharacterID(prepareScene.CharacterID);
            _playerMovementController.Initialize(prepareScene.Segment);

            _mapLoader.LoadMapInPoint(prepareScene.EntryPoint.ToUnity());

            while(_mapLoader.isDone == false)
            {
                _loadingScreenDisplay.Show(0.2f + 0.8f * _mapLoader.progress);
                await Task.Yield();
            }

            MSG_SCENE_STATUS scene_status = new MSG_SCENE_STATUS();
            scene_status.Status = PlayerSceneStatus.ReadyForSync;
            _networkManager.Client.Send(scene_status);

            _loadingScreenDisplay.Hide();
        }

        private void OnDestroy()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_PREPARE_SCENE);
        }
    }
}
