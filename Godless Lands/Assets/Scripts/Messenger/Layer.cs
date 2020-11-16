using Messenger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messenger
{
    [System.Serializable]
    public class Layer
    {
        [SerializeField] MsgLayer layer;
        public MsgColor color;
        public bool use = false;

        public MsgLayer MsgLayer => layer;

        public Layer(MsgLayer layer)
        {
            this.layer = layer;
        }
    }
}