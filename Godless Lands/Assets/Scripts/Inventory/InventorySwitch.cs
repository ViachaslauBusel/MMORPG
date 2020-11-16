using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySwitch : MonoBehaviour
{
    public GameObject bag;
    public GameObject packback;
    public Button but_bag;
    public Button but_packback;
    public Text txt_bag;
    public Text txt_packback;
    public GameObject weightBag;
    public GameObject weightPackback;

    private void Start()
    {
        OnPackback();
    }

    public void OnBag()
    {
        bag.SetActive(true);
        packback.SetActive(false);
        but_bag.interactable = false;
        but_packback.interactable = true;
        txt_bag.enabled = true;
        txt_packback.enabled = false;
        weightBag.SetActive(true);
        weightPackback.SetActive(false);
    }
    public void OnPackback()
    {
        bag.SetActive(false);
        packback.SetActive(true);
        but_bag.interactable = true;
        but_packback.interactable = false;
        txt_bag.enabled = false;
        txt_packback.enabled = true;
        weightBag.SetActive(false);
        weightPackback.SetActive(true);
    }
}
