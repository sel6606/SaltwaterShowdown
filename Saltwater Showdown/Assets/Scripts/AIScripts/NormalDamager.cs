using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDamager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Urchin"))
        {
            StateManager ai = transform.parent.GetComponent<StateManager>();

            if (ai.currentState.state != AIState.Normal)
                return;

            ai.health -= 1.0f;

            //Update the number of times the AI was hit in its current state
            transform.parent.GetComponent<StateManager>().numHits++;

            //Only reset the blink timer if the AI is finished
            //animating the previous damage, otherwise continue
            //animating the previous damage
            if (ai.blinkTimer <= 0)
            {
                ai.blinkTimer = ai.blinkTime;
            }

            //Mark that the AI is ready to animate damage
            ai.animatingDamage = true;
        }
    }
}
