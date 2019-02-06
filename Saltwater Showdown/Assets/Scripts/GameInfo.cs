using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to keep track of various game information
/// </summary>
public class GameInfo : MonoBehaviour {

    //Represents the game info that is stored across all scenes
    public static GameInfo instance;

    private bool gameStart = false;
    private bool gameOver = false;
    private bool win = false;
    private bool paused = false;

    public bool GameStart
    {
        get { return gameStart; }
        set { gameStart = value; }
    }

    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    public bool Win
    {
        get { return win; }
        set { win = value; }
    }

    public bool Paused
    {
        get { return paused; }
        set { paused = value; }
    }

    void Awake()
    {
        //If there is not already a GameInfo object, set it to this
        if (instance == null)
        {
            //Make sure we keep running when clicking off the screen
            Application.runInBackground = true;

            instance = this;
        }
        else if (instance != this)
        {
            //Ensures that there are no duplicate objects being made every time the scene is loaded
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        gameStart = instance.gameStart;
        gameOver = instance.gameOver;
        paused = instance.paused;
        win = instance.Win;
	}
}
