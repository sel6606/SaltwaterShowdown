using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all AI Decisions
/// </summary>
public abstract class Decision : ScriptableObject {

    /// <summary>
    /// Abstract method AI will use to determine if it is ready to switch states
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if ready to switch; false if not</returns>
    public abstract bool MakeDecision(StateManager stateManager);
}
