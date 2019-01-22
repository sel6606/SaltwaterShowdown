using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains variables needed to switch states for the AI
/// </summary>
[System.Serializable]
public class Transition {

    public Decision decision;
    public State nextState;
}
