using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //Private variables
    private Camera cam;
    private Rigidbody playerRB;
    private Transform playerTrans;

    //Public Objects
    public Transform playerCenter;
    public Vector3 cameraOffset;
    public Image healthBar;

    //Public Variables
    public float moveSpeed, jumpForce, timeInAir, maxJumpTime, initialJump;
    public bool canJump, alive = true;

    void Start()
    {
        //Assigning some variables
        cam = Camera.main;
        playerRB = GetComponent<Rigidbody>();
        playerTrans = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "BananaHealthSmall")
            {
                Destroy(other.gameObject);
                healthBar.fillAmount += 0.25f;
            }
            if (other.gameObject.tag == "BananaHealthBig")
            {
                Destroy(other.gameObject);
                healthBar.fillAmount += 0.40f;
            }
        if (other.gameObject.tag == "DeathPlane")
        {

        }
    }

    void Update()
    {
        //Camera offset
        //cam.transform.position = playerCenter.position + cameraOffset;

        RaycastHit hit;
        //Boxcast below the player to check for ground
        if (Physics.BoxCast(playerCenter.position, (playerTrans.localScale / 2) - new Vector3(0.01f, 0.05f, 0), Vector3.down, out hit, playerTrans.rotation, 0.05f) && Input.GetAxis("Jump") == 0)
        {
            if (hit.collider.tag != "Pickups")
            {
                canJump = true;
                timeInAir = 0f;
                playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);
            }
        }

        //Jump Script
        if (Input.GetButton("Jump") && canJump == true && GetComponent<Grapple>().isGrappling == false)
        {
            /*if (timeInAir < maxJumpTime)
            {
                //At the beginning of the jump, set the velocity to make the jump more realistic
                if (timeInAir == 0)
                {
                    playerRB.AddForce(Vector3.up * initialJump, ForceMode.VelocityChange);
                }
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                timeInAir += Time.deltaTime;
            }*/
            //If the player lets go of space the jump stops
            if (timeInAir < maxJumpTime)
            {
                if (timeInAir == 0)
                {
                    playerRB.AddForce(Vector3.up * initialJump, ForceMode.Force);
                }
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Force);
                timeInAir += Time.deltaTime;
            }
            else
            {
                canJump = false;
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            canJump = false;
        }
    }

    //Other input in fixed update
    void FixedUpdate()
    {
        if (alive == true && GetComponent<Grapple>().isGrappling == false)
        {
            YourInput();
        }
    }

    //User input
    void YourInput()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            playerRB.velocity = new Vector3(-moveSpeed, playerRB.velocity.y, 0);
        }

        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            playerRB.velocity = new Vector3(moveSpeed, playerRB.velocity.y, 0);
        }

        else if (GetComponent<Grapple>().isGrappling == false)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x / 1.07f, playerRB.velocity.y, 0);
        }
    }
}