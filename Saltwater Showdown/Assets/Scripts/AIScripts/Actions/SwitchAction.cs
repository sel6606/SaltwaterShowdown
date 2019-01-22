using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to switch/move the AI's weak points
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/Switch")]
public class SwitchAction : Action
{
    /// <summary>
    /// Handles switching/moving the AI's weak points
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        MoveWeakPoints(stateManager);
    }

    /// <summary>
    /// Handles switching/moving the AI's weak points
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    private void MoveWeakPoints(StateManager stateManager)
    {

    }
}
