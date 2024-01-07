using Protocol.MSG.Game.Messenger;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messenger
{
    public class Message
    {
        public MsgLayer Layer { get; set; }
        public MsgColor Color { get; set; }
        public string CharName { get; set; }
        public string Msg { get; set; }

        public override string ToString()
        {
            return CharName + ": " + Msg;
        }
    }
}