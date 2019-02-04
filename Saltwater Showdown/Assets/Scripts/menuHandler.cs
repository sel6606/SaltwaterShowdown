using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuHandler : MonoBehaviour {

    public GameObject OptionsPanel;
    public GameObject HowToPanel;
    public GameObject LevelPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void loadGame()
    {
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }
}
