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
        private MapLoader mapLoader;
        public Image progressBar;
        private NetworkManager m_networkManager;
        private ZenjectSceneLoader m_sceneLoader;

        [Inject]
        private void Construct(NetworkManager networkManager, ZenjectSceneLoader sceneLoader)
        {
            m_networkManager = networkManager;
            m_sceneLoader = sceneLoader;
            networkManager.RegisterHandler(Network.Core.Types.MapEntrance, MapEntrance);
            m_networkManager.RegisterHandler(Opcode.MSG_WORLD_ENTRANCE, WorldEntrance);
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

       

        private void MapEntrance(Packet packet)
        {
            mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();
           //TODO mapLoader.LoadMap(packet.ReadVector3());
        }

        public void LoadGame()
        {
            Debug.Log($"LOAD GAME:{Time.frameCount}");
            StartCoroutine(IELoadGame_part1());
        }
        //private void Update()
        //{
        //    Debug.Log($"Update:{Time.frameCount}");
        //}
        public void LoadPoint()
        {
            StartCoroutine(IELoadPoint());
        }

        private IEnumerator IELoadPoint()
        {
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.enabled = false;

            mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();
            mapLoader.DestroyMap();
            mapLoader.LoadMap();

            while (!mapLoader.isDone)
            {
                yield return null;
                progressBar.fillAmount = mapLoader.progress;
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
            AsyncOperation asyncLoad = m_sceneLoader.LoadSceneAsync("Map");

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
                progressBar.fillAmount = 0.2f * asyncLoad.progress;
            }


            mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();

            while (!mapLoader.Ready)
                yield return null;

            //Дождаться загрузки сцены с картой, и отправить запрос на сервер для того что бы начать получения данных
            MSG_WORLD_ENTRANCE_CS entrance_request = new MSG_WORLD_ENTRANCE_CS();
            m_networkManager.Client.Send(entrance_request);
        }

        private void WorldEntrance(Packet packet)
        {
            packet.Read(out MSG_WORLD_ENTRANCE_SC response);
            StartCoroutine(IELoadGame_part2(response.EntryPoint.ToUnity()));
        }
        private IEnumerator IELoadGame_part2(Vector3 point)
        {
            Debug.Log($"Load map in point:{point}");
            mapLoader.LoadMapInPoint(point);
            while (!mapLoader.isDone)
            {
                yield return null;
                progressBar.fillAmount = 0.2f + (0.8f * mapLoader.progress);
            }



           // GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            Time.timeScale = 1.0f;//Включить физику

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            m_networkManager?.UnregisterHandler(Network.Core.Types.MapEntrance);
            m_instance = null;
        }
    }
}