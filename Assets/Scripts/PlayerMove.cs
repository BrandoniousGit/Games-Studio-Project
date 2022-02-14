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

    public float moveSpeed, jumpForce, waitTime;

    public bool isGrounded;

    void Start()
    {
        cam = Camera.main;
        playerRB = GetComponent<Rigidbody>();
        playerTrans = GetComponent<Transform>();
    }

    void Update()
    {
        cam.transform.position = playerCenter.position + CameraOffset;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.SphereCast(playerCenter.position, (playerTrans.localScale.x / 2) - 0.05f, -Vector3.up, out hit, 0.1f))
        {
            isGrounded = true;
        }

        //Debug.Log(playerRB.velocity.x);
        if (Input.GetKey("a"))
        {
            playerRB.velocity = new Vector3(-moveSpeed, playerRB.velocity.y, 0);
        }

        else if (Input.GetKey("d"))
        {
            playerRB.velocity = new Vector3(moveSpeed, playerRB.velocity.y, 0);
        }

        else { playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0); }

        if (Input.GetKey("space") && isGrounded == true)
        {
            StartCoroutine("JumpTime");
            Jump();
        }
    }

    void Jump()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce, 0);
    }

    //Allows for controlled jump heights
    IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(waitTime);
        isGrounded = false;
    }
}