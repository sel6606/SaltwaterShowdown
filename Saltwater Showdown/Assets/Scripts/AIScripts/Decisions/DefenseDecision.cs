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
        if (stateManager.enteringDefense)
        {
            stateManager.enteringDefense = false;
            stateManager.currDefSpot = 1;

            return true;
        }

        return false;
    }
}
