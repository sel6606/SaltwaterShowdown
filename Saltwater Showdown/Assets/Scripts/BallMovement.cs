using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that handles the movement of the ball
/// </summary>
public class BallMovement : MonoBehaviour
{
    public Vector2 startPos;
    public float yForce = -15;
    public float xForce = 20;
    public float maxSpeed;

    private Rigidbody2D ballRB;
    private bool readyToStart;

	// Use this for initialization
	void Start ()
    {
        ballRB = GetComponent<Rigidbody2D>();
        readyToStart = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(readyToStart && Input.GetKey(KeyCode.Space))
        {
            readyToStart = false;
            GameInfo.instance.GameStart = true;
            BeginMovement();
        }
	}

    /// <summary>
    /// Tell the ball to start moving
    /// </summary>
    public void BeginMovement()
    {
        //Pick a random direction
        float rand = Random.Range(0, 2);

        if(rand < 1)
        {
            ballRB.AddForce(new Vector2(xForce, yForce));
        }
        else
        {
            ballRB.AddForce(new Vector2(-xForce, yForce));
        }
    }

    /// <summary>
    /// Reset the ball's velocity and position
    /// </summary>
    public void Reset()
    {
        ballRB.velocity = Vector2.zero;
        transform.position = startPos;
        readyToStart = true;
    }

    /// <summary>
    /// This is called when the ball collides with the player
    /// </summary>
    /// <param name="collision">The object we are colliding with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If we collided with the player, do a calculation to determine the new direction
        if(collision.collider.CompareTag("Player"))
        {
            Vector2 newVel;

            newVel.x = ballRB.velocity.x;
            newVel.y = (ballRB.velocity.y / 2.0f) + (collision.collider.attachedRigidbody.velocity.y / 3.0f);

            newVel.y = Mathf.Clamp(newVel.y, -maxSpeed, maxSpeed);
            ballRB.velocity = newVel;
        }
    }
}
