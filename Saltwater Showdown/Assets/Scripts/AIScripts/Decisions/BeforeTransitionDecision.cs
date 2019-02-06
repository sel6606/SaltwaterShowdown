using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for decisions before a transition occurs
/// </summary>
public abstract class BeforeTransitionDecision : Decision {

    public int maxHits;

    protected bool ReachedThreshold(StateManager stateManager)
    {
        //The damage threshold was reached
        if (stateManager.numHits >= maxHits)
        {
            //Reset the count for the next state
            stateManager.numHits = 0;

            return true;
        }

        return false;
    }
}
