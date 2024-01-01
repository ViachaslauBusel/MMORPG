using Loader;
using Protocol;
using Protocol.MSG.Game;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lobby
{
    public class LobbyListener : MonoBehaviour
    {

        public GameObject gameLoader;
        public Text text_info;
        //  private bool order = false;
        private Character[] characters;
        private NetworkManager m_networkManager;
        private DiContainer m_diContainer;
        private SceneLoader m_sceneLoader;

        [Inject]
        private void Construct(NetworkManager networkManager, DiContainer diContainer, SceneLoader sceneLoader)
        {
            m_networkManager = networkManager;
            m_diContainer = diContainer;
            m_sceneLoader = sceneLoader;
        }
        void Awake()
        {
            characters = GetComponentsInChildren<Character>();

            m_networkManager.RegisterHandler(Opcode.MSG_CHARACTERS_LIST, GetMyCharacters);
            m_networkManager.RegisterHandler(Opcode.MSG_SELECT_CHARACTER, SelectCharacter);
        }

        private void SelectCharacter(Packet packet)
        {
            packet.Read(out MSG_SELECT_CHARACTER_SC response);

            if (response.InformationCode == Protocol.Data.LoginInformationCode.AuthorizationSuccessful) //Если выбор персонажа прошел успешно
            {
                //m_sessionManagment.SetCharacterObjectID(response.CharacterObjectID);
          
                m_sceneLoader.LoadScene("Map", 0.2f);
                //m_diContainer.InstantiatePrefab(gameLoader).GetComponent<GameLoader>().LoadGame();//Вызов скрипта который выполняет вход в мир
            }
            else
            {
                ErrorCreator.ShowError(3);//Ошибка выбора персонажа
            }
        }


        /// <summary>
        /// Получение списка персонажей ид + имя
        /// </summary>
        /// <param name="packet"></param>
        private void GetMyCharacters(Packet packet)
        {
            packet.Read(out MSG_CHARACTERS_LIST_SC response);
            for (int i = 0; i < characters.Length; i++)
            {
                int ID = -1;
                string charName = "Создать персонажа";
                if (i < response.CharacterDatas.Length)
                {
                    ID = response.CharacterDatas[i].CharacterID;
                    charName = response.CharacterDatas[i].CharacterName;
                }
                characters[i].SetCharacter(ID, charName);
            }
            // order = false;
        }

       

        private void OnDestroy()
        {
            m_networkManager?.UnregisterHandler(Opcode.MSG_CHARACTERS_LIST);
            m_networkManager?.UnregisterHandler(Opcode.MSG_SELECT_CHARACTER);
        }
    }
}