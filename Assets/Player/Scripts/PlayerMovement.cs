using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //character controller
    [SerializeField] float playerSpeed = 10f;
    private CharacterController charaCont;

    //camera headbob
    [SerializeField] Animator camAnim;
    private bool isWalking;

    //player movement and gravity
    private Vector3 inputVector;
    private Vector3 movementVector;
    private float gravity = -10;
    private float momentumDamping = 5f;

    void Start()
    {
        charaCont = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        //basic movement
        GetInput();
        MovePlayer();

        //camera headbob
        camAnim.SetBool("isWalking", isWalking);
    }
    void GetInput()
    {
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.A))
        {
            // basic horizontal movement information gathering
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        else
        {
            // creates that iconic slidy movement from doom
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, (Time.deltaTime * momentumDamping));
            isWalking = false;
        }

        //gravity
        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    }
    void MovePlayer()
    {
        charaCont.Move(movementVector * Time.deltaTime);
    }
}
