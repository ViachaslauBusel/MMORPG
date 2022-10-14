using Player;
using RUCP;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private Canvas interaction;
    private UISort uISort;

    private void Start()
    {
        interaction = GetComponent<Canvas>();
        uISort = GetComponentInParent<UISort>();
        interaction.enabled = false;
    }

    public void OnOffInteraction()
    {
        interaction.enabled = !interaction.enabled;
        if (interaction.enabled) uISort.PickUp(interaction);
    }

    public void Teleport()
    {
    //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.Skill);
        //nw.WriteInt(-1);
        //nw.WriteFloat(PlayerController.Transform.forward.x);//Направление игрока
        //nw.WriteFloat(PlayerController.Transform.forward.z);
        //NetworkManager.Send(nw);
    }
}
