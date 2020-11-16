using RUCP;
using RUCP.Network;
using RUCP.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTaking : MonoBehaviour {

    private Camera _camera;
    private float sleep = 0.050f;//ms

    private void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetButton("MouseLeft"))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            StartCoroutine(Sleep());
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            int layermask = 1 << 8;
            layermask |= 1 << 10;

            if (Physics.Raycast(ray, out hit, layermask))
            {
                    switch (hit.transform.gameObject.layer)
                    {
                        case 8://Monsters
                                SetTarget(2, hit.transform.GetComponent<TargetObject>().Id());
                            break;
                        case 9://Players
                            SetTarget(1, hit.transform.GetComponent<TargetObject>().Id());
                            break;
                    case 10://DeadMobs
                        FindDrop(2, hit.transform.GetComponent<TargetObject>().Id());
                        break;
                    case 14:
                        FindDrop(4, hit.transform.GetComponent<TargetObject>().Id());
                        break;
                    }
            }
        }
    }
    private IEnumerator Sleep()
    {
        enabled = false;
        yield return new WaitForSeconds(sleep);
        enabled = true;
    }

    public void TargetOff()
    {
        SetTarget(0, 0);
    }

    /// <summary>
    /// 0 = target off
    /// 1 = Players
    /// 2 = Mobs
    /// 3 = NPC
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="id_target"></param>
    private void SetTarget(byte layer, int id_target)
    {

        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(Types.Target);
        nw.WriteByte(layer);
        nw.WriteInt(id_target);

        NetworkManager.Send(nw);
    }

    private void FindDrop(byte layer, int id_target)
    {

        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(Types.FindDrop);
        nw.WriteByte(layer);
        nw.WriteInt(id_target);

        NetworkManager.Send(nw);
    }
}
