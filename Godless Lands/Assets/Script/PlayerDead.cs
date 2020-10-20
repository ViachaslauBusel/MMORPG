using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using RUCP.Packets;
using RUCP.Network;

public class PlayerDead : MonoBehaviour {

    public Canvas canvas_dead;

    private PlayerController controller;
    private AnimationSkill animationSkill;


    private void Start()
    {
        controller = GetComponent<PlayerController>();
        animationSkill = GetComponent<AnimationSkill>();
        canvas_dead.enabled = false;
    }

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.PlayerDead, PlayerDeadVoid);
        HandlersStorage.RegisterHandler(Types.PlayerResurrection, PlayerResurrection);
    }

    private void PlayerResurrection(Packet nw)
    {
        transform.position = nw.ReadVector3();

        animationSkill.UseAnimState(5);

        controller.enabled = true;
    }

    private void PlayerDeadVoid(Packet nw)//Пакет 
    {
        animationSkill.DeadOn();
        controller.enabled = false;
       
        canvas_dead.enabled = true;
    }

    public void PlayerRes()
    {
        canvas_dead.enabled = false;

        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(Types.PlayerResurrection);
        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.PlayerDead);
        HandlersStorage.UnregisterHandler(Types.PlayerResurrection);
    }
}
