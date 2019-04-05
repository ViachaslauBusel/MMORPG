using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChatSettings : MonoBehaviour{

	[SerializeField] RectTransform tabSettingsGroup;
	[SerializeField] RectTransform layerSettingsGroup;
	[SerializeField] RectTransform tabItemsContainer;
	[SerializeField] TabItem tabItemPrefab;
	[SerializeField] LayerItem layerItemPrefab;
	[SerializeField] RectTransform layerItemsContainer;
	[SerializeField] InputField tabNameInput;
	[SerializeField] Slider fontSizeSlider;
	[SerializeField] Text fontSizeValue;
	
	Chat_Manager chat;

	List<TabItem> tabItems = new List<TabItem>();
	TabItem selectedTabItem;

	List<LayerItem> chatLayers = new List<LayerItem>();

	void Awake() {
		chat = transform.GetComponentInParent<Chat_Manager>();
	}

	void OnEnable(){

		List<Chat_Tab> tabs = chat.Tabs;
		TabItem tabToSelect = null;
		
		if (tabs.Count == 0){
			layerSettingsGroup.gameObject.SetActive(false);
			tabSettingsGroup.gameObject.SetActive(false);
			return;
		}

		for(int i = 0; i < tabs.Count; i++){
			TabItem tabItem = Instantiate(tabItemPrefab, tabItemsContainer);
			tabItem.Tab = tabs[i];
			tabItem.OnTabClickEvent += OnSelectTab;

			if (tabs[i].TabSelected) {
				tabToSelect = tabItem;
			}
			tabItem.transform.SetSiblingIndex(tabs[i].TabIndex);
			tabItems.Add(tabItem);
		}
		
		for(int i = 0; i < tabItems[0].Tab.Layers.Count; i++){
			LayerItem layerItem = Instantiate(layerItemPrefab, layerItemsContainer);
			layerItem.OnLayerChangedEvent += ApplyLayerSettings;
			chatLayers.Add(layerItem);
		}

		if (tabToSelect != null){ 
			OnSelectTab(tabToSelect);
			SwitchToTabSettings();
		}
	}

	void OnDisable(){
		if (tabItems.Count > 0){
			foreach (TabItem tabItem in tabItems){
				Destroy(tabItem.gameObject);
			}
			tabItems.Clear();
		}
		if (chatLayers.Count > 0){
			foreach(LayerItem layer in chatLayers){
				Destroy(layer.gameObject);
			}
			chatLayers.Clear();
		}
	}

	//Method for button.
	public void CreateNewTab(){
		Chat_Tab tab = chat.CreateNewTab();

		TabItem tabItem = Instantiate(tabItemPrefab, tabItemsContainer);
		tabItem.Tab = tab;
		tabItem.OnTabClickEvent += OnSelectTab;
		tabItems.Add(tabItem);
		
		OnSelectTab(tabItem);

		SwitchToTabSettings();
	}

	public void OnClose(){
		chat.SaveTabs();
		gameObject.SetActive(false);
	}

	public void SwitchToLayerSettings(){
		if (selectedTabItem == null) return; 
		tabSettingsGroup.gameObject.SetActive(false);
		layerSettingsGroup.gameObject.SetActive(true);
	}

	public void SwitchToTabSettings(){
		if (selectedTabItem == null) return;
		layerSettingsGroup.gameObject.SetActive(false);
		tabSettingsGroup.gameObject.SetActive(true);
	}

	void LoadTabSettings(bool loadDefaultLayers = false){
		fontSizeSlider.value = selectedTabItem.Tab.TabFontSize;
		fontSizeValue.text = selectedTabItem.Tab.TabFontSize.ToString();
		tabNameInput.text = selectedTabItem.Tab.TabName;

		//It can not load reseted color data from chat.SelectedTab.Layers and I don't know why.
		List<ChatLayer> layers = loadDefaultLayers ? Chat_Tab.GetDefaultLayers() : chat.SelectedTab.Layers;
		for(int i = 0; i < layers.Count; i++){
			chatLayers[i].SetLayerData(layers[i]);
		}
	}

	public void ApplyFontSize(){
		selectedTabItem.Tab.TabFontSize = (int)fontSizeSlider.value;
		fontSizeValue.text = fontSizeSlider.value.ToString();
	}
	
	public void ApplyTabName(){
		selectedTabItem.TabName = selectedTabItem.Tab.TabName = tabNameInput.text;
	}

	void ApplyLayerSettings(LayerItem layer, bool visible, Color color){
		int layerIndex = chatLayers.IndexOf(layer);

		selectedTabItem.Tab.Layers[layerIndex].visible = visible;
		selectedTabItem.Tab.Layers[layerIndex].color = color;

		selectedTabItem.Tab.ApplyLayerSettings();
	}

	void OnSelectTab(TabItem tabItem){
		if (selectedTabItem) selectedTabItem.TabSelected = false; 

		selectedTabItem = tabItem;

		selectedTabItem.TabSelected = true;

		chat.OnSelectTab(selectedTabItem.Tab);

		LoadTabSettings();

		chat.ValidateTabsWidth();
	}

	public void ResetTabSettings(){
		chat.SelectedTab.TabName = "New Tab";
		chat.SelectedTab.TabFontSize = 24;
		chat.SelectedTab.Layers = Chat_Tab.GetDefaultLayers();
		LoadTabSettings(true);
	}

	public void RemoveSelectedTab(){
		if (selectedTabItem == null) return;

		chat.RemoveTab(selectedTabItem.Tab);
		tabItems.Remove(selectedTabItem);
		Destroy(selectedTabItem.gameObject);
		
		selectedTabItem = null;

		TabItem tabItem = tabItems.Find(t => chat.SelectedTab != null && t.Tab == chat.SelectedTab);

		if (tabItem != null){ 
			OnSelectTab(tabItem);
		}
		else{ 
			layerSettingsGroup.gameObject.SetActive(false);
			tabSettingsGroup.gameObject.SetActive(false);
		}
	}
}
