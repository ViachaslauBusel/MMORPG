﻿using MCamera;
using Network.Core;
using Protocol.MSG.Game.ToServer;
using System;
using Units.Registry;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Target
{
    public class UnitTargetRequestSender : IInitializable, ITickable
    {
        private Camera _camera;
        private CameraController _cameraController;
        private NetworkManager _networkManager;
        private long _lastRequestTime = 0;


        public UnitTargetRequestSender(CameraController cameraController, NetworkManager networkManager)
        {
            _cameraController = cameraController;
            _networkManager = networkManager;
        }

        public void Initialize()
        {
            _camera = _cameraController.Camera;
        }

        public void Tick()
        {
            if (Input.GetButton("MouseLeft"))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                long cooldown = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _lastRequestTime;
                if (cooldown < 200) return;

                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                //int layermask = 1 << 8;
                // layermask |= 1 << 10;

                if (Physics.Raycast(ray, out hit))
                {
                    IUnitVisualObject targetObject = hit.transform.GetComponentInParent<IUnitVisualObject>();

                    if (targetObject != null)
                    {
                        SetTarget(targetObject.NewtorkId);
                    }
                }
            }
        }

        public void TargetOff()
        {
            SetTarget(0);
        }

        private void SetTarget(int gameObjectId)
        {
            //Debug.Log($"UnitTargetRequestSender.SetTarget: {gameObjectId}");
            MSG_UNIT_TARGET_REQUEST_CS targetRequest = new MSG_UNIT_TARGET_REQUEST_CS();
            targetRequest.GameObjectId = gameObjectId;
            _networkManager.Client.Send(targetRequest);
            _lastRequestTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}