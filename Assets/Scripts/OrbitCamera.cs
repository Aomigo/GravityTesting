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

    [SerializeField, Range(0f, 1f)]
    float focusCentering = 0.5f;


    Vector3 focusPoint;
    //Define the base orbital rotation : around x and around y
    Vector2 orbitAngles = new Vector2(45f, 0f);

    void Awake()
    {
        //Define the position of the object to focus, which become the current camera position
        focusPoint = focus.position;
        //base initial rotation
        transform.localRotation = Quaternion.Euler(orbitAngles);
    }


    void LateUpdate()
    {
        //Updates the camera position
        UpdateFocusPoint();
        //Get the direction the camera is looking at
        Vector3 lookDirection = transform.forward;
        //change the position of the camera
        transform.localPosition = focusPoint - lookDirection * distance;
    }

    void UpdateFocusPoint()
    {
        //Checks the current position of the object to focus
        Vector3 targetPoint = focus.position;
        //If the radius is superior to 0
        if (focusRadius > 0f)
        {
            //Checks the distance between the object to focus and the camera position 
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            //If the camera isnt exactly on the focused object
            if (distance > 0.001f && focusCentering > 0f)
            {
                //Keeps the camera 1f - focusCentering % of the way every realLife second
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }
            //If the distance between the 2 position is higher than the focusRadius
            if (distance > focusRadius)
            {
                //Gets the minimum value between t and fRadius/dist;
                t = Mathf.Min(t, focusRadius / distance);
            }
            //Makes the camera travel from the current position to the object to focus, with the minimum value travel time;
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            //Else sticks to following perfectly the player
            focusPoint = targetPoint;
        }
    }
}