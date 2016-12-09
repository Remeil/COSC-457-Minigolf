using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject pointOfInterest;
    public Vector3 offset;
    public Vector3 overviewPosition;
    public Vector3 overviewRotation;
    public float sensitivity = 1f;

    private Quaternion lastBallRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GolfGame.ToggleCamera();
            ToggleCameraMode();
        }
        else if (GolfGame.Golf.gameState == GameState.ReadyToShoot || GolfGame.Golf.gameState == GameState.BallInMotion)
        {
            var newPosition = pointOfInterest.transform.position + offset;
            gameObject.transform.position = newPosition;

            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");

            gameObject.transform.RotateAround(pointOfInterest.transform.position, Vector3.up, sensitivity * mouseX);

            //we use transform.right here to get rotate around the camera's local right axis, instead of the world axis.
            gameObject.transform.RotateAround(pointOfInterest.transform.position, transform.right, sensitivity * mouseY);

            offset = gameObject.transform.position - pointOfInterest.transform.position;
            lastBallRotation = gameObject.transform.rotation;
        }
    }

    private void ToggleCameraMode()
    {
        if (GolfGame.Golf.gameState == GameState.OverviewCamera)
        {
            gameObject.transform.position = overviewPosition;
            gameObject.transform.rotation = Quaternion.Euler(overviewRotation);
        }
        else if (GolfGame.Golf.gameState != GameState.HoleEnd)
        {
            var newPosition = pointOfInterest.transform.position + offset;
            gameObject.transform.position = newPosition;

            gameObject.transform.rotation = lastBallRotation;
        }
    }
}
