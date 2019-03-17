using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Chat_Manager : MonoBehaviour{

	[SerializeField] RectTransform tabsParent;

	[SerializeField] Chat_Tab tabPrefab;
	[SerializeField] Chat_Message messagePrefab;

	[SerializeField] ScrollRect chatScrollRect;

	[SerializeField] RectTransform messageContainerParent;
	[SerializeField] RectTransform messageContainerPrefab;

	Chat_Tab selectedTab;
	public Chat_Tab SelectedTab{ 
		get{ 
			return selectedTab;
		}
	}

	public int maxTabs = 6;

	List<Chat_Tab> tabs = new List<Chat_Tab>();
	public List<Chat_Tab> Tabs{ 
		get{ 
			return tabs;
		}
	}
	
	string chatSettingsFileName = "chatSettings.json";

	void Start(){
		LoadTabs();
	}

	public Chat_Tab CreateNewTab(bool activateAfterCreate = true){
		Chat_Tab newTab = Instantiate(tabPrefab, tabsParent);
		newTab.messageContainer = Instantiate(messageContainerPrefab, messageContainerParent) as RectTransform;
		
		newTab.messageContainer.gameObject.SetActive(false);
		newTab.transform.SetSiblingIndex(tabsParent.childCount);
		newTab.OnChatTabClickEvent += OnSelectTab;
		newTab.LoadDefaultSettings();
		
		tabs.Add(newTab);

		if (activateAfterCreate) OnSelectTab(newTab);
		return newTab;
	}

	public void RemoveTab(Chat_Tab tab){
		tabs.Remove(tab);

		Destroy(tab.messageContainer.gameObject);
		Destroy(tab.gameObject);

		OnSelectTab(tabs.FirstOrDefault());
	}

	public void OnSelectTab(Chat_Tab tab){
		if (tab == null) return;
		if (selectedTab) selectedTab.TabSelected = false;

		selectedTab = tab;

		selectedTab.TabSelected = true;

		chatScrollRect.content = selectedTab.messageContainer;
	}

	public void ReceiveMessage(string message, string player, int layer){
		foreach(Chat_Tab tab in tabs){
			if(tab.CanReceiveMessage(layer)){
				tab.ReceiveMessage(Instantiate(messagePrefab) as Chat_Message, message, player, layer);
			}
		}
	}

	public void SaveTabs(){
		List<TabData> dataToSave = new List<TabData>();

		foreach (Chat_Tab tab in tabs){
			TabData data = new TabData(){
				tabName = tab.TabName,
				tabActive = tab.TabSelected,
				tabFontSize = tab.TabFontSize,
				tabLayers = tab.Layers,
			};
			dataToSave.Add(data);
		}


		string dataAsJson = JsonUtility.ToJson(new TabWrapper(){tabData=dataToSave});

        string filePath = Path.Combine(Application.persistentDataPath, chatSettingsFileName);

        File.WriteAllText(filePath, dataAsJson);
	}

	public void LoadTabs(){
		string filePath = Path.Combine(Application.persistentDataPath, chatSettingsFileName);

        if(File.Exists(filePath)){
            string dataAsJson = File.ReadAllText(filePath);

			TabWrapper loadedData = JsonUtility.FromJson<TabWrapper>(dataAsJson);

			foreach(TabData loadedTab in loadedData.tabData){
				Chat_Tab tab = CreateNewTab(false);
				tab.TabName = loadedTab.tabName;
				tab.TabFontSize = loadedTab.tabFontSize;
				tab.Layers = loadedTab.tabLayers;
				if (loadedTab.tabActive) {
					OnSelectTab(tab);
				}
			}
        }
		
		if (tabs.Count == 0){
			Chat_Tab defaultTab = CreateNewTab();
			defaultTab.TabName = "Default";
			SaveTabs();
		}
	}
}