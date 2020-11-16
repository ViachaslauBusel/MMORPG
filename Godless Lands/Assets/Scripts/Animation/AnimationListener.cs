﻿using Animation;
using RUCP;
using RUCP.Handler;
using RUCP.Packets;
using SkillsBar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    private AnimationSkill animationSkill;
    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.PlayerAnim, PlayerAnim);
      //  RegisteredTypes.RegisterTypes(Types.PlayAnimSkill, PlayAnimSkill);
    }

  /*  private void PlayAnimSkill(NetworkWriter nw)
    {
        int animation = nw.ReadInt();
        int milliseconds = nw.ReadInt();

        PanelSkills.Hide((milliseconds - NetworkManager.Socket.GetPing())/1000);
        animationSkill.UseAnimation(1, animation, (milliseconds / 1000));
    }*/

    private void Start()
    {
        animationSkill = GetComponent<AnimationSkill>();
    }

    private void PlayerAnim(Packet nw)
    {
       
       
        int layer = nw.ReadByte();
        int animation =  nw.ReadInt();
        switch (layer)
        {
            case 1: //layer 1 = Проиграть анимацию умений с контролем времени
                int milliseconds = nw.ReadInt();

                PanelSkills.Hide((milliseconds - NetworkManager.Socket.NetworkInfo.Ping) / 1000.0f);
                animationSkill.UseAnimationSkill(animation, (milliseconds / 1000.0f));
                break;
            case 2: //layer 2 = Проиграть анимацию состояния без контроля времени
                animationSkill.UseAnimState(animation);
                break;
            case 3: //layer 3 = Проиграть анимацию состояния с контролем времени
                int timeMilli = nw.ReadInt();
                PanelSkills.Hide((timeMilli - NetworkManager.Socket.NetworkInfo.Ping) / 1000.0f);
                animationSkill.UseAnimState(animation, (timeMilli / 1000.0f));
                break;
        }
       
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.PlayerAnim);
       // RegisteredTypes.UnregisterTypes(Types.PlayAnimSkill);
    }
}