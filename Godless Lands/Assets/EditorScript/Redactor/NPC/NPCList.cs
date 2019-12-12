using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCRedactor
{
    public class NPCList : ScriptableObject
    {
        public List<NPC> npcList;

        public void Add(NPC _npc)
        {
            if (npcList == null)
            {
                npcList = new List<NPC>();
            }
            if (_npc.id == 0) _npc.id++;

            while (ConstainsKey(_npc.id)) _npc.id++;
            npcList.Add(_npc);
        }

        public GameObject GetPrefab(int id)
        {
            foreach (NPC _npc in npcList)
            {
                if (_npc.id == id) return _npc.prefab;
            }
            return null;
        }

        public NPC GetNPC(int id)
        {
            foreach (NPC _npc in npcList)
            {
                if (_npc.id == id) return _npc;
            }
            return null;
        }

        private bool ConstainsKey(int id)
        {
            foreach (NPC _npc in npcList)
            {
                if (_npc.id == id) return true;
            }
            return false;
        }
        public void RemoveItem(NPC _npc)
        {
            if (_npc == null) return;
            npcList.Remove(_npc);
        }
        public int Count
        {
            get { if (npcList == null) return 0; return npcList.Count; }
        }
        public NPC this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= npcList.Count) return null;
                return npcList[index];
            }
        }

        public List<NPC> GetList()
        {
            return npcList;
        }
    }
}