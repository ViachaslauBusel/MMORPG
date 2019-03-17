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

	void OnEnable(){
		chat = transform.GetComponentInParent<Chat_Manager>();

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
			tabItem.TabName = tabs[i].TabName;
			tabItem.OnTabClickEvent += OnSelectTab;
			if (tabs[i].TabSelected) {
				tabToSelect = tabItem;
			}
			tabItems.Add(tabItem);
		}
		
		for(int i = 0; i < tabItems[0].Tab.Layers.Count; i++){
			LayerItem layerItem = Instantiate(layerItemPrefab, layerItemsContainer);
			layerItem.OnLayerChangedEvent += ApplyLayerSettings;
			chatLayers.Add(layerItem);
		}

		if (tabToSelect != null){ 
			OnSelectTab(tabToSelect);
		}
		SwitchToTabSettings();
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
		if (chat.Tabs.Count + 1 > chat.maxTabs) return;

		Chat_Tab tab = chat.CreateNewTab();

		TabItem tabItem = Instantiate(tabItemPrefab, tabItemsContainer);
		tabItem.Tab = tab;
		tabItem.TabName = tab.TabName;
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

	void LoadTabSettings(){
		fontSizeSlider.value = selectedTabItem.Tab.TabFontSize;
		fontSizeValue.text = selectedTabItem.Tab.TabFontSize.ToString();
		tabNameInput.text = selectedTabItem.Tab.TabName;

		for(int i = 0; i < selectedTabItem.Tab.Layers.Count; i++){
			chatLayers[i].SetLayerData(selectedTabItem.Tab.Layers[i]);
		}
	}

	public void ApplyFontSize(){
		selectedTabItem.Tab.TabFontSize = (int)fontSizeSlider.value;
		fontSizeValue.text = fontSizeSlider.value.ToString();
	}
	
	public void ApplyTabName(){
		selectedTabItem.TabName = tabNameInput.text;
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
	}

	public void ResetTabSettings(){
		selectedTabItem.Tab.LoadDefaultSettings();
		LoadTabSettings();
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
