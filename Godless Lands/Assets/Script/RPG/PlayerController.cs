using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OpenWorld;
using RUCP.Packets;
using RUCP.Network;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public GameObject prefGameLoader;

    private static CharacterController character;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 20.0F;

    private Animator animator;
    private static AnimationSkill animationSkill;

    private int send_cicle = 19;
    private int cicle = 20;

    private GameLoader gameLoader;

    private void Awake()
    {
        enabled = false;
      //  
        HandlersStorage.RegisterHandler(Types.TeleportToPoint, TeleportToPoint);
    }

    private void TeleportToPoint(Packet nw)
    {
        
        transform.position = nw.ReadVector3();
        if (gameLoader != null) Destroy(gameLoader.gameObject);
        gameLoader = Instantiate(prefGameLoader).GetComponent<GameLoader>();
        gameLoader.LoadPoint();
    }

    void Start () {
        animationSkill = GetComponent<AnimationSkill>();
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetFloat("speedRun", speed / 3.5f);
        
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

   /* private void MyCharacter(Packet nw)
    {

        int login_id = nw.ReadInt();

        transform.position = nw.ReadVec3();

        enabled = true;
    }*/



    void Update () {
        if (character.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if ((moveDirection.x + moveDirection.z) > 1.0f)
            {
                moveDirection.Normalize();
            }
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
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
            SendMove();
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

    private Vector3 lastSendingPosition = Vector3.zero;
    private float lastSendRotation = 0.0f;
    private byte indicator = 0;
    private bool endMove = false;
    private byte moveIndex = 0;
    private void SendMove()
    {
        if (Vector3.Distance(lastSendingPosition, transform.position) > 0.1f)
        {
            lastSendingPosition = transform.position;
            lastSendRotation = transform.rotation.eulerAngles.y;
            Packet nw = new Packet(Channel.Unreliable);
            nw.WriteType(Types.Move);
            nw.write(lastSendingPosition);
            nw.WriteFloat(lastSendRotation);
            nw.WriteByte(indicator);
            nw.WriteInt(moveIndex++);
            NetworkManager.Send(nw);
            endMove = true;
            return;
        }
        if (endMove)
        {
            lastSendingPosition = transform.position;
            lastSendRotation = transform.rotation.eulerAngles.y;
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.EndMove);
            nw.write(lastSendingPosition);
            nw.WriteFloat(lastSendRotation);
            nw.WriteInt(++indicator);
            NetworkManager.Send(nw);
            endMove = false;
            return;
        }
        if (Mathf.Abs(Mathf.DeltaAngle(lastSendRotation, transform.rotation.eulerAngles.y)) > 0.5f){

            lastSendRotation = transform.rotation.eulerAngles.y;
            Packet nw = new Packet(Channel.Unreliable);
            nw.WriteType(Types.Rotation);
            nw.WriteFloat(lastSendRotation);

            NetworkManager.Send(nw);
        }
      
    }


    private void OnDestroy()
    {

   //     HandlersStorage.UnregisterHandler(Types.MapEntrance);
        HandlersStorage.UnregisterHandler(Types.TeleportToPoint);
    }
    }
