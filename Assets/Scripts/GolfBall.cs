﻿using UnityEngine;
using UnityEngine.UI;

public class GolfBall : MonoBehaviour
{
    public GameObject gameCamera;
    public float ballPuttForce = 10f;
    public Text outOfBoundsText;
    public Text shotStrengthText;

    public int puttStrength { get; private set; }

    private Vector3 lastPosition;

    public const int PUTT_STRENGTH_MIN = 1;
    public const int PUTT_STRENGTH_MAX = 20;

    void Start()
    {
        puttStrength = 10;
        GetComponent<Rigidbody>().maxAngularVelocity = 100;
        lastPosition.y = .05f;
    }
    
	void Update () {
	    if (GolfGame.Golf.gameState == GameState.ReadyToShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var cameraAngle = gameObject.transform.position - gameCamera.transform.position;

                cameraAngle.y = 0;

                var ballForce = Vector3.Normalize(cameraAngle);
                ballForce *= ballPuttForce * puttStrength;

                gameObject.GetComponent<Rigidbody>().AddForce(ballForce);

                GolfGame.BallShot();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 || 
                     Input.GetKeyDown(KeyCode.UpArrow))
            {
                puttStrength++;

                if (puttStrength > PUTT_STRENGTH_MAX)
                {
                    puttStrength = PUTT_STRENGTH_MAX;
                }

                shotStrengthText.text = "Shot Strength: " + ((puttStrength * 100) / PUTT_STRENGTH_MAX) + "%";
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 ||
                     Input.GetKeyDown(KeyCode.DownArrow))
            {
                puttStrength--;

                if (puttStrength < PUTT_STRENGTH_MIN)
                {
                    puttStrength = PUTT_STRENGTH_MIN;
                }

                shotStrengthText.text = "Shot Strength: " + ((puttStrength * 100) / PUTT_STRENGTH_MAX) + "%";
            }
        }
        else if (GolfGame.Golf.gameState == GameState.BallInMotion && gameObject.GetComponent<Rigidbody>().IsSleeping())
	    {
	        lastPosition = gameObject.transform.position;
	        GolfGame.BallAtRest();
	    }
        else if (transform.position.y < -10)
	    {
	        outOfBoundsText.enabled = true;
	        transform.position = lastPosition;
            GetComponent<Rigidbody>().Sleep();
            Invoke("HideText", 3);
	    }
	}

    void HideText()
    {
        outOfBoundsText.enabled = false;
    }
}
