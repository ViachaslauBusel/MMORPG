using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Quests.Journal.UI
{
    internal class TextBlock : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_text;

        internal void AddLine(string line)
        {
           m_text.text += line + "\n";
        }

        internal void Clear()
        {
           m_text.text = "";
        }

        internal void SetColor(Color color)
        {
            m_text.color = color;
        }
    }
}
