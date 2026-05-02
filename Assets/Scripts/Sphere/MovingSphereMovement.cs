using UnityEngine;
using UnityEngine.InputSystem;

public class MovingSphereMovement : MonoBehaviour
{

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    [SerializeField, Range(0f, 1f)]
	float bounciness = 0.5f;


    Vector3 velocity;
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        //Get the acceleration of our inputs
        Vector3 acceleration = new Vector3(playerInput.x, 0.5f, playerInput.y) * maxSpeed;
        //The velocity the ball wants to be at (imagine a non velocity character wanting to move)
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        //the maximum speed limit when changing speed    
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        //Change the velocity of x and y to the desired one, with a maximum of maxSpeedChange;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        /*With Time.deltaTime (The "Slow" Way)
            Time.deltaTime is the duration (in seconds) that the last frame took to complete. At 144 FPS, 
            that value is roughly 0.0069 seconds.
            Calculation: 1.0 unit × 0.0069 seconds × 144 frames=1.0 unit per second.*/
        //Define the displacement of the ball, with the current velocity
        Vector3 displacement = velocity * Time.deltaTime;
        //Define the new position of the ball
        Vector3 newPosition = transform.localPosition + displacement;
        //Changes the position and velocity of the ball if it hits the boundary
        //Same checks as a Contains() and Mathf.Clamp()
        if (newPosition.x < allowedArea.xMin)
        {
            newPosition.x = allowedArea.xMin;
            velocity.x = - velocity.x * bounciness;
        }
        else if (newPosition.x > allowedArea.xMax)
        {
            newPosition.x = allowedArea.xMax;
            velocity.x = - velocity.x * bounciness;
        }
        if (newPosition.z < allowedArea.yMin)
        {
            newPosition.z = allowedArea.yMin;
            velocity.z = - velocity.z * bounciness;
        }
        else if (newPosition.z > allowedArea.yMax)
        {
            newPosition.z = allowedArea.yMax;
            velocity.z = - velocity.z * bounciness;
        }
        //Now get the ball position and apply it to newPosition
        transform.localPosition = newPosition;
    }
}