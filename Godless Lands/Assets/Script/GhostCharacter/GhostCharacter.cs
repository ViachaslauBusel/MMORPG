﻿using Items;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostCharacter : MonoBehaviour, TargetObject {

    public float smooth_rotation = 0.03f;
    private Text text_name;
    private CharacterController character;
    public int ID;

    private Armor armor;
    private Vector3 next_position;
    private Quaternion next_rotation;
    private Animator animator;
    private AnimationSkill animationSkill;

    private float speed = 0.0f;
    private float gravity = 20.0F;
    private Vector3 direction = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool target_point = false;//Точка назначения, если достигнута true
    private bool end_point = true;//Движение в точку где остановился игрок

    private byte indicator;
    private byte moveIndex;

    public void SetStartPosition(Vector3 pos, byte indicator, byte moveIndex)
    {
        animationSkill = GetComponent<AnimationSkill>();
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();

        transform.position = pos;
        next_position = pos;

        this.indicator = indicator;
        this.moveIndex = --moveIndex;

        animator.SetFloat("speedRun", 1.0f);
        animator.SetFloat("vertical", 0.0f);
        animator.SetFloat("horizontal", 0.0f);
    }

    public void SetCombatState(bool state)
    {
        armor.SetCombatstate(state);
    }
    public void SetArmor(NetworkWriter nw)
    {
        armor = GetComponent<Armor>();
        armor.Init();
        while (nw.AvailableBytes >= 8)
        {
            ItemUse part = (ItemUse)nw.ReadInt();
            int id_item = nw.ReadInt();
            Item _item = Inventory.GetItem(id_item);
            armor.PutItem(part, _item);
        }
    }
    public void UpdateArmor(NetworkWriter nw)
    {
        ItemUse part = (ItemUse)nw.ReadInt();
        int id_item = nw.ReadInt();
        Item _item = Inventory.GetItem(id_item);
        armor.PutItem(part, _item);
    }


    public void SetName(string char_name)
    {
        text_name = GetComponentInChildren<Text>();
        text_name.text = char_name;
    }

   /* private void OnDrawGizmos()
    {
        for(int i=0; i<trackServer.Count-1; i++)
        {
            //if(i%2 == 0) Gizmos.color = Color.blue;
            // else 
            Gizmos.color = Color.red;
            Gizmos.DrawLine(trackServer[i], trackServer[i+1]);
        }
        for (int i = 0; i < trackMove.Count - 1; i++)
        {
            //if(i%2 == 0) Gizmos.color = Color.blue;
            // else 
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(trackMove[i], trackMove[i + 1]);
        }
    }*/
  //  private List<Vector3> trackServer = new List<Vector3>();
  //  private List<Vector3> trackMove = new List<Vector3>();
    public void NextPosition(Vector3 _next, byte indicator, byte moveIndex)
    {
        if (this.indicator != indicator) return;//Отбрасываются пакеты отправленные до того как игрок остановился
        int compare = NumberUtils.ByteCompare(moveIndex, this.moveIndex);
        if (compare <= 0) { print("Устаревший пакет отброшен"); return; }
        this.moveIndex = moveIndex;
       
    /*    if (end_point)
        {
            trackServer.Clear();
            trackMove.Clear();
        }
        trackServer.Add(_next);
        trackMove.Add(transform.position);*/
        direction = (_next - transform.position).normalized;
        speed = (_next - transform.position).magnitude * 2.5f * compare;//for 0.4s
        next_position = _next;
        target_point = true;
        end_point = false;
        CalculateAnimation();
       animator.SetFloat("speedRun", speed / 3.5f);

        //Если игрок застрял, телепортироват в точку
        if(Vector3.Distance(transform.position, next_position) > 3.0f)
        {
            character.transform.position = next_position;
        }
    }
    public void EndPosition(Vector3 _next, byte indicator)
    {

        this.indicator = indicator;

        next_position = _next;
        target_point = true;
        end_point = true;
    }

    public void NextRotation(float _next)
    {
        next_rotation = Quaternion.Euler(0.0f, _next, 0.0f);
      
    }

    private void CalculateAnimation()
    {

        Vector3 animDirection = new Vector3(direction.x, 0.0f, direction.z);
        float vertical = 1.0f - ( 2.0f * (Vector3.Angle(animDirection, transform.forward) / 180.0f));
        float horizontal = 1.0f - (2.0f * (Vector3.Angle(animDirection, transform.right) / 180.0f));
        animator.SetFloat("vertical", vertical);
        animator.SetFloat("horizontal", horizontal);
       // print("Speed: " + speed + " vertical: " + vertical + " horizontal: " + horizontal);
    }

    private void Update()
    {
        if (transform.rotation != next_rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, next_rotation, smooth_rotation);
            if(!end_point || target_point) CalculateAnimation();//Если игрок не остановился или не достиг конечной точки, расчитать направление анимации бега
        }

        if (end_point && target_point)//Если игрок остановился и конечная точка не достигнута
        {
            float _distance = Vector3.Distance(transform.position, next_position);
            if (_distance > 0.1f){
          //      print("Dif position = " +_distance);
                direction = (next_position - transform.position).normalized;
                moveDirection = direction * speed * Time.deltaTime;
                //Если новая позиция пересекла точку next_position
                if (_distance < Vector3.Distance(transform.position, transform.position + moveDirection)) { moveDirection = next_position - transform.position; }
                moveDirection.y -= gravity * Time.deltaTime;
                character.Move(moveDirection);
            }
            else
            {
             //   print("End move");
                target_point = false;
                animator.SetFloat("speedRun", 1.0f);
                animator.SetFloat("vertical", 0.0f);
                animator.SetFloat("horizontal", 0.0f);
            }
        }
        if(!end_point)//Если игрок не остановился
        {
            moveDirection = direction * speed * Time.deltaTime;
            moveDirection.y -= gravity * Time.deltaTime;
            character.Move(moveDirection);
        }       
    }


    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void OnTarget()
    {
       
    }

    public void OffTarget()
    {
       
    }

    public string GetName()
    {
        return text_name.text;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public int Layer()
    {
        return 1;
    }

    public int Id()
    {
        return ID;
    }

    public bool IsAlive()
    {
        throw new System.NotImplementedException();
    }
}
