#if UNITY_EDITOR
using OpenWorld;
using OpenWorldEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerForTestMap : MonoBehaviour
{
    public float speed = 5.0f;

    private static CharacterController character;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 20.0F;

    private Animator animator;

    private void Awake()
    {
        /*  GameObject objectMap = GameObject.Find("MapEditor");
          if (objectMap != null)
          {
              MapLoader mapLoader = objectMap.GetComponent<MapLoader>()trs;
              mapLoader.trackingObj = transform;
              mapLoader.DestroyMap();
              mapLoader.LoadMap();
          }*/
        WorldLoader.Map.trackingObj = transform;
    }

    void Start()
    {
 
        
       
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetFloat("speedRun", speed / 3.5f);
    }

    void Update()
    {
      
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

    private void Animation()
    {
        if (character.isGrounded)
        {

            animator.SetFloat("vertical", Input.GetAxis("Vertical"));
            animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));

        }
    }
}
#endif