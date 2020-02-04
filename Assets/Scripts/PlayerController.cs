using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 2f;
    float rotSpeed = 4f;
    float gravity = 8;
    float rot = -90f;
    bool OpenMenu = false;
    
    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        rotSpeed = PlayerPrefs.GetFloat("XSensitivity");
        speed = PlayerPrefs.GetFloat("PlayerSpeed");
    }

    // Update is called once per frame
    void Update()
    {

        bool moving = false;
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moving = true;
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKey(KeyCode.S))
            {
                moving = true;
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(0, 0, -1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKey(KeyCode.A))
            {
                moving = true;
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(-1, 0, 0);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKey(KeyCode.D))
            {
                moving = true;
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(1, 0, 0);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
        }

        if (!camera.GetComponent<CameraControl>().OpenMenu)
        {
            rot += Input.GetAxisRaw("Mouse X") * rotSpeed;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
        else
        {
            rotSpeed = PlayerPrefs.GetFloat("XSensitivity");
            speed = PlayerPrefs.GetFloat("PlayerSpeed");
        }
        if (!moving)
        {
            anim.SetInteger("condition", 0);
            moveDir = new Vector3(0, 0, 0);
        }

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
