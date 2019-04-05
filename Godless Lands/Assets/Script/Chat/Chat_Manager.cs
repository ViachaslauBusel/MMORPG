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

	List<Chat_Tab> tabs = new List<Chat_Tab>();
	public List<Chat_Tab> Tabs{ 
		get{ 
			return tabs;
		}
	}
	
	string chatSettingsFileName = "chatSettings.json";

	[SerializeField] GameObject tabsDropDown;
	[SerializeField] GameObject dropDownPanel;
	[SerializeField] RectTransform dropDownContainer;
	[SerializeField] Chat_DropdownTab dropDownTabPrefab;

	void Start(){
		LoadTabs();
	}

	void OnDestroy() {
		SaveTabs();	
	}

	public Chat_Tab CreateNewTab(string tabName = "Default", int tabIndex = -1){
		Chat_Tab newTab = Instantiate(tabPrefab, tabsParent);
		newTab.messageContainer = Instantiate(messageContainerPrefab, messageContainerParent) as RectTransform;
		newTab.messageContainer.gameObject.SetActive(false);

		newTab.DropdownTab = Instantiate(dropDownTabPrefab, dropDownContainer);
		newTab.DropdownTab.gameObject.SetActive(false);
		newTab.DropdownTab.OnDropDownTabClickEvent += delegate{ OnSelectDropDownTab(newTab);};

		if (tabIndex == -1){
			newTab.TabIndex = tabsParent.childCount;
			newTab.transform.SetSiblingIndex(tabsParent.childCount);
		}
		else { 
			newTab.TabIndex = tabIndex;
			newTab.transform.SetSiblingIndex(tabIndex);
		}
		
		newTab.OnChatTabClickEvent += OnSelectTab;
		newTab.TabName = tabName;
		
		tabs.Add(newTab);

		return newTab;
	}

	public void RemoveTab(Chat_Tab tab){
		tabs.Remove(tab);

		Destroy(tab.messageContainer.gameObject);
		Destroy(tab.gameObject);
		Destroy(tab.DropdownTab.gameObject);

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
				tabIndex = tab.TabIndex
			};
			dataToSave.Add(data);
		}

		string dataAsJson = JsonUtility.ToJson(new TabWrapper(){tabData=dataToSave});

        string filePath = Path.Combine(Application.persistentDataPath, chatSettingsFileName);

        File.WriteAllText(filePath, dataAsJson);
	}

	public void LoadTabs(){
		string filePath = Path.Combine(Application.persistentDataPath, chatSettingsFileName);

		Chat_Tab tabToActivate = null;

        if(File.Exists(filePath)){
            string dataAsJson = File.ReadAllText(filePath);

			TabWrapper loadedData = JsonUtility.FromJson<TabWrapper>(dataAsJson);

			foreach(TabData loadedTab in loadedData.tabData){
				Chat_Tab tab = CreateNewTab(loadedTab.tabName, loadedTab.tabIndex);
				tab.TabFontSize = loadedTab.tabFontSize;
				tab.Layers = loadedTab.tabLayers;
				if (loadedTab.tabActive) tabToActivate = tab;
			}
        }
		
		if (tabs.Count == 0){
			tabToActivate = CreateNewTab();
		}

		OnSelectTab(tabToActivate);

		ValidateTabsWidth();
	}

	public void ToggleDropDown(){ 
		dropDownPanel.SetActive(!dropDownPanel.activeSelf);
	}

	public void OnSelectDropDownTab(Chat_Tab tab){
		OnSelectTab(tab);
		tab.DropdownTab.gameObject.SetActive(false);
		ToggleDropDown();
		ValidateTabsWidth();
	}

	public void ValidateTabsWidth(){
		if (tabs.Count == 0) return;

		float parentWidth = tabsParent.rect.width;
		float totalTabsWidth = tabs[0].TabWidth * tabs.Count;
		bool activateDropDown = false;

		foreach (Chat_Tab tab in tabs.Reverse<Chat_Tab>()){
			if (tab.TabSelected) continue;

			if (totalTabsWidth > parentWidth){
				tab.gameObject.SetActive(false);
				tab.DropdownTab.gameObject.SetActive(true);

				activateDropDown = true;
				totalTabsWidth -= tab.TabWidth;
			}
			else if (totalTabsWidth <= parentWidth){ 
				tab.gameObject.SetActive(true);
				tab.DropdownTab.gameObject.SetActive(false);
			}
		}

		if (selectedTab.gameObject.activeSelf == false){
			selectedTab.gameObject.SetActive(true);
			selectedTab.DropdownTab.gameObject.SetActive(false);	
		}
		
		tabsDropDown.SetActive(activateDropDown);
	}
}