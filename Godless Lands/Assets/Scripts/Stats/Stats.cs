using RUCP;
using RUCP.Handler;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Stats : MonoBehaviour {

    public static Stats Instance { get; private set; }
    public event Action Update; 

    public Text left_txt;
    private Canvas stats;
    private UISort uISort;


    private string char_name;
    private int minPattack;
    public int maxPattack;
    public int physical_defense;
    private float attack_speed;
    [SerializeField] float moveSpeed = 0;
    private NetworkManager networkManager;

    public float MoveSpeed { get { return moveSpeed; } }


    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Types.LoadStats, LoadingStats);
        networkManager.RegisterHandler(Types.UpdateStats, UpdateStats);
    }

    private void Awake()
    {
        Instance = this;
       
    }

    private void UpdateStats(Packet nw)
    {
        minPattack = nw.ReadInt();
        maxPattack = nw.ReadInt();
        physical_defense = nw.ReadInt();
        attack_speed = nw.ReadFloat();
        moveSpeed = nw.ReadFloat();
        Redraw();
        Update?.Invoke();
    }

    private void LoadingStats(Packet nw)
    {
        char_name = nw.ReadString();
        HPView.Instance.SetName(char_name);

        minPattack = nw.ReadInt();
        maxPattack = nw.ReadInt();
        physical_defense = nw.ReadInt();
        attack_speed = nw.ReadFloat();
        moveSpeed = nw.ReadFloat();
        Redraw();
    }

    private void Redraw()
    {
        left_txt.text = "Имя - " + char_name + '\n'; //name

        left_txt.text += "Сила Атаки - " + minPattack + " - " + maxPattack + '\n';//p. attack


        left_txt.text += "Физ. Защита - " + physical_defense + '\n'; //p.def.

        left_txt.text += "Скор. Атаки - " + attack_speed.ToString("00.00") + '\n';  //attack speed
        left_txt.text += "Скорость - " + attack_speed.ToString("00.00") + '\n';
    }

    private void Start()
    {
        uISort = GetComponentInParent<UISort>();
        stats = GetComponent<Canvas>();
        stats.enabled = false;
    }

    public void OpenCloseStats()
    {
        stats.enabled = !stats.enabled;

        if (stats.enabled)
        {
            uISort.PickUp(stats);
        }
    }

    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Types.LoadStats);
        networkManager?.UnregisterHandler(Types.UpdateStats);
    }
}
