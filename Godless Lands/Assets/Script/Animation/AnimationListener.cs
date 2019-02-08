using Animation;
using RUCP;
using RUCP.Handler;
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
        RegisteredTypes.RegisterTypes(Types.PlayerAnim, PlayerAnim);
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

    private void PlayerAnim(NetworkWriter nw)
    {
       
       
        int layer = nw.ReadByte();
        int animation =  nw.ReadInt();
        switch (layer)
        {
            case 1: //layer 1 = Проиграть анимацию умений с контролем времени
                int milliseconds = nw.ReadInt();

                PanelSkills.Hide((milliseconds - NetworkManager.Socket.GetPing()) / 1000.0f);
                animationSkill.UseAnimationSkill(animation, (milliseconds / 1000.0f));
                break;
            case 2:
                animationSkill.UseAnimState(animation);
                break; //layer 2 = Проиграть анимацию состояния без контроля времени
        }
       
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.PlayerAnim);
       // RegisteredTypes.UnregisterTypes(Types.PlayAnimSkill);
    }
}
