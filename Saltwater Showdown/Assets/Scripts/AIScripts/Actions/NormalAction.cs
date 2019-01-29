using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the AI in its base state
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/Normal")]
public class NormalAction : Action
{
    public float moveSpeed;

    /// <summary>
    /// Checks if the AI is ready to move, then moves it
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        if (!stateManager.isMoving)
        {
            CheckIfReadyToMove(stateManager);
        }
        else
        {
            Move(stateManager);
        }
    }

    /// <summary>
    /// Checks if the AI is ready to move, and finds the next spot to move to.
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if ready; false if not</returns>
    private void CheckIfReadyToMove(StateManager stateManager)
    {
        stateManager.idleTimer -= Time.deltaTime;

        if (stateManager.idleTimer <= 0)
        {
            //Reset timer
            stateManager.idleTimer = stateManager.idleTime;

            //Get a random spot to move to
            stateManager.nextSpot = Random.Range(0, stateManager.normalSpotsToMove.Length);

            //Only start moving if the next spot is different from the current spot
            if (stateManager.nextSpot != stateManager.currentSpot)
            {
                stateManager.isMoving = true;
                stateManager.movePercent = 0;
            }
        }
    }

    /// <summary>
    /// Moves the AI from its current position to its next position.
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    private void Move(StateManager stateManager)
    {
        //Get the vectors to use for Lerping
        Vector3 currPos = stateManager.normalSpotsToMove[stateManager.currentSpot].transform.position;
        Vector3 nextPos = stateManager.normalSpotsToMove[stateManager.nextSpot].transform.position;

        //Adjust the Lerp percent and clamp it if necessary
        stateManager.movePercent += Time.deltaTime * moveSpeed;
        Mathf.Clamp(stateManager.movePercent, 0.0f, 1.0f);

        //Lerp
        stateManager.transform.position = Vector3.Lerp(currPos, nextPos, stateManager.movePercent);

        //Arrived at next spot
        if (stateManager.movePercent >= 1.0f)
        {
            //Update current spot
            stateManager.currentSpot = stateManager.nextSpot;

            //Reset the move percent
            stateManager.movePercent = 0;

            //Mark that the AI is no longer moving
            stateManager.isMoving = false;
        }
    }
}
