using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 1.5f;
    [SerializeField] float smoothing = 1.5f;

    private float xMousePos;
    private float smoothedMousePos;

    private float currentLookingPos;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }
    private void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
    }

    private void ModifyInput()
    {
        xMousePos *= sensitivity * smoothing;
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f/smoothing);
    }

    private void MovePlayer()
    {
        currentLookingPos += smoothedMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }



}
