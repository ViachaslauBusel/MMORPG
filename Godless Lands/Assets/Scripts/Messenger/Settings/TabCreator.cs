using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messenger.Settings {
    public class TabCreator : MonoBehaviour
    {

        public void Create()
        {
            MessengerSettings.Instance.EditableTabs.Add(CreateEmpty());
            MessengerSettings.Instance.UpdateChanges();
        }

        public static Tab CreateAll()
        {
            Tab tab = new Tab() { name = "All" };
            foreach (MsgLayer layer in Enum.GetValues(typeof(MsgLayer)))
            {
                Layer lay = tab.GetLay(layer);
                lay.use = true;
                lay.color = MsgColor.White;
            }
            return tab;
        }

        public static Tab CreateEmpty()
        {
            return new Tab() { name = "new tab" };
        }
    }
}