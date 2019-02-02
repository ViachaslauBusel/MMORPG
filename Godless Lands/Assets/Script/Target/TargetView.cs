using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RUCP;
using System;
using Monsters;

public class TargetView : MonoBehaviour
{

    public GameObject targetCircle;
    private GameObject targetCircle_obj;
    private Text name_txt;
    private Image hp;
    private GameObject targetView;
    public static TargetObject target_obj;

    private void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.Target, Target);
        RegisteredTypes.RegisterTypes(Types.TargetUpdate, TargetUpdate);
    }

    

    private void Start()
    {
        name_txt = GetComponentInChildren<Text>();
        hp = transform.Find("ImageTarget/HP/HPBar").GetComponent<Image>();
        targetView = transform.Find("ImageTarget").gameObject;
        targetView.SetActive(false);
    }

    private void Target(NetworkWriter nw)
    {
        if(targetCircle_obj != null)
        {
            Destroy(targetCircle_obj);
        }
        int layer = nw.ReadInt();
        int id = nw.ReadInt();

        float hp = nw.ReadInt();
        float max_hp = nw.ReadInt();
        this.hp.fillAmount = hp / max_hp;


            target_obj = FindTarget(layer, id);
            if(target_obj == null)
            {
                targetView.SetActive(false);
                return;
            }
            name_txt.text = target_obj.GetName();

        targetCircle_obj = Instantiate(targetCircle);
        targetCircle_obj.transform.SetParent(target_obj.GetTransform());
        targetCircle_obj.transform.localPosition = Vector3.zero;
        targetCircle_obj.transform.localScale = Vector3.one;



        targetView.SetActive(true);
    }

    private void TargetUpdate(NetworkWriter nw)
    {
        int layer = nw.ReadInt();
        int id = nw.ReadInt();

        if (target_obj != null || target_obj.Layer() == layer || target_obj.Id() == id)
        {
            float hp = nw.ReadInt();
            float max_hp = nw.ReadInt();
            this.hp.fillAmount = hp / max_hp;
        }
    }

    public static TargetObject FindTarget(int layer, int id)
    {
        if (layer == 2)
        {
            return MonstersManager.GetMonster(id);
        }
        if (layer == 1) return CharactersManager.GetCharacter(id);
        return null;

    }

    private void OnDestroy()
    {

        RegisteredTypes.UnregisterTypes(Types.Target);
        RegisteredTypes.UnregisterTypes(Types.TargetUpdate);
    }
}
