using System.Collections;
using System.Collections.Generic;

// Classes for save/load tabs data.

[System.Serializable]
public class TabWrapper{
	public List<TabData> tabData;
}

[System.Serializable]
public class TabData{
	public string tabName;
	public bool tabActive;
	public int tabFontSize;
	public int tabIndex;
	public List<ChatLayer> tabLayers;
}