using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;
    public float sprintSpeed;

    [Header("Jump Settings")]
    public float jumpForce = 2.0f;
    public bool isGrounded;

    
    Rigidbody rb;
    private float moveDirection;
    private float yRotation = 0f;
    private int sensitivity = 30;

    void Start()
    {
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }
    //To stay on the ground || NEED TO MAKE SURE IM TOUCHING A FLOOR LAYER ONCOLISSIONENTER(COLLIDER FLOOR)
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        //move around the x axis
        //REMOVED FOR THE ORBIT CAMERA
        /*Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        if (mouseDelta.magnitude > 0)
        {
            yRotation += mouseDelta.x * sensitivity * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
            Debug.Log(transform.localRotation.y);
        }*/





        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        //Use the value of "sprintSpeed" if left-shift is held down, otherwise use the value of "moveSpeed";
        float speed = keyboard.leftShiftKey.isPressed ? sprintSpeed : moveSpeed;

        // 1. Get input values (returns -1, 0, or 1)
        float x = 0;
        float z = 0;

        if (keyboard.wKey.isPressed) z = 1f;
        if (keyboard.sKey.isPressed) z = -1f;
        if (keyboard.aKey.isPressed) x = -1f;
        if (keyboard.dKey.isPressed) x = 1f;

        // 2. Define the moveDirection
        Vector3 moveDirection = new Vector3(x, 0, z);
        //Update the GameObject's position with the detected move direction and speed.
        transform.Translate(moveDirection * speed * Time.deltaTime);
        /* Not bad rb.AddForce(moveDirection * (speed / 10), ForceMode.Impulse);*/

        //To jump
        if(keyboard.spaceKey.wasPressedThisFrame && isGrounded)
        {
            //Get the local upAxis
            Vector3 upAxis = CustomGravity.GetUpAxis(rb.position);

            rb.AddForce(upAxis * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }


    }

    void FixedUpdate()
{
    //Get the gravity from your custom system based on the ball's position to manually apply it after
    Vector3 gravity = CustomGravity.GetGravity(rb.position);

    //ForceMode.Acceleration simulates real gravity
    rb.AddForce(gravity, ForceMode.Acceleration);
}
}
