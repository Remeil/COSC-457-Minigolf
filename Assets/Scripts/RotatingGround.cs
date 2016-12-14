using UnityEngine;
using System.Collections;

public class RotatingGround : MonoBehaviour
{
    void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        var rotation = new Vector3();
        rotation.y = GolfGame.Golf.rand.Next(3) * 90;

        gameObject.transform.Rotate(rotation);
    }
}
