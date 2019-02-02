using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {

    public Image butTabGraphic;
    public Image butTabInput;
    public GameObject tabGraphic;
    public GameObject tabInput;
    private UISort uISort;
    private GraphicSetting graphicSetting;
    private Canvas setting;
  

    private void Awake()
    { 
        uISort = GetComponentInParent<UISort>();
        graphicSetting = GetComponentInChildren<GraphicSetting>();
        setting = GetComponent<Canvas>();
        setting.enabled = false;
    }

    private void Start()
    {
        OnGraphicTab();
    }
    public void OnGraphicTab()
    {
        HideAllTabs();
        butTabGraphic.enabled = false;
        tabGraphic.SetActive(true);
    }
    public void OnInputTab()
    {
        HideAllTabs();
        butTabInput.enabled = false;
        tabInput.SetActive(true);
    }
    private void HideAllTabs()
    {
        butTabGraphic.enabled = true;
        butTabInput.enabled = true;
        tabGraphic.SetActive(false);
        tabInput.SetActive(false);
    }

    public void Apply()
    {
        graphicSetting.Apply();
        setting.enabled = false;

    }

  

    public void OnOffSetting()
    {
        setting.enabled = !setting.enabled;
        if (setting.enabled) uISort.PickUp(setting);
    }
}
