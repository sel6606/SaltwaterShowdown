using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for switching back to the Normal state from the Defense state
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/DefenseToNormal")]
public class DefenseToNormalDecision : Decision
{
    public int maxHits;

    public override bool MakeDecision(StateManager stateManager)
    {
        return BrokenDefense(stateManager);
    }

    private bool BrokenDefense(StateManager stateManager)
    {
        if (stateManager.numHits == maxHits)
        {
            stateManager.numHits = 0;

            //Move back to the starting position
            //stateManager.transform.position = stateManager.normalSpotsToMove[0].transform.position;

            return true;
        }

        return false;
    }
}
