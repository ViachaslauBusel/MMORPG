using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSetting : MonoBehaviour
{
    public GameObject prefGameLoader;
    public Slider sliderScaleUI;
    public Slider sliderViewDistance;
    public Slider sliderBaseMapDis;
    public Slider sliderDetailDistance;
    public Slider sliderDetailDensity;
    public Dropdown dropdownQulity;
    public Dropdown dropdownPostProfile;
    public PostProfiles postProfiles;
    private MapLoader mapLoader;
    private UIScaler scaler;

    void Awake()
    {
        mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();
        scaler = GetComponentInParent<UIScaler>();
    }

    private void Start()
    {
        sliderScaleUI.value = PlayerPrefs.GetFloat("scaleUI", 0.3f);
        ApplyScaleUI();

        sliderViewDistance.value = mapLoader.areaVisible;
        sliderBaseMapDis.value = mapLoader.basemapDistance;

        dropdownQulity.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            optionDatas.Add(new Dropdown.OptionData(QualitySettings.names[i]));
        }
        dropdownQulity.AddOptions(optionDatas);
        dropdownQulity.value = mapLoader.Qulity;

        sliderDetailDistance.value = mapLoader.DetailDistance;
        sliderDetailDensity.value = mapLoader.DetailDensity;

        dropdownPostProfile.ClearOptions();
         optionDatas = new List<Dropdown.OptionData>();
        foreach(string _profile in postProfiles.GetProfiles())
        {
            optionDatas.Add(new Dropdown.OptionData(_profile));
        }
        dropdownPostProfile.AddOptions(optionDatas);
        dropdownPostProfile.value = postProfiles.Profile;
    }



    //Применить размер UI элементов
    private void ApplyScaleUI()
    {
        scaler.apply(sliderScaleUI.value);
        PlayerPrefs.SetFloat("scaleUI", sliderScaleUI.value);
    }

    public void Apply()
    {
        bool restart = false;
        ApplyScaleUI();
        //Зона прогрузки террейна
        if (mapLoader.areaVisible != (int)sliderViewDistance.value)
        {
            mapLoader.areaVisible = (int)sliderViewDistance.value;
            restart = true;
        }
        //Зона прогрузки текстуры хорошего качества
        if (mapLoader.basemapDistance != (int)sliderBaseMapDis.value)
        {
            mapLoader.basemapDistance = (int)sliderBaseMapDis.value;
            restart = true;
        }
        //Качество графики
        if (dropdownQulity.value != mapLoader.Qulity)
        {
            //  print("quality: " + textureQuality.value);
            mapLoader.Qulity = dropdownQulity.value;
            restart = true;
        }
        //Зона прогрузки травы
        if (mapLoader.DetailDistance != sliderDetailDistance.value)
        {
            mapLoader.DetailDistance = sliderDetailDistance.value;
            restart = true;
        }
        //Количество травы
        if (mapLoader.DetailDensity != sliderDetailDensity.value)
        {
            mapLoader.DetailDensity = sliderDetailDensity.value;
            restart = true;
        }
        if (postProfiles.Profile != dropdownPostProfile.value)
        {
            postProfiles.Profile = dropdownPostProfile.value;
        }
        if (restart) StartCoroutine(IERestart());
    }

    private IEnumerator IERestart()
    {
        Confirm confirm = Confirm.instant;
        confirm.SetTitle("Чтобы изменения вступили в силу, нужно перезагрузить игру");
        int answer = confirm.IsConfirm();
        while (answer < 0)
        {
            yield return 0;
            answer = confirm.IsConfirm();
        }
        if (answer == 1)//Yes
        {
            GameLoader gameLoader = Instantiate(prefGameLoader).GetComponent<GameLoader>();
            gameLoader.LoadPoint();
        }
    }
}
