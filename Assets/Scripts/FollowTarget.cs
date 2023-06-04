using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;

    public void FixedUpdate ()
    {
        trackTarget ();
    }

    private void trackTarget ()
    {
        float targetX = target.position.x;
        float targetY = target.position.y;
        float targetZ = transform.position.z;

        transform.position = new Vector3 ( targetX, targetY, targetZ );
    }
}
