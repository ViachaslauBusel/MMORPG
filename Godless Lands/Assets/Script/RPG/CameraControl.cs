using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {


    public Transform character;
    public float speed_rotation = 100.0f;

    private Vector3 rotation;

    private void Start()
    {
        if (!character) character = transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetButton("MouseRight"))
        {
            if (Input.GetButton("MouseLeft"))
            {
                rotation = transform.localRotation.eulerAngles;
                rotation.x -= (Input.GetAxis("Mouse Y") * speed_rotation) * Time.deltaTime;
                if (rotation.x < 300.0f && rotation.x >= 200.0f) rotation.x = 300.0f;
                else if (rotation.x > 65.0f && rotation.x < 200.0f) rotation.x = 65.0f;

                rotation.y += (Input.GetAxis("Mouse X") * speed_rotation) * Time.deltaTime;
                //print(rotation);

                transform.localRotation = Quaternion.Euler(rotation);
            }
            else
            {
                /* if (Input.GetButton("Jump"))
                 {
                     transform.localRotation = Quaternion.Euler(Vector3.zero);
                 }*/
                rotation = character.rotation.eulerAngles;
                rotation.y += (Input.GetAxis("Mouse X") * speed_rotation) * Time.deltaTime;
                character.rotation = Quaternion.Euler(rotation);


                rotation = transform.localRotation.eulerAngles;
                rotation.x -= (Input.GetAxis("Mouse Y") * speed_rotation) * Time.deltaTime;
                if (rotation.x < 300.0f && rotation.x >= 200.0f) rotation.x = 300.0f;
                else if (rotation.x > 65.0f && rotation.x < 200.0f) rotation.x = 65.0f;

                transform.localRotation = Quaternion.Euler(rotation);
            }
        }

            //Возврат камеры за спину
            rotation = transform.localRotation.eulerAngles;
            if (rotation.y != 0.0f && !Input.GetButton("MouseLeft"))
            {
                if (rotation.y > 160.0f)
                {
                    rotation.y += speed_rotation * Time.deltaTime;
                    if (rotation.y > 360.0f) rotation.y = 0.0f;
                }
                else
                {
                    rotation.y -= speed_rotation * Time.deltaTime;
                    if (rotation.y < 0.0f) rotation.y = 0.0f;
                }
                transform.localRotation = Quaternion.Euler(rotation);
            }
        
    }
}
