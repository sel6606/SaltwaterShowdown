using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the AI's transformation to the Normal state
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/NormalTransition")]
public class NormalTransitionAction : TransitionAction {

    /// <summary>
    /// Masks the transformation to the Normal state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        if (!stateManager.finishedTransformation)
        {
            MaskNormalTransformation(stateManager);
        }
    }

    /// <summary>
    /// Masks the transformation to the Normal state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    private void MaskNormalTransformation(StateManager stateManager)
    {
        ApplyMask(stateManager, stateManager.defense, stateManager.normal, stateManager.normalTransitionColor);
    }
}
