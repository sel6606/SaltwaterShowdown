using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for entering the Normal Transition state
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/NormalTransition")]
public class NormalTransitionDecision : BeforeTransitionDecision {

    /// <summary>
    /// Checks if the AI took enough damage to start its transition
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if took enough damage; false if not</returns>
    public override bool MakeDecision(StateManager stateManager)
    {
        return BrokenDefense(stateManager);
    }

    /// <summary>
    /// Checks if the AI took enough damage to start its transition
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if took enough damage; false if not</returns>
    private bool BrokenDefense(StateManager stateManager)
    {
        //Disable collisions with the sprite when the transition is occurring
        if (ReachedThreshold(stateManager))
        {
            stateManager.DisableDefenseSprite();

            return true;
        }

        return false;
    }
}
