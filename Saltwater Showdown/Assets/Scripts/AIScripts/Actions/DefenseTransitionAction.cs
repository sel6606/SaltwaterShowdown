using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/DefenseTransition")]
public class DefenseTransitionAction : Action
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        MaskTransformation(stateManager);
    }

    private void MaskTransformation(StateManager stateManager)
    {
        //if (stateManager.defenseMask.isStopped)
        //{
        //    stateManager.defenseMask.Play();
        //}

        //Move back to the starting position
        stateManager.transform.position = stateManager.normalSpotsToMove[0].transform.position;
        stateManager.currDefSpot = 1;

        //Switch Sprite
        stateManager.GetComponent<SpriteRenderer>().sprite = stateManager.defense;

        //Mark that the AI is entering the defense state
        stateManager.enteringDefense = true;
    }
}
