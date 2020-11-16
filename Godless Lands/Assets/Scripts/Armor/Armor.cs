using Items;
using RUCP.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public Transform weapon_hand;
    public Transform weapon_back;
    private Transform weapon_parent;
    private GameObject weapon;
    private GameObject pickaxe;
    private bool combatState = false;
    private Animator animator;

    public void Initialize()
    {
       animator = GetComponent<Animator>();
       weapon_parent = weapon_back;
    }

   
    public void PutItem(ItemType part, Item item)
    {
        switch (part)
        {
            case ItemType.Weapon:
                DressWeapon(item);
                break;
            case ItemType.Pickaxe:
                DressPickaxe(item);
                break;
        }
    }
    private void DressWeapon(Item _item)//Взять в руки оружие
    {
        if (weapon != null)//Если оружие уже было в руках удалить
        {
            Destroy(weapon);
        }
        if (_item?.IsExist() ?? false)//Если есть новое оружие надеть
        {

            weapon = Instantiate(_item.prefab);
            weapon.transform.SetParent(weapon_parent);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.localScale = Vector3.one;
            animator.SetInteger("weaponType", 1);
        }
        else animator.SetInteger("weaponType", 0);
    }
    private void DressPickaxe(Item _item)//Взять в руки оружие
    {
        if (pickaxe != null)//Если оружие уже было в руках удалить
        {
            Destroy(pickaxe);
        }
        if (_item != null && _item.IsExist())//Если есть новое оружие надеть
        {

            pickaxe = Instantiate(_item.prefab);
            pickaxe.transform.SetParent(weapon_hand);
            pickaxe.transform.localPosition = Vector3.zero;
            pickaxe.transform.localRotation = Quaternion.identity;
            pickaxe.transform.localScale = Vector3.one;
            pickaxe.SetActive(false);
        }
    }

    public void OnPickaxe() { if(pickaxe != null) pickaxe.SetActive(true); }
    public void OffPickaxe() { if (pickaxe != null) pickaxe.SetActive(false); }
    public void SetCombatstate(bool state)
    {
        combatState = state;
        animator.SetBool("armed", combatState);
    }
    public void ChangeParent()
    {
        if (combatState)
        {
            weapon_parent = weapon_hand;
        }
        else
        {
            weapon_parent = weapon_back;
        }

        if (weapon != null)
        {
            weapon.transform.SetParent(weapon_parent);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.localScale = Vector3.one;
        }
    }
}
