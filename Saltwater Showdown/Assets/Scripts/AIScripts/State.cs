using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all of the possible AI states
/// </summary>
public enum AIState {
    Normal,
    NormalTransition,
    Defense,
    DefenseTransition,
};

/// <summary>
/// Contains all of the actions and transitions associated with a state
/// </summary>
[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject {

    public AIState state;
    public Action action;
    public Transition[] transitions;

    /// <summary>
    /// Call to update the current state each frame (do the action associated with the state).
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public void UpdateState(StateManager stateManager)
    {
        action.PerformAction(stateManager);
        CheckTransitions(stateManager);
    }

    /// <summary>
    /// Checks if conditions are met to transition to another state.
    /// Then switches states.
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    private void CheckTransitions(StateManager stateManager)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool isReady = transitions[i].decision.MakeDecision(stateManager);

            if (isReady)
            {
                stateManager.TransitionToNextState(transitions[i].nextState);
            }
        }
    }
}
