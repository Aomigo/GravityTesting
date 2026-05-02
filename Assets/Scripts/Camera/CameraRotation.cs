using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Camera Rotation Speed")]
    public float rSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    void Update()
    {
        
        Vector3 localUp = CustomGravity.GetUpAxis(transform.position); 
        //The rotation the ball wants to be in to match the gravity
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, localUp) * transform.rotation;
        //The time to get to the wanted rotation, from our base rotation to the wanted one
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rSpeed * Time.deltaTime);
        
    }
}
