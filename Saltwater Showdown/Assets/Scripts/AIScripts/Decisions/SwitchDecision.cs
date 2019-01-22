using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to determine if the AI is ready to switch/move its weak points
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/Switch")]
public class SwitchDecision : Decision
{
    /// <summary>
    /// Checks if the AI is ready to switch/move its weak points
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if ready; false if not</returns>
    public override bool MakeDecision(StateManager stateManager)
    {
        throw new System.NotImplementedException();
    }
}
