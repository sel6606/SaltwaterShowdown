using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all AI Actions
/// </summary>
public abstract class Action : ScriptableObject {

    /// <summary>
    /// Abstract method AI will use to perform various actions
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public abstract void PerformAction(StateManager stateManager);
}
