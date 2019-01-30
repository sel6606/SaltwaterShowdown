using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the AI's transformation to the Defense state
/// </summary>
[CreateAssetMenu(menuName = "AI/Actions/DefenseTransition")]
public class DefenseTransitionAction : Action
{
    public float maxEmission;
    public float maxRotSpeed;
    public float rotSpeedIncrementor;
    public float emiSpeedIncrementor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    public override void PerformAction(StateManager stateManager)
    {
        if (!stateManager.finishedMask)
        {
            Rotate(stateManager);
        }
    }

    private void Rotate(StateManager stateManager)
    {
        //Used to increase or decrease the speed (increase before masking, decrease after masking)
        int direction = stateManager.finishedMask ? -1 : 1;

        //Get reference to the particle system
        ParticleSystem bubbles = stateManager.gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule emission = bubbles.emission;

        if (bubbles.isStopped)
        {
            //Only set to the base emission rate when first applying the mask
            if (!stateManager.finishedMask)
            {
                emission.rateOverTime = 10;
            }

            //Show the sprite for the Defense state
            else
            {
                //Remove the sprite for the Normal state
                stateManager.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }

            //Start the particle system
            bubbles.Play();
        }

        //Increase the emission rate until the max is reached (not worried about clamping)
        if (emission.rateOverTime.constant < maxEmission)
        {
            emission.rateOverTime = emission.rateOverTime.constant + emiSpeedIncrementor * Time.deltaTime;
        }

        //Increase the rotation speed (not worried about clamping)
        if (stateManager.rotationSpeed < maxRotSpeed)
        {
            stateManager.rotationSpeed += rotSpeedIncrementor * Time.deltaTime;
        }

        //Rotate the AI
        stateManager.transform.Rotate(Vector3.forward, stateManager.rotationSpeed);

        //Still applying mask (speeding up)
        if (!stateManager.finishedMask)
        {
            //Finished rotating
            if (stateManager.rotationSpeed >= maxRotSpeed && emission.rateOverTime.constant >= maxEmission)
            {
                //Mark that the masking is complete
                stateManager.finishedMask = true;

                //Remove the sprite for the Normal state
                stateManager.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                //Move to the spawn location
                stateManager.gameObject.transform.position = stateManager.normalStatePositions[0].transform.position;

                //Stop the particle system
                bubbles.Stop();
            }
        }

        //Finished applying mask (slowing up)
        else
        {
            if (stateManager.rotationSpeed <= 0 && emission.rateOverTime.constant <= 0)
            {
                //Mark that the transformation is complete
                stateManager.finishedTransformation = true;

                //Stop the particle system
                bubbles.Stop();
            }
        }
    }       

    private void RemoveMask(StateManager stateManager)
    {
        
    }
}
