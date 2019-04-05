using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat_DropdownTab : MonoBehaviour{
	
	[SerializeField] Text tabName;
	public string TabName{
		set{ 
			tabName.text = value;
		}
	}
	
	public delegate void DropDownTabClickEvent();
	public DropDownTabClickEvent OnDropDownTabClickEvent;

	public void OnDropDownTabClick(){
		if (OnDropDownTabClickEvent != null){
			OnDropDownTabClickEvent();
		}
	}
}
