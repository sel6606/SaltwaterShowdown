using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishUp : MonoBehaviour {

    public float speed;
    public float bound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y + speed, transform.position.z);

        if (transform.position.y > bound)
        {
            transform.position = new Vector3(-1f, -3, transform.position.z);
        }
    }
}
