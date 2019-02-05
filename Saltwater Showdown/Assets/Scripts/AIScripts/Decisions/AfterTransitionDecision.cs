using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for decisions after a transition occurs
/// </summary>
public abstract class AfterTransitionDecision : Decision {

    /// <summary>
    /// Resets variables needed after a transition is over
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    protected void ReconfigureState (StateManager stateManager)
    {
        //Reset the AI's finished status
        stateManager.finishedMask = false;
        stateManager.finishedTransformation = false;

        //Reset previously stored data for moving
        stateManager.movePercent = 0;

        //Store the AI's current position to use for LERPing
        stateManager.tempPos = stateManager.transform.position;

        //Mark that the AI needs to reconfigure its position to properly LERP
        stateManager.reconfiguring = true;
    }
}
