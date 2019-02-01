using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle collisions between the urchin and the AI's lights
/// while in the Defense state.
/// </summary>
public class LightExplosion : MonoBehaviour {

    /// <summary>
    /// Checks collisions
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Urchin"))
        {
            //Get reference to the AI
            StateManager ai = transform.parent.parent.GetComponent<StateManager>();

            //This check ensures that two lights won't get destroyed at the same time
            if (ai.numHits == 0)
            {
                //Update the number of times the AI was hit in its current state
                ai.numHits++;

                //Update the light count
                ai.numLights--;

                //Play the particle system to show an explosion
                gameObject.GetComponent<ParticleSystem>().Play();

                //Remove this object
                Destroy(gameObject);
            }
        }
    }
}
