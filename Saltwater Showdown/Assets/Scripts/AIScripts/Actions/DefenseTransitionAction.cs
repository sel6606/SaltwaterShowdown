using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/DefenseTransition")]
public class DefenseTransitionAction : Action
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        MaskTransformation(stateManager);
    }

    private void MaskTransformation(StateManager stateManager)
    {
        if (stateManager.defenseMask.isStopped)
        {
            stateManager.defenseMask.Play();
        }
    }
}
