using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for entering the Defense state
/// </summary>
[CreateAssetMenu(menuName = "AI/Decisions/Defense")]
public class DefenseDecision : AfterTransitionDecision
{
    /// <summary>
    /// Checks if the AI finished transitioning to the Defense state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if finished; false if not</returns>
    public override bool MakeDecision(StateManager stateManager)
    {
        return FinishedTransition(stateManager);
    }

    /// <summary>
    /// Checks if the AI finished transitioning to the Defense state
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    /// <returns>true if finished; false if not</returns>
    private bool FinishedTransition(StateManager stateManager)
    {
        if (stateManager.finishedTransformation)
        {
            //Make sure collisions with the Defense sprite is enabled
            stateManager.EnableDefenseSprite();

            //Reconfigure variables to get ready for the Defense state
            ReconfigureState(stateManager);

            //Choose a random position to move to
            stateManager.currentPos = Random.Range(0, stateManager.defenseStatePositions.Length);
            stateManager.nextPos = stateManager.currentPos;

            //Special Case: Set incrementor just in case AI goes to the middle position (middle doesn't set incrementor)
            stateManager.posIncrementor = 1;

            return true;
        }

        return false;
    }
}
