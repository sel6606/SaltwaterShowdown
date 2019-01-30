using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseLight : MonoBehaviour {

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
            //Update the number of times the AI was hit in its current state
            transform.parent.parent.GetComponent<StateManager>().numHits++;

            //Play the particle system to show an explosion
            gameObject.GetComponent<ParticleSystem>().Play();

            //Remove this object
            Destroy(gameObject);
        }
    }
}
