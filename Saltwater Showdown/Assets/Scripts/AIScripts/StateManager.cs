using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles switching between different states for the AI.
/// Contains variables specific to each AI.
/// </summary>
public class StateManager : MonoBehaviour {

    //Variables to handle states
    public State currentState;
    public State previousState;

    //Variables to hold different sprites
    public Sprite normal;
    public Sprite defense;

    //Variables for damaging the AI
    public float health;
    public GameObject[] weakPoints;
    public GameObject[] weakPointPositions;

    public float blinkTime;
    public float blinkTimer;
    public float blinkInterval;
    public float blinkIntervalTimer;
    public bool animatingDamage;

    //Variables for attacking
    public GameObject bullet;

    //Variables for Normal state
    public float idleTime;
    public float idleTimer;
    public bool isMoving;

    public GameObject[] normalSpotsToMove;
    public int currentSpot;
    public int nextSpot;
    public float movePercent;

    //Variables for transitioning to defensive state
    public bool enteringDefense;
    public ParticleSystem defenseMask;
    public int numDefensiveLights;

    //Variables for Defense state
    public GameObject[] defenseSpotsToMove;
    public int currDefSpot;
    public int defSpotIncrementor;
    public float defMovePercent;
    public int numHits;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        currentState.UpdateState(this);

        //Show that the AI took damage when hit
        if (animatingDamage)
        {
            AnimateDamage();
        }
    }

    /// <summary>
    /// Checks for collision with sea urchin to deal damage to the AI
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState.state == AIState.DefenseTransition)
            return;

        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Reduce damage when defending
            if (currentState.state == AIState.Defense)
            {
                health -= 0.5f;
                numHits++;
            }
            else
            {
                health -= 1.0f;
            }

            //Only reset the blink timer if the AI is finished
            //animating the previous damage, otherwise continue
            //animating the previous damage
            if (blinkTimer <= 0)
            {
                blinkTimer = blinkTime;
            }

            //Mark that the AI is ready to animate damage
            animatingDamage = true;
        }
    }

    /// <summary>
    /// Switches the state of the AI
    /// </summary>
    /// <param name="nextState">state to switch to</param>
    public void TransitionToNextState(State nextState)
    {
        previousState = currentState;
        currentState = nextState;
    }
    
    /// <summary>
    /// Provides visual cues that the AI has taken damage
    /// </summary>
    private void AnimateDamage()
    {
        blinkTimer -= Time.deltaTime;

        //Finished animating
        if (blinkTimer <= 0)
        {
            //Mark that the AI is finished animating damage
            animatingDamage = false;
            
            //Reset the blink interval timer
            blinkIntervalTimer = blinkInterval;

            //Make sure the AI is visible
            GetComponent<SpriteRenderer>().enabled = true;
        }

        //Blink the AI to animate the damage
        else
        {
            blinkIntervalTimer -= Time.deltaTime;

            if (blinkIntervalTimer <= 0)
            {
                //Reset the timer
                blinkIntervalTimer = blinkInterval;

                //Blink the AI
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            }
        }
    }
}
