﻿using Helpers;
using NetworkObjectVisualization;
using Protocol.MSG.Game;
using System;
using UnityEngine;
using Zenject;

namespace Player.Controller
{
    public class PlayerPositionSynchronizer : MonoBehaviour
    {
        /// <summary>
        /// Position sending frequency
        /// </summary>
        private const int m_send_cicle_frequency = 20;
        private int cicle = 20;
        private Vector3 m_lastSentPosition = Vector3.zero;
        private float m_lastSentRotation = 0.0f;
        private bool m_inMove = false;
        private long m_timeStamp;
        private NetworkManager m_networkManager;
        private Transform m_character;
        private PlayerController m_controller;
        private IVisualRepresentation m_skinObjectHolder;



        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            m_networkManager = networkManager;
        }

        private void Awake()
        {
            m_skinObjectHolder = GetComponentInParent<IVisualRepresentation>();
            m_controller = GetComponent<PlayerController>();
        }
        private void Start()
        {
            m_skinObjectHolder.OnVisualObjectUpdated += AssignCharacterController;
            AssignCharacterController(m_skinObjectHolder.VisualObject);
        }

        private void AssignCharacterController(GameObject character)
        {
            m_character = character.transform;
        }

        private void FixedUpdate()
        {
            if (--cicle < 0)
            {
                cicle = m_send_cicle_frequency;
                SyncPosition();
            }
        }

       

        private void SyncPosition()
        {
            //If the character has moved the minimum distance for synchronization
            if (Vector3.Distance(m_lastSentPosition.GetClearY(), m_character.position.GetClearY()) > 0.1f || m_controller.isJumping())
            {
                m_inMove = true;
                SendPosition();
                return;
            }

            //If in previously synchronization frame character has moved
            if (m_inMove)
            { m_inMove = false; SendPosition(); }

            if (Mathf.Abs(Mathf.DeltaAngle(m_lastSentRotation, m_character.rotation.eulerAngles.y)) > 0.5f)
            {

                m_lastSentRotation = m_character.rotation.eulerAngles.y;
                //TODO msg
                //Packet nw = new Packet(Channel.Discard);
                //nw.WriteType(Types.Rotation);
                //nw.WriteFloat(lastSentRotation);

                //NetworkManager.Send(nw);
            }

        }

        private void SendPosition()
        {
            float speed = Vector3.Distance(m_character.position.ClearY(), m_lastSentPosition.ClearY()) / ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - m_timeStamp) / 1000.0f);
            m_timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            m_lastSentPosition = m_character.position;
            m_lastSentRotation = m_character.rotation.eulerAngles.y;

            MSG_PLAYER_INPUT_CS playerInput = new MSG_PLAYER_INPUT_CS();

            playerInput.Position = m_lastSentPosition.ToNumeric();
            playerInput.Rotation = m_lastSentRotation;
            playerInput.Velocity = speed;
            playerInput.InMove = m_inMove;
            playerInput.MoveFlag = m_controller.TakeFlag();

            m_networkManager.Client.Send(playerInput);
        }
    } 
}