using MCamera;
using Services.Replication;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Nickname
{
    public class CharacterNicknameRenderer : MonoBehaviour
    {
        [SerializeField]
        private Text m_nicknameText;
        private SessionManagmentService m_sessionManagmentService;
        private UnitNameHandler m_unitNameHandler;
        private CameraController m_camera;

        [Inject]
        private void Construct(CameraController camera, SessionManagmentService sessionManagmentService)
        {
            m_sessionManagmentService = sessionManagmentService;
            m_camera = camera;
        }

        private void Awake()
        {
            m_unitNameHandler = GetComponentInParent<UnitNameHandler>();
           
            var objInfo = GetComponentInParent<NetworkComponentsProvider>();

            //IF this is our character, we don't need to render nickname
            if(objInfo.NetworkGameObjectID == m_sessionManagmentService.CharacterObjectID)
            {
                gameObject.SetActive(false);
                return;
            }

            if(m_unitNameHandler == null)
            {
                Debug.LogError("UnitNameHandler not found");
                return;
            }

            m_unitNameHandler.OnNameChanged += OnNameChanged;
            OnNameChanged(m_unitNameHandler.UnitName);
        }

        private void OnNameChanged(string nickname)
        {
            m_nicknameText.text = nickname;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - m_camera.Camera.transform.position);
        }
    }
}