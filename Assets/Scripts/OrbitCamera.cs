using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    Transform focus = default;

    [SerializeField, Range(1f, 20f)]
    float distance = 5f;

    [SerializeField, Min(0f)]
    float focusRadius = 1f;

    Vector3 focusPoint;

    void Awake()
    {
        //Define the position of the object to focus
        focusPoint = focus.position;
    }
    
    void LateUpdate()
    {
        UpdateFocusPoint();
        Vector3 lookDirection = transform.forward;
        transform.localPosition = focusPoint - lookDirection * distance;
    }

    void UpdateFocusPoint()
    {
        //Checks the current position of the object to focus
        Vector3 targetPoint = focus.position;
        //If the radius is superior to 0
        if(focusRadius > 0f)
        {
            //Checks the distance between the camera position and the object to focus
            float distance = Vector3.Distance(targetPoint, focusPoint);
            //If the distance between the 2 position is higher than the focusRadius
            if(distance > focusRadius)
            {
                //Makes the camera travel from the current position to the object to focus, with a fRadius/d ratio;
                focusPoint = Vector3.Lerp(targetPoint, focusPoint, focusRadius / distance);
            }
        } else
        {
            //Else sticks to following perfectly the player
            focusPoint = targetPoint;
        }
    }
}