using UnityEngine;
using System.Collections;

public class BoardRandomizer : MonoBehaviour
{
    private bool ballWasInMotion = true;

	void FixedUpdate () {
	    if (ballWasInMotion && !GolfGame.Golf.ballInMotion)
	    {
            foreach (Transform ground in gameObject.transform)
	        {
                ground
	            ground.gameObject.GetComponent<RotatingGround>().Rotate();
	        }
	    }

	    ballWasInMotion = GolfGame.Golf.ballInMotion;
	}
}
