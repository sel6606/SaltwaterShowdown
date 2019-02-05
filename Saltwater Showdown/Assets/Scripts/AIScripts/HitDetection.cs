using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle collisions when the AI is in the Normal state
/// </summary>
public class HitDetection : MonoBehaviour {

    private StateManager ai;

    private bool readyToDestroy;

    // Use this for initialization
    void Start ()
    {
        ai = transform.parent.GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy the AI after it has been hit and its particles have stopped
        if (readyToDestroy && gameObject.GetComponent<ParticleSystem>().isStopped)
        {
            //Mark that the game is over
            GameInfo.instance.GameOver = true;

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Handles collisions
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Only check for collisions while in the Normal state
            if (ai.currentState.state != AIState.Normal)
                return;

            //Update the number of times the AI was hit
            ai.numHits++;

            //The AI can no longer go to the Defense state and is ready to die
            if (ai.numLights <= 0 && ai.numHits >= ai.maxHits)
            {
                //Play the death particles
                gameObject.GetComponent<ParticleSystem>().Play();

                //Hide this object
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                //Mark that the AI should be destroyed
                readyToDestroy = true;
            }

            //The AI can still go to the Defense state
            else
            {
                //Temporarily convert to trigger
                gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;

                //Mark that collisions should be re-enabled when ready
                ai.reenableNormal = true;

                //Reset blink timer
                ai.blinkTimer = ai.blinkTime;

                //Set its damage color
                gameObject.GetComponent<SpriteRenderer>().color = ai.damageColor;

                //Mark that the AI is ready to animate damage
                ai.animatingDamage = true;
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
            ai.reenableNormal = false;
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
            ai.reenableNormal = true;
        }
    }
}
