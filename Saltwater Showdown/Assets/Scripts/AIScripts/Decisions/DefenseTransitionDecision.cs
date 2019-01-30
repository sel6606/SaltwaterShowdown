using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for transitioning to the Defense State
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/DefenseTransition")]
public class DefenseTransitionDecision : Decision
{
    public int hitThreshold;

    /// <summary>
    /// Checks if the AI is ready to transition to the Defense state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if ready; false if not</returns>
    public override bool MakeDecision(StateManager stateManager)
    {
        return ReadyToDefend(stateManager);
    }

    /// <summary>
    /// Checks if the AI is ready to transition to the Defense state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if ready; false if not</returns>
    private bool ReadyToDefend(StateManager stateManager)
    {
        if (stateManager.currentState.state != AIState.Defense && stateManager.numHits >= hitThreshold)
        {
            return true;
        }

        return false;
    }
}
