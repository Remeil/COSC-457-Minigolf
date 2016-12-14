using UnityEngine;
using System.Collections;

public class BouncePeg : MonoBehaviour
{
    public float soundCooldown = 2f;
    public float volume = 1f;

    private bool soundOffCooldown = true;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "golfBall")
        {
            var golfBallSpeed = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

            if (soundOffCooldown)
            {
                soundOffCooldown = false;

                audioSource.volume = Mathf.Min(golfBallSpeed * volume, 1);
                audioSource.Play();

                Invoke("ResetSound", soundCooldown);
            }
        }
    }

    void ResetSound()
    {
        soundOffCooldown = true;
    }
}
