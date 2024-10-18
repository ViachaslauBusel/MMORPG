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
    public class CharacterInstancePositionSynchronizer : MonoBehaviour
    {
        /// <summary>
        /// Количество кадров между отправкой пакетов
        /// </summary>
        private const int SEND_CYCLE_FREQUENCY = 20;
        private int _cicle = 20;
        private Vector3 _lastSentPosition = Vector3.zero;
        private float _lastSentRotation = 0.0f;
        private long _timeStamp;
        private NetworkManager _networkManager;
        private Transform _character;
        private CharacterInstanceMovementController _controller;
        private IVisualRepresentation _skinObjectHolder;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        private void Awake()
        {
            _skinObjectHolder = GetComponentInParent<IVisualRepresentation>();
            _controller = GetComponent<CharacterInstanceMovementController>();

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
            if (--_cicle < 0)
            {
                _cicle = SEND_CYCLE_FREQUENCY;
                SyncPosition();
            }
        }

        private void OnStartMove()
        {
            _cicle = SEND_CYCLE_FREQUENCY;
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
            if (Vector3.Distance(_lastSentPosition.GetClearY(), _character.position.GetClearY()) > 0.1f)
            {
                SendPosition(true);
                return;
            }

            if (Mathf.Abs(Mathf.DeltaAngle(_lastSentRotation, _character.rotation.eulerAngles.y)) > 0.5f)
            {

                _lastSentRotation = _character.rotation.eulerAngles.y;
                //TODO msg
                //Packet nw = new Packet(Channel.Discard);
                //nw.WriteType(Types.Rotation);
                //nw.WriteFloat(lastSentRotation);

                //NetworkManager.Send(nw);
            }

        }

        private void SendPosition(bool inMove, MoveFlag flag = MoveFlag.None)
        {
            float speed = Vector3.Distance(_character.position.ClearY(), _lastSentPosition.ClearY()) / ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _timeStamp) / 1000.0f);
            _timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            _lastSentPosition = _character.position;
            _lastSentRotation = _character.rotation.eulerAngles.y;

            MSG_PLAYER_INPUT_CS playerInput = new MSG_PLAYER_INPUT_CS();

            playerInput.Position = _lastSentPosition.ToNumeric();
            playerInput.Rotation = _lastSentRotation;
            playerInput.Velocity = speed;
            playerInput.InMove = inMove;
            playerInput.MoveFlag = flag;
            playerInput.Segment = _controller.Segment;

            _networkManager.Client.Send(playerInput);
        }
    } 
}
