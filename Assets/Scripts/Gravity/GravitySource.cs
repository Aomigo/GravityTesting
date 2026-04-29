using UnityEngine;

public class GravitySource : MonoBehaviour
{
    void OnEnable () {
        CustomGravity.Register(this);
    }

    void OnDisable () {
        CustomGravity.Unregister(this);
    }

    //Virtual only returns THIS object's gravity.
    public virtual Vector3 GetGravity(Vector3 position)
    {
        return Physics.gravity;
    }
}