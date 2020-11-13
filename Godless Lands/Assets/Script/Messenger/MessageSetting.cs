using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger
{
    public class MessageSetting : MonoBehaviour
    {
        public static MessageSetting Instance { get; private set; }
        [SerializeField] Font font;
        public int fontSize;



        private void Awake()
        {
            Instance = this;
        }

        internal static void Tune(Text textComponent)
        {
            textComponent.font = Instance.font;
            textComponent.fontSize = Instance.fontSize;
        }
    }
}