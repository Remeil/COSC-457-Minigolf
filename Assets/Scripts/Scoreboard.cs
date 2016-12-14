using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Text scoreboardText;

    void Start()
    {
        var holes = SavedData.par.Length;
        var score = SavedData.shotsTaken;
        var par = SavedData.par;

        //var holes = 4;
        //var score = new int[] {1, 2, 3, 4};
        //var par = new int[] {4, 3, 2, 1};

        var holesString = "Hole  |";
        var scoreString = "Score |";
        var parString =   "Par   |";

        int i;
        for (i = 0; i < holes; i++)
        {
            holesString += String.Format("{0,2} ", i+1);
            scoreString += String.Format("{0,2} ", score[i]);
            parString += String.Format("{0,2} ", par[i]);
        }

        holesString += "| Overall";
        scoreString += "| " + score.Sum();
        parString += "| " + par.Sum();

        scoreboardText.text = holesString + Environment.NewLine + parString + Environment.NewLine + scoreString;
    }

    public void RestartGame()
    {
        SavedData.par = null;
        SavedData.hole = 1;
        SavedData.shotsTaken = null;

        SceneManager.LoadScene("Hole_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
