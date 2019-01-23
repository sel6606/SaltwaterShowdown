using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that handles player controls
/// </summary>
public class PlayerControls : MonoBehaviour
{

    public Rigidbody2D player1;
    public Rigidbody2D player2;

    public float speed = 10.0f;
    public float maxY = 2.25f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 velocityP1 = player1.velocity;
        Vector2 velocityP2 = player2.velocity;

        //Determine if player one is moving
        if(Input.GetAxis("Vertical") != 0)
        {
            velocityP1.y = (Input.GetAxis("Vertical") < 0 ? -speed : speed); 
        }
        else
        {
            velocityP1.y = 0;
        }

        //Determine if player 2 is moving
        if (Input.GetAxis("Vertical2") != 0)
        {
            velocityP2.y = (Input.GetAxis("Vertical2") < 0 ? -speed : speed);
        }
        else
        {
            velocityP2.y = 0;
        }

        player1.velocity = velocityP1;
        player2.velocity = velocityP2;

        CheckBounds(player1);
        CheckBounds(player2);
	}


    /// <summary>
    /// Make sure the player stays in bounds
    /// </summary>
    /// <param name="player">The player to check</param>
    private void CheckBounds(Rigidbody2D player)
    {
        Vector2 pos = player.transform.position;

        if (pos.y > maxY)
        {
            pos.y = maxY;
        }
        else if (pos.y < -maxY)
        {
            pos.y = -maxY;
        }

        player.transform.position = pos;
    }
}
