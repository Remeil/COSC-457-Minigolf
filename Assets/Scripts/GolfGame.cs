using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    ReadyToShoot,
    BallInMotion,
    HoleEnd,
    OverviewCamera
}

public class GolfGame : MonoBehaviour
{
    public static GolfGame Golf;
    public GameState gameState { get; private set; }
    public System.Random rand { get; private set; }
    public bool ballInMotion { get; private set; }
    public int[] par;
    public int numberOfHoles;
    public string nextScene;

    public Text shotsTakenText;
    public Text parText;
    public Text holeText;
    public Text holeScoreText;

    private Dictionary<int, string> shotScoreNames;

    // Use this for initialization
    void Start ()
	{
	    gameState = GameState.ReadyToShoot;
	    Golf = this;

	    SavedData.SetNumberOfHoles(numberOfHoles);
	    SavedData.SetPar(par);

        shotScoreNames = new Dictionary<int, string>
        {
            {-4, "Condor" },
            {-3, "Albatross" },
            {-2, "Eagle" },
            {-1, "Birdie" },
            {0, "Par" },
            {1, "Bogey" },
            {2, "Double Bogey" },
            {3, "Triple Bogey" }
        };

        rand = new System.Random();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    switch (Golf.gameState)
	    {
	        case GameState.BallInMotion:
	        case GameState.ReadyToShoot:
	            Cursor.visible = false;
	            Cursor.lockState = CursorLockMode.Locked;
	            break;
	        case GameState.OverviewCamera:
	        case GameState.HoleEnd:
	            Cursor.visible = true;
	            Cursor.lockState = CursorLockMode.None;
	            break;
	    }
        
        holeText.text = "Hole " + SavedData.hole + " of " + numberOfHoles;
	    shotsTakenText.text = "Shots Taken: " + SavedData.shotsTaken[SavedData.hole - 1];
	    parText.text = "Par: " + SavedData.par[SavedData.hole - 1];

	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        Golf.gameState = GameState.HoleEnd;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Scoreboard");
	    }
	}

    public static void BallShot()
    {
        Golf.gameState = GameState.BallInMotion;
        Golf.ballInMotion = true;
        SavedData.shotsTaken[SavedData.hole - 1]++;
    }

    public static void BallAtRest()
    {
        Golf.ballInMotion = false;
        if (Golf.gameState == GameState.BallInMotion)
        {
            Golf.gameState = GameState.ReadyToShoot;
        }
    }

    public static void ToggleCamera()
    {
        switch (Golf.gameState)
        {
            case GameState.ReadyToShoot:
            case GameState.BallInMotion:
                Golf.gameState = GameState.OverviewCamera;
                break;
            case GameState.OverviewCamera:
                if (Golf.ballInMotion)
                {
                    Golf.gameState = GameState.BallInMotion;
                }
                else
                {
                    Golf.gameState = GameState.ReadyToShoot;
                }
                break;
        }
    }

    public static void HoleFinished()
    {
        Golf.gameState = GameState.HoleEnd;

        var holeScore = "";
        if (Golf.shotScoreNames.Keys.Contains(SavedData.shotsTaken[SavedData.hole - 1] - SavedData.par[SavedData.hole - 1]))
        {
            holeScore = Golf.shotScoreNames[SavedData.shotsTaken[SavedData.hole - 1] - SavedData.par[SavedData.hole - 1]];
        }
        else
        {
            holeScore = "+" + (SavedData.shotsTaken[SavedData.hole - 1] - SavedData.par[SavedData.hole - 1]);
        }

        Golf.holeScoreText.enabled = true;
        Golf.holeScoreText.text = holeScore;

        Golf.Invoke("LoadHole", 3);
    }

    public void LoadHole()
    {
        SavedData.hole++;
        SceneManager.LoadScene(nextScene);
    }
}
