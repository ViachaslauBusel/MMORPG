using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using Monsters;
using MonsterRedactor;

namespace Monsters
{
    public class MonstersManager : MonoBehaviour
    {

        public MonstersList monstersList;
        private static Dictionary<int, Monster> monsters;

        private void Awake()
        {
            RegisteredTypes.RegisterTypes(Types.MonsterCreate, MonsterCreate);
            RegisteredTypes.RegisterTypes(Types.MonsterDelete, MonsterDelete);
            RegisteredTypes.RegisterTypes(Types.MonsterAnim, MonsterAnim);
            RegisteredTypes.RegisterTypes(Types.MonsterMove, MonsterMove);
            RegisteredTypes.RegisterTypes(Types.MonsterEndMove, MonsterEndMove);
        }

        private void MonsterEndMove(NetworkWriter nw)
        {
           // print("Monster Endmove");
            int id = nw.ReadInt();

            if (monsters.ContainsKey(id))
            {
                Vector3 pos = nw.ReadVec3();
                byte indicator = nw.ReadByte();
                //   float rot = nw.ReadFloat();
                GhostMove ghost = monsters[id].controller();
                ghost.EndPosition(pos, indicator);

              //  ghost.NextRotation(Quaternion.LookRotation(pos - ghost.transform.position).eulerAngles.y);


            }
        }

        private void MonsterMove(NetworkWriter nw)
        {
        //    print("Monster move");
            int id = nw.ReadInt();

            if (monsters.ContainsKey(id))
            {
                Vector3 pos = nw.ReadVec3();
                byte indicator = nw.ReadByte();
                //   byte moveIndex = nw.ReadByte();
                //  float rot = nw.ReadFloat();
                GhostMove ghost = monsters[id].controller();
                ghost.NextPosition(pos, indicator);

                ghost.NextRotation(Quaternion.LookRotation(pos - ghost.transform.position).eulerAngles.y);


            }
        }

        private void MonsterAnim(NetworkWriter nw)
        {
            int id_login = nw.ReadInt();

            if (monsters.ContainsKey(id_login))
            {

                int anim = nw.ReadInt();
                //print("monster anim: " + anim);
                TargetObject target = null;
                if (nw.AvailableBytes > 0)
                {
                    int target_layer = nw.ReadInt();
                    int target_id = nw.ReadInt();

                    target = TargetView.FindTarget(target_layer, target_id);
                }
                monsters[id_login].SkillAnim(anim, target);

            }
        }

        private void Start()
        {
            monsters = new Dictionary<int, Monster>();
        }

        private void MonsterDelete(NetworkWriter nw)
        {
            int id = nw.ReadInt();
            if (monsters.ContainsKey(id))
            {
                GameObject.Destroy(monsters[id].gameObject);
                monsters.Remove(id);
            }
        }

        private void MonsterCreate(NetworkWriter nw)
        {
            int id = nw.ReadInt();
            int idSkin = nw.ReadInt();
            Vector3 postion = nw.ReadVec3();
            float rotation = nw.ReadFloat();
            byte indicator = nw.ReadByte();
            if (monsters.ContainsKey(id)) { print("error create monster"); return; } //Если монстр с таким ид уже создан


            GameObject prefabMonster = monstersList.GetPrefab(idSkin);
            if (prefabMonster == null) { print("Error monster skin: " + idSkin); return; }

            GameObject _obj = Instantiate(prefabMonster, postion, Quaternion.Euler(0.0f, rotation, 0.0f));
            _obj.transform.SetParent(transform);

            Monster _monster = _obj.GetComponent<Monster>();
            _monster.StartMonster();
            _monster.ID = id;
            _monster.SetName(monstersList.GetMonster(idSkin).name);

            GhostMove ghost = _monster.controller();
            ghost.SetStartPosition(postion, indicator, 0);

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
            RegisteredTypes.UnregisterTypes(Types.MonsterCreate);
            RegisteredTypes.UnregisterTypes(Types.MonsterDelete);
            RegisteredTypes.UnregisterTypes(Types.MonsterAnim);
            RegisteredTypes.UnregisterTypes(Types.MonsterMove);
            RegisteredTypes.UnregisterTypes(Types.MonsterEndMove);
        }
    }
}