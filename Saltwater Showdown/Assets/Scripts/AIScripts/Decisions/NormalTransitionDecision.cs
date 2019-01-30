using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/NormalTransition")]
public class NormalTransitionDecision : Decision {

    public int maxHits;

    public override bool MakeDecision(StateManager stateManager)
    {
        return BrokenDefense(stateManager);
    }

    private bool BrokenDefense(StateManager stateManager)
    {
        if (stateManager.numHits == maxHits)
        {
            //Reset the count for the number of times the AI was hit for a specific state
            stateManager.numHits = 0;

            return true;
        }

        return false;
    }
}
