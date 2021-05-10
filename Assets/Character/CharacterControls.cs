using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] enum SIDE {Left, Mid, Right}

public class CharacterControls : MonoBehaviour
{

    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField]  private float jumpHeight = 5f;

    private float gravity = -25f;
    private CharacterController characterController;
    private Vector3 velocity;
    private Transform groundCheck;
    private bool isGrounded;
    private float horizontalInput;
    private SIDE sideController  = SIDE.Mid;

    [HideInInspector] private bool SwipeLeft;
    [HideInInspector] private bool SwipeRight;

    private float XValue;
    private float NewXPosition = 0f;
    private float x;
    private float SpeedDodge = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        groundCheck.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 1;

        //Face towards 
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        // isGrounded   
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayers);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else 
        {
            // Add gravity 
            velocity.y += gravity * Time.deltaTime;
        }
        
        characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);

        // Jump 
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2  * gravity);
        }

        if ()

        // Left-right movement 
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);

        if(SwipeLeft)
        {
            if(sideController == SIDE.Mid)
            {
                NewXPosition = -XValue;
                sideController = SIDE.Left;
            }
            else if (sideController == SIDE.Right)
            {
                NewXPosition = 0;
                sideController = SIDE.Mid;
            }
        }
        else if(SwipeRight)
        {
            if(sideController == SIDE.Mid)
            {
                NewXPosition = XValue;
                sideController = SIDE.Right;
            }
            else if (sideController == SIDE.Left)
            {
                NewXPosition = 0;
                sideController = SIDE.Mid;
            }
        }

        x = Mathf.Lerp(x, NewXPosition, Time.deltaTime * SpeedDodge);

        characterController.Move((x - transform.position.x) * Vector3.right);

        // Vertical Velocity 
        characterController.Move(velocity * Time.deltaTime);
    }
}
