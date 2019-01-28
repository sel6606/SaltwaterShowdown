using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for transitioning to the Defensive State
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/DefenseTransition")]
public class DefenseTransitionDecision : Decision
{
    [Range(0.0f, 1.0f)]
    public float chanceToDefend;

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
        if (stateManager.numDefensiveLights > 0 && stateManager.dTransitionTimer <= 0)
        {
            //Reset timer
            stateManager.dTransitionTimer = stateManager.dTransitionTime;

            //Get a random chance
            float rand = Random.Range(0.0f, 1.0f);

            if (rand <= chanceToDefend)
            {
                return true;
            }

            return false;
        }

        return false;
    }
}
