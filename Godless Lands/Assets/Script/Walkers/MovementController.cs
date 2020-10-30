using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Walkers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] float smooth_rotation = 0.03f;
        private CharacterController character;


        private Vector3 destination;
        private Quaternion rotation;
        private Animator animator;

        private float speed = 0.0f;
        private float gravity = 20.0F;
        private Vector3 direction = Vector3.zero;
        private Vector3 step = Vector3.zero;
        private bool target_point = false;//Точка назначения, если достигнута true
        private bool endMove = true;//Движение в точку где остановился игрок


        public void SetStartPosition(Vector3 position)
        {
            animator = GetComponent<Animator>();
            character = GetComponent<CharacterController>();

            transform.position = destination = position;



            animator.SetFloat("speedRun", 1.0f);
            animator.SetFloat("vertical", 0.0f);
            animator.SetFloat("horizontal", 0.0f);
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
        public void SyncPosition(Vector3 destination, float speed, bool endMove)
        {

            direction = (destination - transform.position).normalized;
            this.speed = speed;
            this.destination = destination;
            target_point = true;
            this.endMove = endMove;

            animator.SetFloat("speedRun", speed / 2.0f);
        }

        public void SyncRotation(float _next)
        {
            rotation = Quaternion.Euler(0.0f, _next, 0.0f);

        }

        private void CalculateAnimation()
        {

            Vector3 animDirection = new Vector3(direction.x, 0.0f, direction.z).normalized;
            float vertical = 1.0f - (Vector3.Angle(animDirection, transform.forward) / 90.0f);
            float horizontal = 1.0f - (Vector3.Angle(animDirection, transform.right) / 90.0f);
            animator.SetFloat("vertical", vertical);
            animator.SetFloat("horizontal", horizontal);
            // print("Speed: " + speed + " vertical: " + vertical + " horizontal: " + horizontal);
        }

        private void Update()
        {
            if (transform.rotation != rotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smooth_rotation);
            }
            if (!endMove || target_point) CalculateAnimation();//Если игрок не остановился или не достиг конечной точки, расчитать направление анимации бега

            if (endMove && target_point)//Если игрок остановился и конечная точка не достигнута
            {
                Vector3 dis = transform.position - destination;
                dis.y = 0.0f;
                float distance = dis.magnitude;
                if (distance > 0.1f)
                {
                    //      print("Dif position = " +_distance);
                    direction = (destination - transform.position).normalized;
                    step = direction * speed * Time.deltaTime;
                    //Если новая позиция пересекла точку next_position
                    if (distance < Vector3.Distance(transform.position, transform.position + step)) { step = destination - transform.position; EndMove(); }
                    step.y -= gravity * Time.deltaTime;
                    character.Move(step);
                    //   print("move end point: "+speed);
                }
                else
                {
                    EndMove();
                }
            }
            if (!endMove)//Если игрок не остановился
            {
                step = direction * speed * Time.deltaTime;
                step.y -= gravity * Time.deltaTime;
                character.Move(step);
            }
        }

        private void EndMove()
        {
            //   print("End move");
            target_point = false;
           // animator.SetFloat("speedRun", 1.0f);
            animator.SetFloat("vertical", 0.0f);
            animator.SetFloat("horizontal", 0.0f);
        }


    }
}