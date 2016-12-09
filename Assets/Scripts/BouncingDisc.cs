using UnityEngine;
using System.Collections;

public class BouncingDisc : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 firstBouncePoint;
    public Vector3 secondBouncePoint;

    private bool goingToFirst = true;

    void Start()
    {
        firstBouncePoint += transform.position;
        secondBouncePoint += transform.position;
    }
    
    void Update()
    {
        if (goingToFirst && (firstBouncePoint - transform.position).magnitude < moveSpeed)
        {
            goingToFirst = false;
        }
        else if (!goingToFirst && (secondBouncePoint - transform.position).magnitude < moveSpeed)
        {
            goingToFirst = true;
        }

        var distance = Vector3.Normalize((goingToFirst ? firstBouncePoint : secondBouncePoint) - transform.position) * moveSpeed;
        transform.position += distance;
    }
}
