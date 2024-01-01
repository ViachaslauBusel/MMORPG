using MonsterRedactor;
using RUCP;
using RUCP.Handler;
using System.Collections.Generic;
using UnityEngine;
using Walkers;
using Zenject;

namespace Monsters
{
    public class MonstersManager : MonoBehaviour, Manager
    {

        public MonstersList monstersList;
        private static Dictionary<int, Monster> monsters;
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            networkManager.RegisterHandler(Types.MonsterCreate, MonsterCreate);
            networkManager.RegisterHandler(Types.MonsterDelete, MonsterDelete);
            networkManager.RegisterHandler(Types.MonsterAnim, MonsterAnim);
            networkManager.RegisterHandler(Types.MonsterMove, MonsterMove);
        }


        public void AllDestroy()
        {
            foreach (Monster monster in monsters.Values)
                Destroy(monster.gameObject);
            monsters.Clear();
    
        }



        private void MonsterMove(Packet nw)
        {
        //    print("Monster move");
            int id = nw.ReadInt();

            if (monsters.ContainsKey(id))
            {
                Vector3 pos = nw.ReadVector3();
                int indicator = nw.ReadByte();
                //   byte moveIndex = nw.ReadByte();
                //  float rot = nw.ReadFloat();
                MovementController ghost = monsters[id].controller();
               // ghost.SyncPosition(pos, indicator);

                ghost.SyncRotation(Quaternion.LookRotation(pos - ghost.transform.position).eulerAngles.y);


            }
        }

        private void MonsterAnim(Packet nw)
        {
            int id_login = nw.ReadInt();

            if (monsters.ContainsKey(id_login))
            {

                int anim = nw.ReadInt();
                //print("monster anim: " + anim);
                ITargetObject target = null;
                if (nw.AvailableBytesForReading > 0)
                {
                    int target_layer = nw.ReadInt();
                    int target_id = nw.ReadInt();

                   // target = TargetView.FindTarget(target_layer, target_id);
                }
                monsters[id_login].SkillAnim(anim, target);

            }
        }

        private void Start()
        {
            monsters = new Dictionary<int, Monster>();
        }

        private void MonsterDelete(Packet nw)
        {
            int id = nw.ReadInt();
            if (monsters.ContainsKey(id))
            {
                GameObject.Destroy(monsters[id].gameObject);
                monsters.Remove(id);
            }
        }

        private void MonsterCreate(Packet nw)
        {
            int id = nw.ReadInt();
            int idSkin = nw.ReadInt();
            Vector3 postion = nw.ReadVector3();
            float rotation = nw.ReadFloat();
            int indicator = nw.ReadByte();
            if (monsters.ContainsKey(id)) { print("error create monster"); return; } //Если монстр с таким ид уже создан


            GameObject prefabMonster = monstersList.GetPrefab(idSkin);
            if (prefabMonster == null) { print("Error monster skin: " + idSkin); return; }

            GameObject _obj = Instantiate(prefabMonster, postion, Quaternion.Euler(0.0f, rotation, 0.0f));
            _obj.transform.SetParent(transform);

            Monster _monster = _obj.GetComponent<Monster>();
            _monster.StartMonster();
            _monster.ID = id;
            _monster.SetName(monstersList.GetMonster(idSkin).name);

            MovementController ghost = _monster.controller();
        //    ghost.SetStartPosition(postion, indicator, 0);

            monsters.Add(_monster.ID, _monster);
        }

        public static Monster GetMonster(int id)
        {
            if (monsters.ContainsKey(id))
                return monsters[id];
            else return null;
        }

        private void OnDestroy()
        {
            networkManager?.UnregisterHandler(Types.MonsterCreate);
            networkManager?.UnregisterHandler(Types.MonsterDelete);
            networkManager?.UnregisterHandler(Types.MonsterAnim);
            networkManager?.UnregisterHandler(Types.MonsterMove);

        }


    }
}