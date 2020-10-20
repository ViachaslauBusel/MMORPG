using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using UnityEngine.UI;
using Items;
using RUCP.Packets;

public class Chat_MessageReception : MonoBehaviour
{
    //public GameObject msg_obj;
    //private GameObject[] messages;
    //private int index = 0;
    //private int max_message = 30;
  //  private bool auto_scroll = false;
   // private int auto_scroll_index = 0;
  //  private RectTransform rectTransform;
    private ItemsList itemsList;
	private Chat_Manager chat;

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.ChatMessage, ChatMessage);
    }

    private void Start()
    {
		chat = GetComponent<Chat_Manager>();
        itemsList = Resources.Load("Inventory/ItemList") as ItemsList;
      //  rectTransform = GetComponent<RectTransform>();
        //messages = new GameObject[max_message];
    }

    private void ChatMessage(Packet nw)
    {
       

		int layer = nw.ReadInt();
        string character = nw.ReadString();
        string _message = nw.ReadString();
        if (_message.Contains("%")) _message = ReplaceSpecial(_message, nw);

     
		chat.ReceiveMessage(_message, character, layer);
    }

    private string ReplaceSpecial(string msg, Packet nw)
    {
        if (nw.AvailableBytes < 4) return msg;
        Item _item = itemsList.GetItem(nw.ReadInt());
        if (_item == null) return msg;
        return msg.Replace("%item:", _item.nameItem);
    }
    private void OnDestroy()
    {

        HandlersStorage.UnregisterHandler(Types.ChatMessage);
    }
}
