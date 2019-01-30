using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for switching back to the Normal state from the Defense state
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/DefenseToNormal")]
public class DefenseToNormalDecision : Decision
{
    public override bool MakeDecision(StateManager stateManager)
    {
        return BrokenDefense(stateManager);
    }

    private bool BrokenDefense(StateManager stateManager)
    {
        if (stateManager.finishedTransformation)
        {
            //Reset the AI's finished status
            stateManager.finishedMask = false;
            stateManager.finishedTransformation = false;

            //Choose a random position to move to
            stateManager.currentPos = Random.Range(0, stateManager.normalStatePositions.Length);
            stateManager.nextPos = stateManager.currentPos;

            //Reset previously stored data for moving (which was stored in the Normal state)
            stateManager.movePercent = 0;

            //Store the AI's current position to use for LERPing
            stateManager.tempPos = stateManager.transform.position;

            //Mark that the AI needs to reconfigure its position to properly move between defenseStatePositions
            stateManager.reconfiguring = true;

            return true;
        }

        return false;
    }
}
