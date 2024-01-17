using DynamicsObjects;
using MCamera;
using Protocol.MSG.Game.ToServer;
using RUCP;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UnitTargetRequestSender : MonoBehaviour {

    private Camera _camera;
    private float sleep = 0.050f;//ms
    private CameraController m_cameraController;
    private NetworkManager m_networkManager;
    private long m_lastTime = 0;


    [Inject]
    private void Construct(CameraController cameraController, NetworkManager networkManager)
    {
        m_cameraController = cameraController;
        m_networkManager = networkManager;
    }

    private void Start()
    {
        _camera = m_cameraController.Camera;
    }

    private void Update()
    {
        if (Input.GetButton("MouseLeft"))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            long cooldown = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - m_lastTime;
            if (cooldown < 100) return;

            StartCoroutine(Sleep());
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //int layermask = 1 << 8;
           // layermask |= 1 << 10;

            if (Physics.Raycast(ray, out hit))
            {
                DynamicObject dynamicObject = hit.transform.GetComponentInParent<DynamicObject>();

                if (dynamicObject != null)
                {
                       SetTarget(dynamicObject.ID);
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
        SetTarget(0);
    }


    public void SetTarget(int gameObjectId)
    {
        MSG_UNIT_TARGET_REQUEST_CS targetRequest = new MSG_UNIT_TARGET_REQUEST_CS();
        targetRequest.GameObjectId = gameObjectId;
        m_networkManager.Client.Send(targetRequest);
        m_lastTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    private void FindDrop(byte layer, int id_target)
    {
    //TOD msg

        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.FindDrop);
        //nw.WriteByte(layer);
        //nw.WriteInt(id_target);

        //NetworkManager.Send(nw);
    }
}
