using UnityEngine;

public class BallDetector : MonoBehaviour
{
    private bool ballDetected = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "golfBall" && !ballDetected)
        {
            ballDetected = true;
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            
            audio.Play();

            GolfGame.HoleFinished();
        }
    }
}
