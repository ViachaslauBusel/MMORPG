using Protocol.Data.Stats;
using RUCP;
using RUCP.Handler;
using System;
using Systems.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HPView : MonoBehaviour {

    [SerializeField] Text name_txt;
    [SerializeField] Image hp_bar;
    [SerializeField] Text hp_txt;
    [SerializeField] Image mp_bar;
    [SerializeField] Text mp_txt;
    [SerializeField] Image stamina_bar;
    [SerializeField] Text stamina_txt;
    private CharacterStatsHolder _statsHolder;


    [Inject]
    private void Construct(CharacterStatsHolder characterStatsHolder)
    {
        _statsHolder = characterStatsHolder;
    }

    private void Start()
    {
        _statsHolder.OnUpdateStats += UpdateStats;
        UpdateStats();
    }

    private void UpdateStats()
    {
        SetName(_statsHolder.GetName());
        UpdateHP(_statsHolder.GetStat(StatCode.HP), _statsHolder.GetStat(StatCode.MaxHP));
        UpdateMP(_statsHolder.GetStat(StatCode.MP), _statsHolder.GetStat(StatCode.MaxMP));
        UpdateStamina(_statsHolder.GetStat(StatCode.Stamina), _statsHolder.GetStat(StatCode.MaxStamina));
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
       _statsHolder.OnUpdateStats -= UpdateStats;
    }
}
