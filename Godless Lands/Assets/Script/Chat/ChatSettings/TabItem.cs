using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabItem : MonoBehaviour{

	[SerializeField] Text tabName;
	public string TabName{
		set{
			tabName.text = value; 
		}
	}

	Chat_Tab tab;
	public Chat_Tab Tab {
		get{
			return tab;
		}
		set{
			tab = value;
			TabName = tab.TabName;
		}
	}

	[SerializeField] Image tabImage;
	bool selected;
	public bool TabSelected{
		get{
			return selected;
		}
		set{
			selected = value;
			if (selected){
				tabImage.color = new Color32(210,210,210,255);
			}
			else {
				tabImage.color = Color.white;
			}
		}
	}

	public delegate void TabClickEvent(TabItem tabItem);
	public TabClickEvent OnTabClickEvent;

	public void OnClick(){
		if (OnTabClickEvent != null){
			OnTabClickEvent(this);
		}
	}
}
