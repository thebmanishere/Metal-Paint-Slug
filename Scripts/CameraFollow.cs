//links
//1. https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
//2. https://www.youtube.com/watch?v=Gwc4VCGEuBM
//2. https://www.youtube.com/watch?v=MFQhpwc6cKE


using System.Numerics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
     
    public float bound_x;

    private UnityEngine.Vector3 velocity = UnityEngine.Vector3.zero;
    private UnityEngine.Vector3 delta = UnityEngine.Vector3.zero;

    void LateUpdate()
    {
        float delta_x = target.position.x - transform.position.x;

        if (delta_x > bound_x || delta_x < -bound_x)
        {
            if(transform.position.x < target.position.x)
            {
                delta.x = delta_x - bound_x;
            }
        }

        UnityEngine.Vector3 desiredPosition = transform.position + delta;
        UnityEngine.Vector3 smoothedPosition = UnityEngine.Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

    }

}
