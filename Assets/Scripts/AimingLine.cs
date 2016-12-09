using UnityEngine;
using UnityEngine.UI;

public class AimingLine : MonoBehaviour
{
    public GameObject gameCamera;
    private LineRenderer line;

    void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        line.SetVertexCount(2);
        line.SetPosition(0, gameObject.transform.position);
    }

	void Update () {
	    if (GolfGame.Golf.gameState == GameState.ReadyToShoot)
	    {
	        line.enabled = true;
	        var cameraAngle = gameObject.transform.position - gameCamera.transform.position;

	        cameraAngle.y = 0;
	        var lineEndPoint = Vector3.Normalize(cameraAngle);
	        lineEndPoint *= (GetComponent<GolfBall>().puttStrength / 10.0f);

            line.SetPosition(0, gameObject.transform.position);
	        line.SetPosition(1, gameObject.transform.position + lineEndPoint);
	    }
	    else
	    {
	        line.enabled = false;
	    }
	}
}
