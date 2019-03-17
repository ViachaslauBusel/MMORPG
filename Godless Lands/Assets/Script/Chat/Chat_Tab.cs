using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class Chat_Tab : MonoBehaviour{

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
			messageContainer.gameObject.SetActive(selected);
		}
	}

	[SerializeField] Text tabName;
	public string TabName{
		get{
			return tabName.text;
		}
		set {
			tabName.text = value;
		}
	}

	int tabFontSize;
	public int TabFontSize{
		get{
			return tabFontSize;
		}
		set {
			tabFontSize = value;

			foreach(Chat_Message message in messages){
				message.FontSize = TabFontSize;
			}
		}
	}

	[SerializeField] List<ChatLayer> layers = new List<ChatLayer>();
	public List<ChatLayer> Layers{
		get{
			return layers;
		}
		set{
			layers = value;
			ApplyLayerSettings();
		}
	}

	public delegate void ChatTabClickEvent(Chat_Tab tab);
	public ChatTabClickEvent OnChatTabClickEvent;

	public RectTransform messageContainer;

	public int maxMessages = 30;
	[System.NonSerialized] List<Chat_Message> messages = new List<Chat_Message>();

	public void OnChatTabClick(){
		if (OnChatTabClickEvent != null){
			OnChatTabClickEvent(this);
		}
	}

	public void ApplyLayerSettings(){
		// All messsages that are now disabled in this tab is hidden and can be restored,
		// but new messages will not be added;
		foreach(Chat_Message msg in messages){
			foreach(ChatLayer layer in Layers){
				if (msg.LayerID == layer.id){
					msg.Color = layer.color;
					msg.gameObject.SetActive(layer.visible);
				}
			}
		}
	}

	public void LoadDefaultSettings(){
		TabName = "New Tab";
		TabFontSize = 24;
		// TODO layer color alpha not applying. wtf?
		Layers = new List<ChatLayer>{
			new ChatLayer(){
				id = 0,
				name = "System",
				visible = true,
				color = new Color32(255,148,20,255)
			},
			new ChatLayer(){
				id = 1,
				name = "Damage In",
				visible = true,
				color = new Color32(0,30,255,255)
			},
			new ChatLayer(){
				id = 2,
				name = "Damage Out",
				visible = true,
				color = new Color32(0,90,255,255)
			},
			new ChatLayer(){
				id = 3,
				name = "Drop",
				visible = true,
				color = new Color32(37,255,110,255)
			},
			new ChatLayer(){
				id = 4,
				name = "Local",
				visible = true,
				color = new Color32(255,255,255,255)
			},
			new ChatLayer(){
				id = 5,
				name = "World",
				visible = true,
				color = new Color32(240,255,174,255)
			}
		};
	}

	public bool CanReceiveMessage(int layer){
		bool canReceive = false;

		foreach (ChatLayer chatLayer in layers){
			if (chatLayer.id == layer && chatLayer.visible){
				canReceive = true;
				break;
			}			
		}

		return canReceive;
	}

	public void ReceiveMessage(Chat_Message messageObject, string message, string player, int layerID){
		// Prevent that messages goes out of maximum defined per tab.
		CheckMessageCount();
		ChatLayer chatLayer = Layers.FirstOrDefault(layer => layer.id == layerID);

		if (chatLayer == null){
			// Maybe use a fallback layer instead of 0?
			chatLayer = Layers[0];
		}

		messageObject.FontSize = TabFontSize;
		messageObject.Color = chatLayer.color;
		messageObject.LayerID = layerID;

		string msg = string.Empty;
		msg += "[" + chatLayer.name + "] ";
		if (player != null && player.Length > 0) msg += player + ": ";
		msg += message;

		messageObject.Text = msg;
		
		messageObject.GetComponent<RectTransform>().localScale = Vector3.one;
		messageObject.transform.SetParent(messageContainer, false);
		messages.Add(messageObject);
	}

	void CheckMessageCount(){
		if (messages.Count > maxMessages && messages[0] != null){
			Destroy(messages[0].gameObject);
			messages.RemoveAt(0);
		}
	}
}