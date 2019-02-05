using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuHandler : MonoBehaviour {

    //UI panels for display
    public GameObject OptionsPanel;
    public GameObject HowToPanel;
    public GameObject LevelPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject gameOverPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !HowToPanel.activeInHierarchy)
        {
            TogglePause();
        }
	}

    /// <summary>
    /// Toggle the display of the otions panel
    /// </summary>
    public void ToggleOptions ()
    {
        if (OptionsPanel.activeInHierarchy)
        {
            OptionsPanel.SetActive(false);
        }
        else
        {
            OptionsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Toggle the display of the How To Play Panel
    /// </summary>
    public void ToggleHowTo()
    {
        if (HowToPanel.activeInHierarchy)
        {
            HowToPanel.SetActive(false);
        }
        else
        {
            HowToPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Toggles the display of the level select panel
    /// </summary>
    public void ToggleLevelSelect()
    {
        if (LevelPanel.activeInHierarchy)
        {
            LevelPanel.SetActive(false);
        }
        else
        {
            LevelPanel.SetActive(true);
        }
    }

    /// <summary>
    /// toggles pause panel displays
    /// </summary>
    public void TogglePause()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(true);
        }
    }

    public void ToggleWin()
    {
        if (winPanel.activeInHierarchy)
        {
            winPanel.SetActive(false);
        }
        else
        {
            winPanel.SetActive(true);
        }
    }

    public void ToggleGameOver()
    {
        if (gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Loads the game scene from the level select
    /// </summary>
    public void loadGame()
    {
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }

    /// <summary>
    /// Button hookup that quits the game scene and returns to the menu scene
    /// </summary>
    public void returnToMenu()
    {
        SceneManager.LoadScene("Menus", LoadSceneMode.Single);
    }
}
