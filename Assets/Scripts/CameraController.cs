using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float xMargin = 1;
    [SerializeField] private float yMargin = 1;
    [SerializeField] private float xSmooth = 10;
    [SerializeField] private float ySmooth = 10;
    [SerializeField] private Vector2 maxXAndY;
    [SerializeField] private Vector2 minXAndY;
    [SerializeField] private Transform camTarget;

    private bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - camTarget.position.x) > xMargin;
    }
    private bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - camTarget.position.y) > yMargin;
    }

    private void FixedUpdate()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if(CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, camTarget.position.x, xSmooth * Time.deltaTime);
        }

        if(CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, camTarget.position.y, ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
