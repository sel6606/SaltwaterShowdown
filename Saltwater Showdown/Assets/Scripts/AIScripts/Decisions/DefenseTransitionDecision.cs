using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for entering the Defense Transition state
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/DefenseTransition")]
public class DefenseTransitionDecision : BeforeTransitionDecision
{
    /// <summary>
    /// Checks if the AI took enough damage to start its transition
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if took enough damage; false if not</returns>
    public override bool MakeDecision(StateManager stateManager)
    {
        return ReadyToDefend(stateManager);
    }

    /// <summary>
    /// Checks if the AI took enough damage to start its transition
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if took enough damage; false if not</returns>
    private bool ReadyToDefend(StateManager stateManager)
    {
        //Disable collisions with the sprite when the transition is occurring
        if (ReachedThreshold(stateManager) && stateManager.numLights > 0)
        {
            stateManager.DisableNormalSprite();

            return true;
        }

        return false;
    }
}
