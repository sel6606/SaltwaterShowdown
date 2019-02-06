using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishScroll : MonoBehaviour {

    public float speed;
    public float bound;
    public float reset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameInfo.instance.Paused)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);

            if (transform.position.x < bound)
            {
                transform.position = new Vector3(reset, transform.position.y, transform.position.z);
            }
        }
	}
}
