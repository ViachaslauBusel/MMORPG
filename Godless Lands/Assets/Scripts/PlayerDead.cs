using DynamicsObjects.Player;
using Player;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using Zenject;

public class PlayerDead : MonoBehaviour {

    public Canvas canvas_dead;

    private PlayerController controller;
    private AnimationSkill animationSkill;
    private NetworkManager networkManager;

    [Inject]
   private void Construct(NetworkManager networkManager)
   {
        this.networkManager = networkManager;
       // networkManager.RegisterHandler(Types.PlayerDead, PlayerDeadVoid);
        networkManager.RegisterHandler(Types.PlayerResurrection, PlayerResurrection);
    }


    private void Start()
    {
        controller = GetComponent<PlayerController>();
        animationSkill = GetComponent<AnimationSkill>();
        canvas_dead.enabled = false;
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
        //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.PlayerResurrection);
        //NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        //networkManager?.UnregisterHandler(Types.PlayerDead);
        networkManager?.UnregisterHandler(Types.PlayerResurrection);
    }
}
