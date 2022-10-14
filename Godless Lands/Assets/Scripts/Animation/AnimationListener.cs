using RUCP;
using SkillsBar;
using UnityEngine;
using Zenject;

public class AnimationListener : MonoBehaviour
{
    private AnimationSkill animationSkill;
    private NetworkManager networkManager;

    [Inject]
    private void Constructor(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Types.PlayerAnim, PlayerAnim);
    }
    private void Awake()
    {
       
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

                PanelSkills.Hide((milliseconds - networkManager.Client.Statistic.Ping) / 1000.0f);
                animationSkill.UseAnimationSkill(animation, (milliseconds / 1000.0f));
                break;
            case 2: //layer 2 = Проиграть анимацию состояния без контроля времени
                animationSkill.UseAnimState(animation);
                break;
            case 3: //layer 3 = Проиграть анимацию состояния с контролем времени
                int timeMilli = nw.ReadInt();
                PanelSkills.Hide((timeMilli - networkManager.Client.Statistic.Ping) / 1000.0f);
                animationSkill.UseAnimState(animation, (timeMilli / 1000.0f));
                break;
        }
       
    }

    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Types.PlayerAnim);
       // RegisteredTypes.UnregisterTypes(Types.PlayAnimSkill);
    }
}
