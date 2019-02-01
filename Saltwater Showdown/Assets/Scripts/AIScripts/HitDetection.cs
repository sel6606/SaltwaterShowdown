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
            //This occurs when transitioning from Defense to Normal state
            if (ai.GetComponent<ParticleSystem>().isPlaying)
                return;

            //Update the number of times the AI was hit in its current state
            ai.numHits++;

            //Turn off the collider to avoid being hit while blinking
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;

            //Reset blink timer
            ai.blinkTimer = ai.blinkTime;

            //Set its damage color
            gameObject.GetComponent<SpriteRenderer>().color = ai.damageColor;

            //Mark that the AI is ready to animate damage
            ai.animatingDamage = true;
        }
    }
}
