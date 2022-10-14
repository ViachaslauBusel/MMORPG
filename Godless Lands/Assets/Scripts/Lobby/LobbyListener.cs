using Loader;
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
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
            networkManager.RegisterHandler(Types.MyCharacters, MyCharacters);
            networkManager.RegisterHandler(Types.SelectCharacter, SelectCharacter);
        }

        private void SelectCharacter(Packet nw)
        {
            if (nw.ReadByte() == 0) //Если выбор персонажа прошел успешно
            {
                Instantiate(gameLoader).GetComponent<GameLoader>().LoadGame();//Загрузка карты
            }
            else
            {
                ErrorCreator.ShowError(3);//Ошибка выбора персонажа
            }
        }

        void Start()
        {
            characters = GetComponentsInChildren<Character>();
         

        }


        /// <summary>
        /// Получение списка персонажей ид + имя
        /// </summary>
        /// <param name="nw"></param>
        private void MyCharacters(Packet nw)
        {
            print("Пакет получен");
            for (int i = 0; i < characters.Length; i++)
            {
                int ID = -1;
                string charName = "Создать персонажа";
                if (nw.AvailableBytesForReading >= 8)
                {
                    ID = nw.ReadInt();
                    charName = nw.ReadString();
                }
                characters[i].SetCharacter(ID, charName);
            }
            // order = false;
        }

       

        private void OnDestroy()
        {
            networkManager?.UnregisterHandler(Types.MyCharacters);
            networkManager?.UnregisterHandler(Types.SelectCharacter);
        }
    }
}