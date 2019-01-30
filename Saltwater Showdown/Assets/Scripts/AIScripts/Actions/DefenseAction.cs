using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/Defense")]
public class DefenseAction : Action
{
    public float moveSpeed;

    /// <summary>
    /// Moves the AI up and down constantly
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        MoveBackAndForth(stateManager);
    }

    /// <summary>
    /// Moves the AI up and down constantly.
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    private void MoveBackAndForth(StateManager stateManager)
    {
        //Get the vectors to use for Lerping
        Vector3 currPos = stateManager.reconfiguring ? stateManager.tempPos : stateManager.defenseStatePositions[stateManager.currentPos].transform.position;
        Vector3 nextPos = stateManager.defenseStatePositions[stateManager.nextPos].transform.position;

        //Adjust the Lerp percent and clamp it if necessary
        stateManager.movePercent += Time.deltaTime * moveSpeed;
        Mathf.Clamp(stateManager.movePercent, 0.0f, 1.0f);

        //Lerp
        stateManager.transform.position = Vector3.Lerp(currPos, nextPos, stateManager.movePercent);

        //Arrived at next spot
        if (stateManager.movePercent >= 1.0f)
        {
            //Mark that the AI is finished reconfiguring
            if (stateManager.reconfiguring)
            {
                stateManager.reconfiguring = false;
            }

            //Update current spot
            stateManager.currentPos = stateManager.nextPos;

            //Increment down when the AI has reached the last spot in the array
            if (stateManager.currentPos == stateManager.defenseStatePositions.Length - 1)
            {
                stateManager.posIncrementor = -1;
            }

            //Increment up when the AI has reached the first spot in the array
            else if (stateManager.currentPos == 0)
            {
                stateManager.posIncrementor = 1;
            }

            //Update the next spot
            stateManager.nextPos += stateManager.posIncrementor;

            //Reset the move percent
            stateManager.movePercent = 0;
        }
    }
}
