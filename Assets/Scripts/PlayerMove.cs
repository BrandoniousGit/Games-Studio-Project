using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    private Camera cam;
    private Rigidbody playerRB;
    private Transform playerTrans;

    public Transform playerCenter;
    public GameObject arrow;
    public Vector3 CameraOffset;
    public GameObject projectile;

    public float moveSpeed, jumpForce, counter;

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

        Jump();
        ClampVelocity();
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject clone;
            clone = Instantiate(projectile, playerTrans.position, arrow.transform.rotation);

            clone.GetComponent<Rigidbody>().velocity = arrow.transform.TransformDirection(Vector3.up * 5);
            StartCoroutine(removeBullet(clone));
        }
    }

    public void Jump()
    {
        RaycastHit hit;
        if (Physics.SphereCast(playerCenter.position, (playerTrans.localScale.x / 2) - 0.05f, -Vector3.up, out hit, 0.1f) && (Input.GetAxis("Jump") == 0))
        {
            isGrounded = true;
            jumpForce = 5;
        }
        else if (Input.GetAxis("Jump") == 0)
        {
            jumpForce = 0;
        }

        if (Input.GetAxis("Jump") == 1)
        {
            isGrounded = false;
            if (jumpForce > 1.5f)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce, 0);
            }
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
        if (Input.GetAxis("Jump") == 1)
        {
            jumpForce -= 0.15f;
        }
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

    IEnumerator removeBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(100);
        Destroy(bullet);
    }
}