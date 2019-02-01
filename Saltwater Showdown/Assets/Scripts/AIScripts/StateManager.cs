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

    //Variables for damaging the AI
    public int health;
    public GameObject[] weakPoints;
    public GameObject[] weakPointPositions;

    //Variables for attacking
    public GameObject bullet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        currentState.UpdateState(this);
    }

    /// <summary>
    /// Checks for collision with sea urchin to deal damage to the AI
    /// </summary>
    /// <param name="collision">object collided with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
}
