using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat_Message : MonoBehaviour {

	[SerializeField] Text messageText;

	public int LayerID {get;set;}

	public string Text{
		set{
			messageText.text = value;
		}
	}

	public int FontSize{
		set {
			messageText.fontSize = value;
		}
	}

	public Color Color{
		set {
			messageText.color = value;
		}
	}

}
