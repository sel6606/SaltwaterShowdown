using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle collisions between the urchin and the AI's lights
/// while in the Defense state.
/// </summary>
public class LightExplosion : MonoBehaviour {

    private StateManager ai;

    private bool readyToDestroy;

    // Use this for initialization
    void Start()
    {
        ai = transform.parent.parent.GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy the light after it has been hit and its particles have stopped
        if (readyToDestroy && gameObject.GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(gameObject);
        }

        //Pause the particle system on pause
        if (GameInfo.instance.Paused)
        {
            if (gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                gameObject.GetComponent<ParticleSystem>().Pause();
            }
        }

        //Resume the particle system if it was paused
        //Note: We play and stop to ensure the particle system ends
        else
        {
            if (gameObject.GetComponent<ParticleSystem>().isPaused)
            {
                gameObject.GetComponent<ParticleSystem>().Play();
                gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    /// <summary>
    /// Checks collisions
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Only check for collisions while in the Defense state
            if (ai.currentState.state != AIState.Defense)
                return;

            //This check ensures that two lights won't get destroyed at the same time
            if (ai.numHits == 0)
            {
                //Update the number of times the AI was hit in its current state
                ai.numHits++;

                //Update the light count
                ai.numLights--;

                //Play the particle system to show an explosion
                gameObject.GetComponent<ParticleSystem>().Play();

                //Make the light invisible
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                //Mark that the light should be destroyed
                readyToDestroy = true;
            }
        }
    }

    /// <summary>
    /// Handles collisions
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Mark that the collision shouldn't be re-enabled until the urchin exits the trigger
            ai.reenableDefense = false;
        }
    }

    /// <summary>
    /// Handles collisions
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Mark that the collision should be re-enabled since the urchin is out of the trigger
            ai.reenableDefense = true;
        }
    }
}
