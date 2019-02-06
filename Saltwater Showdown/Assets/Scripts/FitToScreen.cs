using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitToScreen : MonoBehaviour {

    //Player Variables
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject urchin;

    //AI Variables
    public GameObject ai;
    public GameObject[] aiPositions;

    //Bounds Variables
    public GameObject[] bounds;
    public GameObject background;

	// Use this for initialization
	void Start () {
        ScaleObjects();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    /// <summary>
    /// Scales all the game objects according to the screen dimensions
    /// </summary>
    private void ScaleObjects()
    {
        //Get the orthographic screen width and height
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        //Scale and position the AI
        ai.transform.localScale = Vector3.one * width / height / 2.0f;
        ai.transform.position = new Vector3(0.0f, 0.0f, 1.0f);

        //Variable to keep track of the row for the positions
        int row = 1;

        //Scale and position the AI positions
        for (int i = 0; i < aiPositions.Length; i++)
        {
            float yOffset = 0;

            //Go to the next row
            if (i != 0 && i % 3 == 0)
            {
                row++;
            }

            //Set the y position 
            //Note: Dividing by 2 to get the half width of the half width (since it works well)
            if (row == 1)
            {
                yOffset = Camera.main.orthographicSize - Camera.main.orthographicSize / 2.0f;
            }
            else if (row == 2)
            {
                yOffset = 0;
            }
            else if (row == 3)
            {
                yOffset = -Camera.main.orthographicSize + Camera.main.orthographicSize / 2.0f;
            }

            //Set the x position and place the AI position
            //Note: divinding by 2 to get the half width; dividing by 3 just because it works well
            if (i % 3 == 0)
            {
                float xOffset = -width / 2.0f + width / 3.0f;
                aiPositions[i].transform.position = new Vector3(xOffset, yOffset, 1.0f);
            }
            else if (i % 3 == 1)
            {
                float xOffset = 0;
                aiPositions[i].transform.position = new Vector3(xOffset, yOffset, 1.0f);
            }
            else if (i % 3 == 2)
            {
                float xOffset = width / 2.0f - width / 3.0f;
                aiPositions[i].transform.position = new Vector3(xOffset, yOffset, 1.0f);
            }
        }

        //Scale and position the players
        playerOne.transform.localScale = Vector3.one * width / height / 2.0f;
        playerOne.transform.position = new Vector3(width / 2.0f - playerOne.GetComponent<BoxCollider2D>().bounds.extents.x * 2.0f, 0.0f, 1.0f);

        playerTwo.transform.localScale = Vector3.one * width / height / 2.0f;
        playerTwo.transform.position = new Vector3(-width / 2.0f + playerTwo.GetComponent<BoxCollider2D>().bounds.extents.x * 2.0f, 0.0f, 1.0f);

        urchin.transform.localScale = Vector3.one * width / height / 2.0f;
        urchin.transform.position = new Vector3(playerOne.transform.position.x - playerOne.GetComponent<BoxCollider2D>().bounds.extents.x * 2.0f, 0.0f, 1.0f);

        //Scale and position the bounds
        for (int i = 0; i < bounds.Length; i++)
        {
            if (bounds[i].name == "TopBound")
            {
                bounds[i].transform.localScale = new Vector3(width * 2.0f, 1.0f, 1.0f);
                bounds[i].transform.position = new Vector3(0.0f, height / 2.0f + bounds[i].transform.localScale.y / 2.0f, 1.0f);
            }
            else if (bounds[i].name == "BottomBound")
            {
                bounds[i].transform.localScale = new Vector3(width * 2.0f, 1.0f, 1.0f);
                bounds[i].transform.position = new Vector3(0.0f, -height / 2.0f - bounds[i].transform.localScale.y / 2.0f, 1.0f);
            }
            else if (bounds[i].name == "LeftBound")
            {
                bounds[i].transform.localScale = new Vector3(1.0f, height * 2.0f, 1.0f);
                bounds[i].transform.position = new Vector3(-width / 2.0f - bounds[i].transform.localScale.x / 2.0f, 0.0f, 1.0f);
            }
            else if (bounds[i].name == "RightBound")
            {
                bounds[i].transform.localScale = new Vector3(1.0f, height * 2.0f, 1.0f);
                bounds[i].transform.position = new Vector3(width / 2.0f + bounds[i].transform.localScale.x / 2.0f, 0.0f, 1.0f);
            }
        }
    }
}
