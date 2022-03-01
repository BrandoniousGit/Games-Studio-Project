using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Camera cam;
    private Rigidbody playerRB;
    private Transform playerTrans;

    public Transform playerCenter;
    public Vector3 CameraOffset;

    public float moveSpeed, jumpForce, timer;

    public bool isGrounded, alive = true;

    void Start()
    {
        cam = Camera.main;
        playerRB = GetComponent<Rigidbody>();
        playerTrans = GetComponent<Transform>();
    }

    void Update()
    {
        cam.transform.position = playerCenter.position + CameraOffset;

        RaycastHit hit;
        if (Physics.SphereCast(playerCenter.position, (playerTrans.localScale.x / 2) - 0.05f, -Vector3.up, out hit, 0.1f))
        {
            isGrounded = true;
            jumpForce = 5;
        }

        ClampVelocity();

        if (Input.GetAxis("Jump") == 1)
        {
            isGrounded = false;
            jumpForce -= 0.06f;
            if (jumpForce > 1.5f)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce, 0);
            }
        }
        else if(Input.GetAxis("Jump") == 0)
        {
            jumpForce = 0;
        }
    }

    public void ClampVelocity()
    {
        if (playerRB.velocity.y < -10.0f)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, -10.0f, 0);
        }
    }

    void FixedUpdate()
    {
        if (alive == true)
        {
            YourInput();
        }
    }

    void YourInput()
    {
        //Debug.Log(playerRB.velocity.x);
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            playerRB.velocity = new Vector3(-moveSpeed, playerRB.velocity.y, 0);
        }

        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            playerRB.velocity = new Vector3(moveSpeed, playerRB.velocity.y, 0);
        }

        else { playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0); }
    }
}