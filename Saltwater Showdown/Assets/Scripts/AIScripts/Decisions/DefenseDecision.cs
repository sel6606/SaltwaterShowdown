using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for entering the Defense state
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/Defense")]
public class DefenseDecision : Decision
{
    /// <summary>
    /// Checks if the AI is entering the defensive state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if entering defense state; false if not</returns>
    public override bool MakeDecision(StateManager stateManager)
    {
        return EnteringDefensiveState(stateManager);
    }

    /// <summary>
    /// Checks if the AI is entering the defensive state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if entering defense state; false if note</returns>
    private bool EnteringDefensiveState(StateManager stateManager)
    {
        if (stateManager.finishedTransformation)
        {
            //Reset the AI's finished status
            stateManager.finishedMask = false;
            stateManager.finishedTransformation = false;

            //Reset the count for the number of times the AI was hit for a specific state
            stateManager.numHits = 0;

            //Choose a random position to move to
            stateManager.currentPos = Random.Range(0, stateManager.defenseStatePositions.Length);
            stateManager.nextPos = stateManager.currentPos;

            //Reset previously stored data for moving (which was stored in the Normal state)
            stateManager.movePercent = 0;

            //Set the incrementor to 1 since we are starting at position 0
            stateManager.posIncrementor = 1;

            //Store the AI's current position to use for LERPing
            stateManager.tempPos = stateManager.transform.position;

            //Mark that the AI needs to reconfigure its position to properly move between defenseStatePositions
            stateManager.reconfiguring = true;

            return true;
        }

        return false;
    }
}
