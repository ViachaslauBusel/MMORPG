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
        hp_txt.text = hp + "/" + max_hp;
        if (max_hp == 0) return;
        hp_bar.fillAmount = Mathf.Clamp01(hp / (float)max_hp);
    }

    private void UpdateMP(int mp, int maxMp)
    {
        mp_txt.text = mp + "/" + maxMp;
        if (maxMp == 0) return;
        mp_bar.fillAmount = Mathf.Clamp01(mp / (float)maxMp);
    }

    private void UpdateStamina(int stamina, int maxStamina)
    {
        stamina_txt.text = stamina + "/" + maxStamina;
        if (maxStamina == 0) return;
        stamina_bar.fillAmount = Mathf.Clamp01(stamina / (float)maxStamina);
    }

    private void OnDestroy()
    {
       _vitalsStorage.VitalsUpdated -= UpdateVitals;
    }
}
