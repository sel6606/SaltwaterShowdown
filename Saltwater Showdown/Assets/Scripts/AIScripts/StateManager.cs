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
    public int numHits;
    public int numLights;

    public Color damageColor;
    public bool animatingDamage;

    public float blinkTime;
    public float blinkTimer;
    public float blinkInterval;
    public float blinkIntervalTimer;

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

            //Change the color back to normal
            normal.GetComponent<SpriteRenderer>().color = Color.white;

            //Re-enable the collider
            normal.GetComponent<PolygonCollider2D>().enabled = true;
            
            //Reset the blink interval timer
            blinkIntervalTimer = blinkInterval;

            //Ensure the AI is visible
            normal.GetComponent<SpriteRenderer>().enabled = true;
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
                normal.GetComponent<SpriteRenderer>().enabled = !normal.GetComponent<SpriteRenderer>().enabled;
            }
        }
    }
}
