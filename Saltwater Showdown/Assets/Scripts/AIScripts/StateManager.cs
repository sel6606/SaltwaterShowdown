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

    public GameObject normal;
    public GameObject defense;

    //Variables for damaging the AI
    public float health;
    public int numHits;
    public GameObject[] weakPoints;
    public GameObject[] weakPointPositions;

    public float blinkTime;
    public float blinkTimer;
    public float blinkInterval;
    public float blinkIntervalTimer;
    public bool animatingDamage;

    //Variables for attacking
    public GameObject bullet;

    //Variables for moving
    public Vector3 tempPos;
    public int currentPos;
    public int nextPos;
    public int posIncrementor;
    public float movePercent;
    public bool reconfiguring;

    //Variables for Normal state
    public GameObject[] normalStatePositions;
    public float idleTime;
    public float idleTimer;

    //Variables for masking transitions
    public float rotationSpeed;
    public bool finishedMask;
    public bool finishedTransformation;

    //Variables for Defense state
    public GameObject[] defenseStatePositions;


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
        if (currentState.state == AIState.DefenseTransition || currentState.state == AIState.NormalTransition)
            return;

        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Reduce damage when defending
            if (currentState.state == AIState.Defense)
            {
                health -= 0.5f;
            }
            else
            {
                health -= 1.0f;
            }

            numHits++;

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

            if (currentState.state == AIState.Normal)
            {
                //Blink the AI
                normal.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (currentState.state == AIState.Defense)
            {
                //Blink the AI
                defense.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        //Blink the AI to animate the damage
        else
        {
            blinkIntervalTimer -= Time.deltaTime;

            if (blinkIntervalTimer <= 0)
            {
                //Reset the timer
                blinkIntervalTimer = blinkInterval;

                if (currentState.state == AIState.Normal)
                {
                    //Blink the AI
                    normal.GetComponent<SpriteRenderer>().enabled = !normal.GetComponent<SpriteRenderer>().enabled;
                }
                else if (currentState.state == AIState.Defense)
                {
                    //Blink the AI
                    defense.GetComponent<SpriteRenderer>().enabled = !defense.GetComponent<SpriteRenderer>().enabled;
                }
                
            }
        }
    }
}
