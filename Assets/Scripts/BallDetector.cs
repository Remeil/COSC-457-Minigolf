using UnityEngine;

public class BallDetector : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "golfBall")
        {
            GolfGame.HoleFinished();
        }
    }
}
