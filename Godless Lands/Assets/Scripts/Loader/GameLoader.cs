using Helpers;
using Network.Core;
using OpenWorld;
using OpenWorldLegacy;
using Player.Controller;
using Protocol;
using Protocol.MSG.Game;
using RUCP;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Loader
{
    public class GameLoader : MonoBehaviour
    {
        private static GameLoader m_instance;
        private MapLoader _mapLoader;
        public Image progressBar;
        private NetworkManager _networkManager;
        private ZenjectSceneLoader _sceneLoader;
        private PlayerMovementController _playerMovementController;

        [Inject]
        private void Construct(NetworkManager networkManager, ZenjectSceneLoader sceneLoader)
        {
            _networkManager = networkManager;
            _sceneLoader = sceneLoader;
            _networkManager.RegisterHandler(Opcode.MSG_WORLD_ENTRANCE, WorldEntrance);
        }

        private void Awake()
        {
            if (m_instance != null)
            {
                Debug.LogError("GameLoader: Two versions of an instance cannot run at the same time");
                Destroy(gameObject);
                return;
            }
            else { m_instance = this; }

         
            progressBar.fillAmount = 0.0f;
            DontDestroyOnLoad(gameObject);
            //Отключить физику
            Time.timeScale = 0.0f;
        }

        public void LoadPoint()
        {
            StartCoroutine(IELoadPoint());
        }

        private IEnumerator IELoadPoint()
        {
            CharacterInstanceMovementController player = GameObject.Find("Player").GetComponent<CharacterInstanceMovementController>();
            player.enabled = false;

            _mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();
            _mapLoader.DestroyMap();
            _mapLoader.LoadMap();

            while (!_mapLoader.isDone)
            {
                yield return null;
                progressBar.fillAmount = _mapLoader.progress;
            }

            //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.MapEntrance);
            //NetworkManager.Send(nw);

            Time.timeScale = 1.0f;//Включить физику
            player.enabled = true;
            Destroy(gameObject);

        }
        private IEnumerator IELoadGame_part1()
        {
            AsyncOperation asyncLoad = _sceneLoader.LoadSceneAsync("Map");

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
                progressBar.fillAmount = 0.2f * asyncLoad.progress;
            }


            _mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();

            while (!_mapLoader.Ready)
                yield return null;

            //Дождаться загрузки сцены с картой, и отправить запрос на сервер для того что бы начать получения данных
            MSG_WORLD_ENTRANCE_CS entrance_request = new MSG_WORLD_ENTRANCE_CS();
            _networkManager.Client.Send(entrance_request);
        }

        private void WorldEntrance(Packet packet)
        {
            packet.Read(out MSG_WORLD_ENTRANCE_SC response);
            StartCoroutine(IELoadGame_part2(response.EntryPoint.ToUnity()));
        }
        private IEnumerator IELoadGame_part2(Vector3 point)
        {
            Debug.Log($"Load map in point:{point}");
            _mapLoader.LoadMapInPoint(point);
            while (!_mapLoader.isDone)
            {
                yield return null;
                progressBar.fillAmount = 0.2f + (0.8f * _mapLoader.progress);
            }



           // GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            Time.timeScale = 1.0f;//Включить физику

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_WORLD_ENTRANCE);
            m_instance = null;
        }
    }
}