using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject weapon;

    public float speed = 5f;
    public float jumpSpeed = 10f;
    public float gravity = 20f;

    public CharacterController controller;
    public float inputH, inputV;

    private Vector3 moveDirection = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        // Check if space is pressed
        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetActive(true);
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(inputH, 0, inputV);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
