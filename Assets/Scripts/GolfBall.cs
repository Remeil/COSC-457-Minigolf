using UnityEngine;
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

        lastPosition = gameObject.transform.position;
    }
    
	void Update () {
	    if (GolfGame.Golf.gameState == GameState.ReadyToShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var audio = gameObject.GetComponent<AudioSource>();

                audio.pitch = 1f;
                audio.volume = 1f;

                audio.Play();

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
        else if ((GolfGame.Golf.gameState == GameState.BallInMotion || GolfGame.Golf.gameState == GameState.OverviewCamera) && gameObject.GetComponent<Rigidbody>().IsSleeping())
	    {
	        lastPosition = gameObject.transform.position;
	        GolfGame.BallAtRest();
	    }
        else if (GolfGame.Golf.ballInMotion)
	    {
	        var rigidbody = gameObject.GetComponent<Rigidbody>();

	        if (rigidbody.velocity.magnitude < 1 && rigidbody.velocity.magnitude > Physics.sleepThreshold && Mathf.Abs(rigidbody.velocity.y) < .01)
	        {
	            rigidbody.velocity *= .95f;
	        }

	    }

        if (transform.position.y < -10)
	    {
	        outOfBoundsText.enabled = true;
	        transform.position = lastPosition;
            GetComponent<Rigidbody>().Sleep();
            Invoke("HideText", 3);
	    }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
        {
            var audio = gameObject.GetComponent<AudioSource>();
            audio.pitch = .5f;

            audio.volume = Mathf.Min(gameObject.GetComponent<Rigidbody>().velocity.magnitude * .2f , .8f);

            audio.Play();
        }
    }

    void HideText()
    {
        outOfBoundsText.enabled = false;
    }
}
