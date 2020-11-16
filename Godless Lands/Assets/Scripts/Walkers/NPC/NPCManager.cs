
using RUCP;
using RUCP.Handler;
using RUCP.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NPCs
{
    public class NPCManager : MonoBehaviour, Manager
    {

        public NPCList npcList;
        private static Dictionary<int, NPC> npc;

        private void Awake()
        {
            HandlersStorage.RegisterHandler(Types.NpcCreate, NPCCreate);
            HandlersStorage.RegisterHandler(Types.NpcDelete, NPCDelet);
        }

        public void AllDestroy()
        {
            foreach (NPC monster in npc.Values)
                Destroy(monster.gameObject);
            npc.Clear();

        }


        private void Start()
        {
            npc = new Dictionary<int, NPC>();
        }

        private void NPCDelet(Packet nw)
        {
            int id = nw.ReadInt();
            if (npc.ContainsKey(id))
            {
                GameObject.Destroy(npc[id].gameObject);
                npc.Remove(id);
            }
        }

        private void NPCCreate(Packet nw)
        {
            int id = nw.ReadInt();
            int idSkin = nw.ReadInt();
            Vector3 postion = nw.ReadVector3();
            float rotation = nw.ReadFloat();

            if (npc.ContainsKey(id)) { print("error create NPC"); return; } //Если NPC с таким ид уже создан


            GameObject prefabNPC = npcList.GetPrefab(idSkin);
            if (prefabNPC == null) { print("Error NPC skin: " + idSkin); return; }

            GameObject _obj = Instantiate(prefabNPC, postion, Quaternion.Euler(0.0f, rotation, 0.0f));
            _obj.transform.SetParent(transform);

            NPC _npc = _obj.GetComponent<NPC>();
            _npc.StartNPC();
            _npc.ID = id;
            _npc.SetName(npcList.GetNPC(idSkin).name);


            npc.Add(_npc.ID, _npc);
        }

        public static NPC GetNPCs(int id)
        {
            if (npc.ContainsKey(id))
                return npc[id];
            else return null;
        }

        private void OnDestroy()
        {
            HandlersStorage.UnregisterHandler(Types.NpcCreate);
            HandlersStorage.UnregisterHandler(Types.NpcDelete);
        }


    }
}