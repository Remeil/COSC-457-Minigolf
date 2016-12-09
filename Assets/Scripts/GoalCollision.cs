using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    public Collider ground;

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.gameObject.tag == "golfBall")
        {
            Physics.IgnoreCollision(ground, otherObject, true);
        }
    }

    void OnTriggerExit(Collider otherObject)
    {
        if (otherObject.gameObject.tag == "golfBall")
        {
            Physics.IgnoreCollision(ground, otherObject, false);
        }
    }
}
