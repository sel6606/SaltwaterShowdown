using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the AI's transformation to the Defense state
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/DefenseTransition")]
public class DefenseTransitionAction : TransitionAction
{
    /// <summary>
    /// Masks the transformation to the Defense state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        if (!stateManager.finishedTransformation)
        {
            MaskDefenseTransformation(stateManager);
        }
    }

    /// <summary>
    /// Masks the transformation to the Defense state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    private void MaskDefenseTransformation(StateManager stateManager)
    {
        ApplyMask(stateManager, stateManager.normal, stateManager.defense);
    }
}
