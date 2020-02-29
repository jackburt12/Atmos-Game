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

    private GameTime gameTime;

    void Start() {
        gameTime = GameObject.Find("GameManager").GetComponent<GameTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameTime.paused) {
            horizontalVelocity = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump")) {
                jump = true;
            }

            if (Input.GetButtonDown("Crouch")) {
                crouch = true;
            } else if (Input.GetButtonUp("Crouch")) {
                crouch = false;
            }
        } else {
            horizontalVelocity = 0f;
        }
        
        
    }

    void FixedUpdate () {

        controller2D.Move(horizontalVelocity * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
}
