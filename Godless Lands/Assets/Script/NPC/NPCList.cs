
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    public class NPCList : ScriptableObject
    {
        public List<NPCPrefab> npcList;

        public void Add(NPCPrefab _npc)
        {
            if (npcList == null)
            {
                npcList = new List<NPCPrefab>();
            }
            if (_npc.id == 0) _npc.id++;

            while (ConstainsKey(_npc.id)) _npc.id++;
            npcList.Add(_npc);
        }

        public GameObject GetPrefab(int id)
        {
            foreach (NPCPrefab _npc in npcList)
            {
                if (_npc.id == id) return _npc.prefab;
            }
            return null;
        }

        public NPCPrefab GetNPC(int id)
        {
            foreach (NPCPrefab _npc in npcList)
            {
                if (_npc.id == id) return _npc;
            }
            return null;
        }

        private bool ConstainsKey(int id)
        {
            foreach (NPCPrefab _npc in npcList)
            {
                if (_npc.id == id) return true;
            }
            return false;
        }
        public void RemoveItem(NPCPrefab _npc)
        {
            if (_npc == null) return;
            npcList.Remove(_npc);
        }
        public int Count
        {
            get { if (npcList == null) return 0; return npcList.Count; }
        }
        public NPCPrefab this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= npcList.Count) return null;
                return npcList[index];
            }
        }

        public List<NPCPrefab> GetList()
        {
            return npcList;
        }

        public string GetName(int nPC)
        {
            foreach (NPCPrefab _npc in npcList)
            {
                if (_npc.id == nPC) return _npc.name;
            }
            return "not found";
        }
    }
}
