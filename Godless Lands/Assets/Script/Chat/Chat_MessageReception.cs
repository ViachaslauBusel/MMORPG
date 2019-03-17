using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using UnityEngine.UI;
using Items;

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
	[SerializeField] Chat_Manager chat;

    private void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.ChatMessage, ChatMessage);
    }

    private void Start()
    {
        itemsList = Resources.Load("Inventory/ItemList") as ItemsList;
      //  rectTransform = GetComponent<RectTransform>();
        //messages = new GameObject[max_message];
    }

    private void ChatMessage(NetworkWriter nw)
    {
       
        //GameObject obj =  Instantiate(msg_obj);
        //obj.transform.SetParent(transform);
        //obj.GetComponent<RectTransform>().localScale = Vector3.one;
		int layer = nw.ReadInt();
        string character = nw.ReadString();
        string _message = nw.ReadString();
        if (_message.Contains("%")) _message = ReplaceSpecial(_message, nw);
        //obj.GetComponent<Text>().text = character + ": " + _message;

        //if (messages[index] != null) Destroy(messages[index]);
        //messages[index] = obj;

        //index++;
        //index %= max_message;
		chat.ReceiveMessage(_message, character, layer);

       // auto_scroll = true;

    }

    private string ReplaceSpecial(string msg, NetworkWriter nw)
    {
        if (nw.AvailableBytes < 4) return msg;
        Item _item = itemsList.GetItem(nw.ReadInt());
        if (_item == null) return msg;
        return msg.Replace("%item:", _item.nameItem);
    }
    private void Update()
    {
       /* if (auto_scroll)
        {
            print("true");
            auto_scroll = false;
            while(auto_scroll_index != index)
            {
              

                Vector2 pos = rectTransform.anchoredPosition;
                pos.y += messages[auto_scroll_index].GetComponent<RectTransform>().sizeDelta.y;
                print(rectTransform.sizeDelta);
                if (messages[auto_scroll_index].GetComponent<RectTransform>().sizeDelta.y == 0.0f) print("error");
                print(messages[auto_scroll_index].GetComponent<RectTransform>().sizeDelta.y);
                rectTransform.anchoredPosition = pos;

                auto_scroll_index++;
                auto_scroll_index %= max_message;
            }
        }*/
    }
    public void AutoScroll()
    {

    }
    private void OnDestroy()
    {

        RegisteredTypes.UnregisterTypes(Types.ChatMessage);
    }
}
