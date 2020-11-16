using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger
{
    [System.Serializable]
    public class Tab
    {
        public string name;
        public int fontSize = 14;
        public List<Layer> layers;
        public int ID;
        [NonSerialized]
        public Queue<Message> messages = new Queue<Message>(40);
        private static Tab select = null;

        public bool IsSelected 
        { get
            {
                if (select == null) return false;
                return this.ID == select.ID;
            }
        }

        internal void Select()
        {
            select = this;
            MessageManager.Instance.Clear();
            MessageSetting.Instance.fontSize = fontSize;
            foreach (Message message in messages)
            {
                Layer layer = layers.Find((l) => l.MsgLayer == message.Layer);
                message.Color = layer.color;
                MessageManager.Instance.Add(message);
            }
        }

        internal void AddMessage(Message message)
        {
            Layer layer = layers.Find((l) => l.MsgLayer == message.Layer);

            if (layer == null || !layer.use) return;

            message.Color = layer.color;
                messages.Enqueue(message);

            if (messages.Count > 30)
                messages.Dequeue();

            if (!IsSelected) return;

            MessageManager.Instance.Add(message); 
        }

        internal Layer GetLay(MsgLayer l)
        {
            if (layers == null) layers = new List<Layer>();
            Layer layerOBJ = layers.Find((lay) => lay.MsgLayer == l);
            if (layerOBJ == null)
            {
                layerOBJ = new Layer(l);
                layers.Add(layerOBJ);
            }
            return layerOBJ;
        }
    }
}