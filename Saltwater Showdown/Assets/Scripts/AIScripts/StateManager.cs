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

    public Sprite baseDefSprite;

    //Variables for damaging the AI
    public int numHits;
    public int maxHits;
    public int numLights;

    public Color damageColor;
    public bool animatingDamage;
    public bool reenableNormal;
    public bool reenableDefense;

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

    public Color defTransitionColor;
    public Color normalTransitionColor;

    //Variables for Defense state
    public GameObject[] defenseStatePositions;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameInfo.instance.GameStart && !GameInfo.instance.GameOver && !GameInfo.instance.Win && !GameInfo.instance.Paused)
        {
            //Update the state
            currentState.UpdateState(this);

            //Show that the AI took damage when hit
            if (animatingDamage)
            {
                AnimateDamage();
            }

            //Re-enable collisions when done animating damage.
            //This also re-enables collisions when switching from Defense
            //to Normal state.
            else if (reenableNormal && !gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                ConvertNormalFromTrigger();
            }

            //This re-enables collisions when switching from Normal
            //to Defense state
            if (reenableDefense && !gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                ConvertDefenseFromTrigger();
            }
        }

        if (GameInfo.instance.Paused)
        {
            //Pause the particle system if it's playing
            if (gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                gameObject.GetComponent<ParticleSystem>().Pause(false);
            }

            //Pause the animation
            if (defense.GetComponent<Animator>().speed == 1)
            {
                defense.GetComponent<Animator>().speed = 0;
            }
        }
        else
        {
            //Resume the particle system if it was paused
            if (gameObject.GetComponent<ParticleSystem>().isPaused)
            {
                //If the AI finished transitioning, the particle system should be stopping
                //Note: Play and then Stop the particle system to have it resume from a pause
                if (currentState.state == AIState.Normal || currentState.state == AIState.Defense)
                {
                    gameObject.GetComponent<ParticleSystem>().Play(false);
                    gameObject.GetComponent<ParticleSystem>().Stop(false);
                }

                //The AI is still transitioning, continue playing the particles
                else
                {
                    gameObject.GetComponent<ParticleSystem>().Play(false);
                }
            }

            //Play the animation if it was paused
            if (defense.GetComponent<Animator>().speed == 0)
            {
                defense.GetComponent<Animator>().speed = 1;
            }
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
    /// Disables collisions for the Normal sprite
    /// </summary>
    public void DisableNormalSprite()
    {
        normal.GetComponent<PolygonCollider2D>().enabled = false;
    }

    /// <summary>
    /// Enables collisions for the Normal sprite
    /// </summary>
    public void EnableNormalSprite()
    {
        //IMPORTANT: When re-enabling, make sure to set to isTrigger just in case
        //the urchin is inside the collider when re-enabling.
        normal.GetComponent<PolygonCollider2D>().enabled = true;
        normal.GetComponent<PolygonCollider2D>().isTrigger = true;

        //IMPORTANT: Mark this as true so that the AI can turn off the trigger
        //when the urchin is outside of the collider
        reenableNormal = true;
    }

    /// <summary>
    /// Turns the collider into a normal collider (not trigger)
    /// </summary>
    private void ConvertNormalFromTrigger()
    {
        //Switch collider back to a normal collider
        reenableNormal = false;
        normal.GetComponent<PolygonCollider2D>().isTrigger = false;
    }

    /// <summary>
    /// Disables collisions for the Defense sprite
    /// </summary>
    public void DisableDefenseSprite()
    {
        for (int i = 0; i < defense.transform.childCount; i++)
        {
            defense.transform.GetChild(i).GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    /// <summary>
    /// Enables collisions for the Defense sprite
    /// </summary>
    public void EnableDefenseSprite()
    {
        for (int i = 0; i < defense.transform.childCount; i++)
        {
            //IMPORTANT: When re-enabling, make sure to set to isTrigger just in case
            //the urchin is inside the collider when re-enabling.
            defense.transform.GetChild(i).GetComponent<CircleCollider2D>().enabled = true;
            defense.transform.GetChild(i).GetComponent<CircleCollider2D>().isTrigger = true;
        }

        //IMPORTANT: Mark this as true so that the AI can turn off the trigger
        //when the urchin is outside of the collider
        reenableDefense = true;
    }

    /// <summary>
    /// Turns the collider into a normal collider (not trigger)
    /// </summary>
    private void ConvertDefenseFromTrigger()
    {
        //Reset variable
        reenableDefense = false;

        //Switch collider back to a normal collider
        for (int i = 0; i < defense.transform.childCount; i++)
        {
            defense.transform.GetChild(i).GetComponent<CircleCollider2D>().isTrigger = false;
        }
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
