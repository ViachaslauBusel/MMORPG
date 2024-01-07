using Messenger;
using Protocol.MSG.Game.Messenger;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger.Settings
{
    public class LayerField : MonoBehaviour
    {
        [SerializeField] MsgLayer layer;
        private Toggle toggleUsage;
        private Dropdown dropdownColor;

        public MsgLayer Layer => layer;
        public Toggle Usage => toggleUsage;
        public Dropdown Color => dropdownColor;

        private void Awake()
        {
            toggleUsage = GetComponentInChildren<Toggle>();
            dropdownColor = GetComponentInChildren<Dropdown>();

            dropdownColor.options.Clear();
            foreach (MsgColor color in Enum.GetValues(typeof(MsgColor)))
            {
                dropdownColor.options.Add(new Dropdown.OptionData(color.ToString()));
            }
        }

    }
}