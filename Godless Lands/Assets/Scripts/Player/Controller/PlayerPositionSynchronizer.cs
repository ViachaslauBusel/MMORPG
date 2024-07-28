using Helpers;
using Network.Core;
using Network.Object.Visualization;
using Protocol.Data.Replicated.Transform;
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
        private long m_timeStamp;
        private NetworkManager _networkManager;
        private Transform _character;
        private PlayerController _controller;
        private IVisualRepresentation _skinObjectHolder;



        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        private void Awake()
        {
            _skinObjectHolder = GetComponentInParent<IVisualRepresentation>();
            _controller = GetComponent<PlayerController>();

            _controller.OnStartMove += OnStartMove;
            _controller.OnStopMove += OnStopMove;
            _controller.OnJump += OnJump;
        }

        private void Start()
        {
            _skinObjectHolder.OnVisualObjectUpdated += AssignCharacterController;
            AssignCharacterController(_skinObjectHolder.VisualObject);
        }

        private void AssignCharacterController(GameObject character)
        {
            _character = character.transform;
        }

        private void FixedUpdate()
        {
            if (--cicle < 0)
            {
                cicle = m_send_cicle_frequency;
                SyncPosition();
            }
        }

        private void OnStartMove()
        {
            cicle = m_send_cicle_frequency;
            enabled = true;
        }

        private void OnStopMove()
        {
            //Debug.Log("StopMove");
            SendPosition(false);
            enabled = false;
        }

        private void OnJump()
        {
            SendPosition(_controller.InMove, MoveFlag.Jump);
        }


        private void SyncPosition()
        {
            //If the character has moved the minimum distance for synchronization
            if (Vector3.Distance(m_lastSentPosition.GetClearY(), _character.position.GetClearY()) > 0.1f)
            {
                SendPosition(true);
                return;
            }

            if (Mathf.Abs(Mathf.DeltaAngle(m_lastSentRotation, _character.rotation.eulerAngles.y)) > 0.5f)
            {

                m_lastSentRotation = _character.rotation.eulerAngles.y;
                //TODO msg
                //Packet nw = new Packet(Channel.Discard);
                //nw.WriteType(Types.Rotation);
                //nw.WriteFloat(lastSentRotation);

                //NetworkManager.Send(nw);
            }

        }

        private void SendPosition(bool inMove, MoveFlag flag = MoveFlag.None)
        {
            float speed = Vector3.Distance(_character.position.ClearY(), m_lastSentPosition.ClearY()) / ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - m_timeStamp) / 1000.0f);
            m_timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            m_lastSentPosition = _character.position;
            m_lastSentRotation = _character.rotation.eulerAngles.y;

            MSG_PLAYER_INPUT_CS playerInput = new MSG_PLAYER_INPUT_CS();

            playerInput.Position = m_lastSentPosition.ToNumeric();
            playerInput.Rotation = m_lastSentRotation;
            playerInput.Velocity = speed;
            playerInput.InMove = inMove;
            playerInput.MoveFlag = flag;

            _networkManager.Client.Send(playerInput);
        }
    } 
}
