using Loader;
using RUCP;
using RUCP.Handler;
using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerControllerOld : MonoBehaviour
    {


        public GameObject prefGameLoader;

        private static CharacterController character;
        private Vector3 moveDirection = Vector3.zero;
        private float gravity = 20.0F;

        private Animator animator;
        private static AnimationSkill animationSkill;

        private int send_cicle = 19;
        private int cicle = 20;

        private short syncNumber = 0;

        private GameLoader gameLoader;


        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
            networkManager.RegisterHandler(Types.TeleportToPoint, TeleportToPoint);
        }

        private void Awake()
        {
            enabled = false;
            //  

            animationSkill = GetComponent<AnimationSkill>();
            character = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }


        private void Start()
        {
            Stats.Instance.Update += UpdateStats;
        }
        private void UpdateStats()
        {
            animator.SetFloat("speedRun", Stats.Instance.MoveSpeed / 3.5f);
        }

        private void TeleportToPoint(Packet nw)
        {
            transform.position = nw.ReadVector3();
            lastSentPosition = transform.position;
            lastSentRotation = transform.rotation.eulerAngles.y;
            if (gameLoader != null) Destroy(gameLoader.gameObject);
            gameLoader = Instantiate(prefGameLoader).GetComponent<GameLoader>();
            gameLoader.LoadPoint();
        }

        public static AnimationSkill PlayerAnim()
        {
            return animationSkill;
        }

        public static Vector3 PlayerPos()
        {
            return character.transform.position;
        }
        public static Transform Transform
        {
            get { return character.transform; }
        }



        void Update()
        {
            if (character.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                if (moveDirection.magnitude > 1.0f)
                {
                    moveDirection.Normalize();
                }
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= (Stats.Instance.MoveSpeed * 0.95f);
            }
            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            character.Move(moveDirection * Time.deltaTime);

            Animation();
        }

        private void FixedUpdate()
        {
            if (cicle-- <= 0)
            {
                cicle = send_cicle;
                SyncPosition();
            }


        }

        private void Animation()
        {
            if (character.isGrounded)
            {

                animator.SetFloat("vertical", Input.GetAxis("Vertical"));
                animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));

            }
        }

        private Vector3 lastSentPosition = Vector3.zero;
        private float lastSentRotation = 0.0f;
        private bool endMove = false;

        private void SyncPosition()
        {
            if (Vector3.Distance(lastSentPosition, transform.position) > 0.1f)
            {
                endMove = true;
                SendPosition(false);
                return;
            }

            if (endMove)
            { endMove = false; SendPosition(true); }

            if (Mathf.Abs(Mathf.DeltaAngle(lastSentRotation, transform.rotation.eulerAngles.y)) > 0.5f)
            {

                lastSentRotation = transform.rotation.eulerAngles.y;
                //TODO msg
                //Packet nw = new Packet(Channel.Discard);
                //nw.WriteType(Types.Rotation);
                //nw.WriteFloat(lastSentRotation);

                //NetworkManager.Send(nw);
            }

        }
        private long timeStamp;
        private NetworkManager networkManager;

        private void SendPosition(bool endMove)
        {
            float speed = Vector3.Distance(transform.position.ClearY(), lastSentPosition.ClearY()) / ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - timeStamp) / 1000.0f);
            timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            lastSentPosition = transform.position;
            lastSentRotation = transform.rotation.eulerAngles.y;
            //TODO msg
            //Packet nw = new Packet(endMove ? Channel.Reliable : Channel.Unreliable);
            //nw.WriteType(Types.Move);
            //nw.WriteShort(syncNumber++);
            //nw.WriteVector3(lastSentPosition);
            //nw.WriteFloat(lastSentRotation);
            //nw.WriteBool(endMove);
            //NetworkManager.Send(nw);
        }

        private void OnDestroy()
        {
            networkManager?.UnregisterHandler(Types.TeleportToPoint);
            Stats.Instance.Update -= UpdateStats;
        }
    }
}