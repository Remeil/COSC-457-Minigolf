using UnityEngine;
using System.Collections;

public class WindmillRotator : MonoBehaviour
{
    public float rotationSpeed = 180f;

    public void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
