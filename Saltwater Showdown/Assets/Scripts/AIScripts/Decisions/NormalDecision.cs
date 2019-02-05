using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for entering the Normal state
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/Normal")]
public class NormalDecision : AfterTransitionDecision
{
    /// <summary>
    /// Checks if the AI finished transitioning to the Normal state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if finished; false if not</returns>
    public override bool MakeDecision(StateManager stateManager)
    {
        return FinishedTransition(stateManager);
    }

    /// <summary>
    /// Checks if the AI finished transitioning to the Normal state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if finished; false if not</returns>
    private bool FinishedTransition(StateManager stateManager)
    {
        if (stateManager.finishedTransformation)
        {
            //Make sure collisions with the Normal sprite is enabled
            stateManager.EnableNormalSprite();

            //Reconfigure variables to get ready for the Normal state
            ReconfigureState(stateManager);

            //Choose a random position to move to
            stateManager.currentPos = Random.Range(0, stateManager.normalStatePositions.Length);
            stateManager.nextPos = stateManager.currentPos;

            return true;
        }

        return false;
    }
}
