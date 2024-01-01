using RUCP;
using RUCP.Handler;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Characters
{
    public class CharactersManager : MonoBehaviour, Manager
    {

        public GameObject characterPrefab;
        private static Dictionary<int, Character> characters; //Ид персонажа, персонаж
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            networkManager.RegisterHandler(Types.CharacterCreate, CharacterCreate);
            //networkManager.RegisterHandler(Types.CharacterMove, CharacterMove);
            networkManager.RegisterHandler(Types.CharacterDelete, CharacterDelete);
            networkManager.RegisterHandler(Types.CharacterAnim, CharacterAnim);
            networkManager.RegisterHandler(Types.GhostUpdateArmor, GhostUpdateArmor);
            networkManager.RegisterHandler(Types.CharacterRotation, CharacterRotation);
            networkManager.RegisterHandler(Types.CharacterCombatState, CharacterCombatState);
        }


        /* private void CharacterDead(NetworkWriter nw)
         {
             int login_id = nw.ReadInt();
             int corpse_id = nw.ReadInt();
             print("Transfer: " + login_id);
             if (characters.ContainsKey(login_id))
             {
                 CorpsesManager.AddUnconfirmedBode(corpse_id, characters[login_id].gameObject);
                 characters.Remove(login_id);
             }
             else { print("Error not found character"); }
         }*/

        public static GameObject FindChar(int id)
        {
            try
            {
                GameObject obj = characters[id].gameObject;
                characters.Remove(id);
                return obj;
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }

        }

        public void AllDestroy()
        {
            foreach (Character ghost in characters.Values)
                Destroy(ghost.gameObject);
            characters.Clear();

        }
        private void CharacterCombatState(Packet nw)
        {

            int id_login = nw.ReadInt();

            if (characters.ContainsKey(id_login))
            {
                characters[id_login].Armor.SetCombatstate(nw.ReadBool());
            }
        }


        private void CharacterMove(Packet nw)
        {
            int charID = nw.ReadInt();   

            if (characters.ContainsKey(charID))
            {
                short syncNumber = nw.ReadShort();
                if (!characters[charID].Controller.Sync(syncNumber)) return;

                Vector3 position = nw.ReadVector3();
                float rotation = nw.ReadFloat();
                bool endMove = nw.ReadBool();

                characters[charID].Controller.SyncPosition(position, endMove);

                characters[charID].Controller.SyncRotation(rotation);
            }
            else
            {
                print("Move character not found: " + charID);
            }
        }
        private void CharacterRotation(Packet nw)
        {
            int charID = nw.ReadInt();

            if (characters.ContainsKey(charID))
            {

                characters[charID].Controller.SyncRotation(nw.ReadFloat());

            }
            else
            {
                print("Rotation character not found: " + charID);
            }
        }

        private void GhostUpdateArmor(Packet nw)
        {
            int id_login = nw.ReadInt();

            if (characters.ContainsKey(id_login))
            {
                characters[id_login].Armor.UpdateArmor(nw);
            }
        }

        private void CharacterAnim(Packet nw)
        {
            int id_login = nw.ReadInt();
            //print("use anim");
            if (characters.ContainsKey(id_login))
            {
                int layer = nw.ReadByte();
                int anim = nw.ReadInt();
                //  print("anim: "+anim);
                switch (layer)
                {
                    case 1: //layer 1 = Проиграть анимацию умений с контролем времени
                        int milliseconds = nw.ReadInt();
                        characters[id_login].GetComponent<AnimationSkill>().UseAnimationSkill(anim, (milliseconds / 1000.0f));
                        break;
                    case 2://layer 2 = Проиграть анимацию состояния без контроля времени
                        characters[id_login].GetComponent<AnimationSkill>().UseAnimState(anim);
                        break;
                    case 3: //layer 3 = Проиграть анимацию состояния с контролем времени
                        int timeMilli = nw.ReadInt();
                        characters[id_login].GetComponent<AnimationSkill>().UseAnimState(anim, (timeMilli / 1000.0f));
                        break;
                }
            }
        }

        void Start()
        {
            characters = new Dictionary<int, Character>(100);


        }

        public static ITargetObject GetCharacter(int id)
        {
            if (characters.ContainsKey(id))
                return characters[id];
            else return null;
        }

        private void CharacterCreate(Packet packet)
        {
            int charID = packet.ReadInt();
            string char_name = packet.ReadString();

            print("Character create: " + char_name+ ": "+charID);

            if (!characters.ContainsKey(charID))
            {

                GameObject obj = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(transform);
                Character charComponent = obj.GetComponent<Character>();

                charComponent.Initialize(char_name, charID);
       

                charComponent.Controller.Initialize(packet);

                charComponent.Armor.Initialize(packet);

                charComponent.Armor.SetCombatstate(packet.ReadBool());

                characters.Add(charID, charComponent);
            }
            else
            {
                print(char_name + ": " + charID + " персонаж уже был загружен");
            }
        }

        private void CharacterDelete(Packet nw)
        {
            int charID = nw.ReadInt();
            print("Delet: " + charID);
            if (characters.ContainsKey(charID))
            {
                GameObject.Destroy(characters[charID].gameObject);
                characters.Remove(charID);
            }
            else { print("Error not found character " + charID); }
        }



        private void OnDestroy()
        {
            networkManager?.UnregisterHandler(Types.CharacterCreate);
            //networkManager?.UnregisterHandler(Types.CharacterMove);
            networkManager?.UnregisterHandler(Types.CharacterDelete);
            networkManager?.UnregisterHandler(Types.CharacterAnim);
            networkManager?.UnregisterHandler(Types.GhostUpdateArmor);
            networkManager?.UnregisterHandler(Types.CharacterRotation);
            networkManager?.UnregisterHandler(Types.CharacterCombatState);
        }


    }
}