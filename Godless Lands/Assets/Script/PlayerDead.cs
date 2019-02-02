using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;

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
        RegisteredTypes.RegisterTypes(Types.PlayerDead, PlayerDeadVoid);
        RegisteredTypes.RegisterTypes(Types.PlayerResurrection, PlayerResurrection);
    }

    private void PlayerResurrection(NetworkWriter nw)
    {
        transform.position = nw.ReadVec3();

        animationSkill.UseAnimState(5);

        controller.enabled = true;
    }

    private void PlayerDeadVoid(NetworkWriter nw)
    {
        animationSkill.UseAnimState(4);
        animationSkill.dead = true;
        controller.enabled = false;
       
        canvas_dead.enabled = true;
    }

    public void PlayerRes()
    {
        animationSkill.dead = false;
        canvas_dead.enabled = false;

        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.PlayerResurrection);
        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.PlayerDead);
        RegisteredTypes.UnregisterTypes(Types.PlayerResurrection);
    }
}
