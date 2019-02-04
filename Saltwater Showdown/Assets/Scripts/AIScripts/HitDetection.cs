using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle collisions when the AI is in the Normal state
/// </summary>
public class HitDetection : MonoBehaviour {

    /// <summary>
    /// Handles collisions
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Get reference to the AI
            StateManager ai = transform.parent.GetComponent<StateManager>();

            //Only check for collisions while in the Normal state
            if (ai.currentState.state != AIState.Normal)
                return;

            //Special case: Don't check for collisions if the masking isn't finished
            //This occurs when transitioning into the Normal state
            if (ai.GetComponent<ParticleSystem>().isPlaying)
                return;

            //Update the number of times the AI was hit in its current state
            ai.numHits++;

            //Temporarily convert to trigger
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;

            //Mark that collisions should be re-enabled when ready
            ai.reenableCollision = true;

            //Reset blink timer
            ai.blinkTimer = ai.blinkTime;

            //Set its damage color
            gameObject.GetComponent<SpriteRenderer>().color = ai.damageColor;

            //Mark that the AI is ready to animate damage
            ai.animatingDamage = true;
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
            //Get reference to the AI
            StateManager ai = transform.parent.GetComponent<StateManager>();

            //Mark that the collision shouldn't be re-enabled until the urchin exits the trigger
            ai.reenableCollision = false;
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
            //Get reference to the AI
            StateManager ai = transform.parent.GetComponent<StateManager>();

            //Mark that the collision should be re-enabled since the urchin is out of the AI
            ai.reenableCollision = true;
        }
    }
}
