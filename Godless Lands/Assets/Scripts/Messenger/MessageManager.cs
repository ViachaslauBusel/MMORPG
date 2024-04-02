using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger
{
    public class MessageManager : MonoBehaviour
    {
        public static MessageManager Instance { get; private set; }
        [SerializeField] Transform content;
        private List<Text> textComponents = new List<Text>();

        private void Awake()
        {
            Instance = this;
        }

        public void Clear()
        {
            foreach (Text text in textComponents)
                Destroy(text.gameObject);
            textComponents.Clear();
        }
        internal void Add(Message message)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(content);
            obj.transform.localScale = Vector3.one;
            Text text = obj.AddComponent<Text>();
            MessageSetting.Tune(text);

            switch (message.Color)
            {
                case MsgColor.White:
                    text.color = Color.white;
                    break;
                case MsgColor.Blue:
                    text.color = Color.blue;
                    break;
                case MsgColor.Cyan:
                    text.color = Color.cyan;
                    break;
                case MsgColor.Green:
                    text.color = Color.green;
                    break;
                case MsgColor.Magenta:
                    text.color = Color.magenta;
                    break;
                case MsgColor.Orange:
                    text.color = new Color(1.0f, 0.549f, 0f, 1.0f);
                    break;
                case MsgColor.Red:
                    text.color = Color.red;
                    break;
                case MsgColor.Yellow:
                    text.color = Color.yellow;
                    break;
            }

            text.text = message.ToString();
            textComponents.Add(text);
        }
    }
}