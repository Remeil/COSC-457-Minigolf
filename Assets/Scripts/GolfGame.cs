using System.Collections.Generic;
using UnityEngine;
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
    public int par;
    public int numberOfHoles;

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
	    SavedData.hole = 1;

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
	    parText.text = "Par: " + par;
	}

    public static void BallShot()
    {
        Golf.gameState = GameState.BallInMotion;
        SavedData.shotsTaken[SavedData.hole - 1]++;
    }

    public static void BallAtRest()
    {
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
                Golf.gameState = GameState.BallInMotion;
                break;
        }
    }

    public static void HoleFinished()
    {
        Golf.gameState = GameState.HoleEnd;
    }
}
