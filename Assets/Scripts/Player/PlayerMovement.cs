using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller2D;

    public float runSpeed = 20f;

    private float horizontalVelocity = 0f;
    private bool jump = false;
    private bool crouch = false;


    // Update is called once per frame
    void Update()
    {

        horizontalVelocity = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
        
    }

    void FixedUpdate () {

        controller2D.Move(horizontalVelocity * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
}
