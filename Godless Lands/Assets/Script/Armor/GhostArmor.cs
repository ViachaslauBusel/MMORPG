using Items;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostArmor : MonoBehaviour{

    public Transform weapon_right;
    public Transform weapon_sheath;
    private Transform weapon_parent;
    private GameObject weapon_obj;
 //   private ItemsList itemsList;
    private Animator animator;
    private bool combatState = false;
    private int weaponType = 0;


    public void ReadArmor(NetworkWriter nw)
    {
        animator = GetComponent<Animator>();
       
        weapon_parent = weapon_sheath;
       // itemsList = Resources.Load("Inventory/ItemList") as ItemsList;
        //weapon
        int item_id = nw.ReadInt();
        Item item = Inventory.CreateItem(item_id);
        PutOnWeapon(item);
    }

    public void UpdateArmor(NetworkWriter nw)
    {
        ItemType part = (ItemType)nw.ReadInt();
        int item_id = nw.ReadInt();

        Item item = Inventory.CreateItem(item_id);
        PutOnWeapon(item);
    }

    public void SetCombatState(bool state)
    {
        combatState = state;
        animator.SetBool("armed", combatState);
        if (weaponType == 0) ChangeParent();
    }

    public void ChangeParent()
    {
        if (combatState)
        {
            weapon_parent = weapon_right;
        }
        else
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
}
