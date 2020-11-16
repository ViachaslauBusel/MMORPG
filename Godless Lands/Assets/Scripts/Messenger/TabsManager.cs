using Messenger.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Messenger {
    public class TabsManager : MonoBehaviour
    {
        public static TabsManager Instance { get; private set; }


        private TabsContainer tabsContainer;
        private string path;

        public event Action update;


        public TabsContainer Tabs => tabsContainer;

        private void Awake()
        {
            Instance = this;

            path = Application.dataPath + "/Streaming Assets/tabs container.dat";

            tabsContainer = ReadTabsContainer();
        }



        public void SaveTabsContainer(TabsContainer container)
        {
            //Копирование старых сообщений
            foreach(Tab oldTab in tabsContainer)
            {
               Tab newTab = container.Find(oldTab.ID);
                if(newTab != null)
                   newTab.messages = oldTab.messages;
            }
            tabsContainer = container;
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            { Directory.CreateDirectory(path); }
            File.WriteAllText(path, JsonUtility.ToJson(container));
            update?.Invoke();
        }

        internal TabsContainer ReadTabsContainer()
        {
            if (File.Exists(path))
                return JsonUtility.FromJson<TabsContainer>(File.ReadAllText(path));

          
            tabsContainer = new TabsContainer();
            tabsContainer.Add(TabCreator.CreateAll());
           // SaveTabsContainer(tabsContainer);
                return tabsContainer;
        }


        public void AddMessage(Message message)
        {
            foreach (Tab tab in tabsContainer)
                tab.AddMessage(message);
        }

        
    }
}