using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat_Autoscroll : MonoBehaviour {

    private void OnGUI()
    {
        RectTransform rect = transform.parent.GetComponent<RectTransform>();
        RectTransform this_rect = GetComponent<RectTransform>();
        //  print("this: " + this_rect.sizeDelta.y);
        //   print("parent: " + rect.sizeDelta.y);
       
        if (rect.sizeDelta.y > rect.anchoredPosition.y)
        {
            Vector2 pos = rect.anchoredPosition;
            pos.y += this_rect.sizeDelta.y;
            if (pos.y > rect.sizeDelta.y) pos.y = rect.sizeDelta.y;
            rect.anchoredPosition = pos;
            
        }

        Destroy(this);
    }

}
