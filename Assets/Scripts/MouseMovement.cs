using System;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 400f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Cursor lock onto the middle and invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        //Getting mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Looking up n down
        xRotation -= mouseY;

        //Looking left right
        yRotation += mouseX;

        //Clamp
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Applying rotations
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}