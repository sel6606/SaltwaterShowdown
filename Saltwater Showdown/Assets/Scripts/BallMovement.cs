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
    public float minSpeed;

    public float rightBound;
    public float leftBound;
    public float topBound;
    public float bottomBound;

    public float speedScale;

    private Rigidbody2D ballRB;
    private bool readyToStart;

    //This variable is for debugging purposes (i want to view the values in the inspector)
    public Vector2 velocityDebug;

    // Use this for initialization
    void Start ()
    {
        ballRB = GetComponent<Rigidbody2D>();
        readyToStart = true;
        Reset();
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocityDebug = ballRB.velocity;
        if(readyToStart && Input.GetKey(KeyCode.Space))
        {
            readyToStart = false;
            BeginMovement();
        }

        Vector2 clampedPos = transform.position;

        clampedPos.x = Mathf.Clamp(clampedPos.x, leftBound, rightBound);
        clampedPos.y = Mathf.Clamp(clampedPos.y, bottomBound, topBound);

        transform.position = clampedPos;
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
        ballRB.angularVelocity = 0;
        transform.position = startPos;
        readyToStart = true;
    }

    /// <summary>
    /// This is called when the ball collides with the player
    /// </summary>
    /// <param name="collision">The object we are colliding with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Out"))
        {
            Reset();
        }
        else
        {
            Vector2 newVel;
            newVel.x = ballRB.velocity.x;

            //If we collided with the player, do a calculation to determine the new direction
            if (collision.collider.gameObject.transform.parent.CompareTag("Player"))
            {
                switch (collision.collider.tag)
                {
                    case "Top":
                        if (ballRB.velocity.y <= 0)
                        {
                            newVel.y = ballRB.velocity.y * -1;
                        }
                        else
                        {
                            newVel.y = ballRB.velocity.y;
                        }
                        break;
                    case "Middle":
                        newVel.y = ballRB.velocity.y;
                        break;
                    case "Bottom":
                        if (ballRB.velocity.y >= 0)
                        {
                            newVel.y = ballRB.velocity.y * -1;
                        }
                        else
                        {
                            newVel.y = ballRB.velocity.y;
                        }
                        break;
                    default:
                        newVel = ballRB.velocity;
                        break;
                }

                newVel *= speedScale;
            }
            else
            {
                newVel.y = ballRB.velocity.y;
            }

            newVel.y = (newVel.y <= 0) ? Mathf.Clamp(newVel.y, -maxSpeed, -minSpeed) : Mathf.Clamp(newVel.y, minSpeed, maxSpeed);
            newVel.x = (newVel.x <= 0) ? Mathf.Clamp(newVel.x, -maxSpeed, -minSpeed) : Mathf.Clamp(newVel.x, minSpeed, maxSpeed);

            ballRB.velocity = newVel;
        }
    }
}
