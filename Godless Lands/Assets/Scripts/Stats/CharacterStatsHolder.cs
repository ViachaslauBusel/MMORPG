using Protocol.Data.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Systems.Stats
{
    internal class CharacterStatsHolder : MonoBehaviour
    {
        private Dictionary<StatCode, int> m_stats = new Dictionary<StatCode, int>();
        private string m_name;


        public event Action OnUpdateStats;

        public void SetStat(StatCode key, string value)
        {
            if(key == StatCode.Name)
            {
               m_name = value;
            }
        }

        internal void SetStat(StatCode key, int value)
        {
            if(m_stats.ContainsKey(key))
            {
                m_stats[key] = value;
            }
            else
            {
                m_stats.Add(key, value);
            }
        }

        internal int GetStat(StatCode key)
        {
            if(m_stats.ContainsKey(key))
            {
                return m_stats[key];
            }
            else
            {
                return 0;
            }
        }

        public string GetName()
        {
            return m_name;
        }

        public void CallStatsUpdateEvent()
        {
            OnUpdateStats?.Invoke();
        }
    }
}
