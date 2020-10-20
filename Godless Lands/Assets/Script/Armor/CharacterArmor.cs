using Items;
using RUCP;
using RUCP.Handler;
using RUCP.Network;
using RUCP.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterArmor : MonoBehaviour
{
    public Transform weapon_right;
    public Transform weapon_sheath;
    private Transform weapon_parent;
    private GameObject weapon_obj;
    private Animator animator;
    private bool combatState = false;
    private int weaponType = 0;

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.CombatState, CombatState);
    }

    private void CombatState(Packet nw)
    {

        if (nw.ReadBool())
        {
        //    print("Arm =" + combatState);
            if (!combatState) Arm();
        }
        else
        {
         //  print("Disarm =" + combatState);
            if (combatState) Disarm();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("armed", combatState);
        weapon_parent = weapon_sheath;
    }

    public void ChangeParent()
    {
        if (combatState)
        {
            weapon_parent = weapon_right;
        }else
        {
            weapon_parent = weapon_sheath;
        }

        if (weapon_obj != null)
        {
            weapon_obj.transform.SetParent(weapon_parent);
            weapon_obj.transform.localPosition = Vector3.zero;
            weapon_obj.transform.localRotation = Quaternion.identity;
            weapon_obj.transform.localScale = Vector3.one;
        }
    }

    public void PutOnWeapon(Item _item)//Взять в руки оружие
    {
        if (weapon_obj != null)//Если оружие уже было в руках удалить
        {
            Destroy(weapon_obj);
        }
        if (_item != null)//Если есть новое оружие надеть
        {
           
            weapon_obj = Instantiate(_item.prefab);
            weapon_obj.transform.SetParent(weapon_parent);
            weapon_obj.transform.localPosition = Vector3.zero;
            weapon_obj.transform.localRotation = Quaternion.identity;
            weapon_obj.transform.localScale = Vector3.one;
           
            weaponType = 1;
        }
        else
        {
            weaponType = 0;
        }
        animator.SetInteger("weaponType", weaponType);
    }

    private void Update()
    {
        if (Input.GetButtonDown("R"))
        {
            if (combatState)
            {
                Disarm();
            }
            else
            {
                Arm();
            }
            SendState();
            if (weaponType == 0) ChangeParent();
        }
    }

    private void SendState()
    {
        Packet nw = new Packet(Channel.Discard);
        nw.WriteType(Types.CombatState);
        nw.WriteBool(combatState);
        NetworkManager.Send(nw);
    }

    public void Arm()
    {
        combatState = true;
        animator.SetBool("armed", combatState);
    }
    public void Disarm()
    {
        combatState = false;
        animator.SetBool("armed", combatState);
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.CombatState);
    }
}
