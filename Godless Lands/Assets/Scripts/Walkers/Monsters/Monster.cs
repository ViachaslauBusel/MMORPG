
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Walkers;

namespace Monsters
{
    public class Monster : MonoBehaviour, ITargetObject
    {

        public int ID;

        private Animator animator;
        private bool alive = true;
        private MovementController ghostMove;

        private Text name_txt;


        public void StartMonster()
        {
            animator = GetComponent<Animator>();
            ghostMove = GetComponent<MovementController>();
        }

        public MovementController controller()
        {
            return ghostMove;
        }

        public void Dead()
        {
            gameObject.layer = 10;//DeadMonster
            alive = false;
        }

        public bool IsAlive()
        {
            return alive;
        }

        public void SetName(string _name)
        {
            name_txt = transform.Find("TextInfo/Canvas/Text").GetComponent<Text>();
            name_txt.text = _name;
        }

        public string GetName()
        {
            return name_txt.text;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void OffTarget()
        {
            throw new System.NotImplementedException();
        }

        public void OnTarget()
        {
            throw new System.NotImplementedException();
        }

        public void SkillAnim(int anim, ITargetObject target)
        {
            if (!alive) return;
            if (target != null)
            {
                Quaternion quat = Quaternion.LookRotation(target.GetTransform().position - transform.position);
                transform.rotation = Quaternion.Euler(0.0f, quat.eulerAngles.y, 0.0f);
            }
            if (anim == 1)
            {
                Dead();
                animator.SetTrigger("dead");
            }
            else if (anim == 2) animator.SetTrigger("hit");
            else if(anim == 3) animator.SetTrigger("idle");

        }

        public Transform GetTransform()
        {
            return transform;
        }

        public int Layer()
        {
            return 2;
        }

        public int Id()
        {
            return ID;
        }
    }
}
