using Protocol.Data.Stats;
using Protocol.Data.Vitals;
using RUCP;
using RUCP.Handler;
using System;
using Systems.Stats;
using UnityEngine;
using UnityEngine.UI;
using Vitals;
using Zenject;

public class HPView : MonoBehaviour {

    [SerializeField] Text name_txt;
    [SerializeField] Image hp_bar;
    [SerializeField] Text hp_txt;
    [SerializeField] Image mp_bar;
    [SerializeField] Text mp_txt;
    [SerializeField] Image stamina_bar;
    [SerializeField] Text stamina_txt;
    private CharacterVitalsStorage _vitalsStorage;
    private CharacterStatsHolder _characterStatsHolder;


    [Inject]
    private void Construct(CharacterVitalsStorage vitalsStorage, CharacterStatsHolder characterStatsHolder)
    {
        _vitalsStorage = vitalsStorage;
        _characterStatsHolder = characterStatsHolder;
    }

    private void Start()
    {
        _vitalsStorage.VitalsUpdated += UpdateVitals;
        UpdateVitals();
    }

    private void UpdateVitals()
    {
        SetName(_characterStatsHolder.GetName());
        UpdateHP(_vitalsStorage.GetVital(VitalCode.Health).CurrentValue, _vitalsStorage.GetVital(VitalCode.Health).MaxValue);
        UpdateMP(_vitalsStorage.GetVital(VitalCode.Mana).CurrentValue, _vitalsStorage.GetVital(VitalCode.Mana).MaxValue);
        UpdateStamina(_vitalsStorage.GetVital(VitalCode.Stamina).CurrentValue, _vitalsStorage.GetVital(VitalCode.Stamina).MaxValue);
    }

    public void SetName(string _name)
    {
        name_txt.text = _name;
    }

    private void UpdateHP(int hp, int max_hp)
    {
        hp_bar.fillAmount = hp / (float)max_hp;
        hp_txt.text = hp + "/" + max_hp;
    }
    private void UpdateMP(int mp, int maxMp)
    {
        mp_bar.fillAmount = mp / (float)maxMp;
        mp_txt.text = mp + "/" + maxMp;
    }
    private void UpdateStamina(int stamina, int maxStamina)
    {
        stamina_bar.fillAmount = stamina / (float)maxStamina;
        stamina_txt.text = stamina + "/" + maxStamina;
    }

    private void OnDestroy()
    {
       _vitalsStorage.VitalsUpdated -= UpdateVitals;
    }
}
