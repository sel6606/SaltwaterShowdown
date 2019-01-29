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
        //Increment down when the AI has reached the last spot in the array
        if (stateManager.currDefSpot == stateManager.defenseSpotsToMove.Length - 1)
        {
            stateManager.defSpotIncrementor = -1;
        }

        //Increment up when the AI has reached the first spot in the array
        else if (stateManager.currDefSpot == 0)
        {
            stateManager.defSpotIncrementor = 1;
        }

        //Get the vectors to use for Lerping
        Vector3 currPos = stateManager.defenseSpotsToMove[stateManager.currDefSpot].transform.position;
        Vector3 nextPos = stateManager.defenseSpotsToMove[stateManager.currDefSpot + stateManager.defSpotIncrementor].transform.position;

        //Adjust the Lerp percent and clamp it if necessary
        stateManager.defMovePercent += Time.deltaTime * moveSpeed;
        Mathf.Clamp(stateManager.defMovePercent, 0.0f, 1.0f);

        //Lerp
        stateManager.transform.position = Vector3.Lerp(currPos, nextPos, stateManager.defMovePercent);

        //Arrived at next spot
        if (stateManager.defMovePercent >= 1.0f)
        {
            //Update current spot
            stateManager.currDefSpot += stateManager.defSpotIncrementor;

            //Reset the move percent
            stateManager.defMovePercent = 0;
        }
    }
}
