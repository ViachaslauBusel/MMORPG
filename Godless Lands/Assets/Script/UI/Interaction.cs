using RUCP;
using System.Collections;
using System.Collections.Generic;
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
        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.Skill);
        nw.write(-1);
        nw.write(PlayerController.Transform.forward.x);//Направление игрока
        nw.write(PlayerController.Transform.forward.z);
        NetworkManager.Send(nw);
    }
}
