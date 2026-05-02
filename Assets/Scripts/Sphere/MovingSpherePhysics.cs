using UnityEngine;
using UnityEngine.InputSystem;

public class MovingSpherePhysics : MonoBehaviour
{

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f, maxAirAcceleration = 1f;

    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    [SerializeField, Range(0, 5)]
    int maxAirJumps = 0;

    [SerializeField, Range(0f, 90f)]
    float maxGroundAngle = 25f;


    Vector3 velocity, desiredVelocity;
    Rigidbody body;
    bool desiredJump;
    bool onGround;
    int jumpPhase;
    float minGroundDotProduct;

    //store the maximum degrees for the ground
	void OnValidate () {
		minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
	}

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        OnValidate();
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }
    void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }
    void EvaluateCollision(Collision collision)
    {
        //Refer to a normal vector for more info
        for (int i = 0; i < collision.contactCount; i++)
        {
            //Checks if the normal y is point up with a 10% acceptation
            Vector3 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= minGroundDotProduct;
        }
    }
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        //The velocity the ball wants to be at (imagine a non velocity character wanting to move)
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        desiredJump |= Input.GetButtonDown("Jump");

    }
    void FixedUpdate()
    {
        UpdateState();
        //Changes the acceleration type depending on if we are touching the ground
        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        //the maximum speed limit when changing speed    
        float maxSpeedChange = acceleration * Time.deltaTime;
        //Change the velocity of x and z to the desired one, with a maximum of maxSpeedChange
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
        //Apply the velocity to the rigidbody
        body.linearVelocity = velocity;
        onGround = false;
    }

    void UpdateState()
    {
        //Get the current velocity of the ball;
        velocity = body.linearVelocity;
        //When on the ground, resets the air jumps
        if (onGround)
        {
            jumpPhase = 0;
        }
    }

    void Jump()
    {
        //Checks if we are on the ground or if we have enough jumps left    
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }
}