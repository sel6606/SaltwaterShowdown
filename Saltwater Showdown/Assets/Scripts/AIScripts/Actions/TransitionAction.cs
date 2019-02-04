using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for transitions
/// </summary>
public abstract class TransitionAction : Action {

    public float maxEmission;
    public float maxRotSpeed;
    public float rotSpeedIncrementor;
    public float emiSpeedIncrementor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stateManager"></param>
    /// <param name="currSprite"></param>
    /// <param name="nextSprite"></param>
    private void ApplyMask(StateManager stateManager, GameObject currSprite, GameObject nextSprite)
    {
        //Get reference to the particle system
        ParticleSystem bubbles = stateManager.gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule emission = bubbles.emission;

        if (bubbles.isStopped)
        {
            //Set the base emission rate
            emission.rateOverTime = 0;

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

        //Finished rotating
        if (stateManager.rotationSpeed >= maxRotSpeed && emission.rateOverTime.constant >= maxEmission)
        {
            //Reset the rotation back to normal
            stateManager.transform.rotation = Quaternion.identity;
            stateManager.rotationSpeed = 0;

            //Remove the sprite for the Defense state
            currSprite.SetActive(false);

            //Make the sprite for the Normal state visible
            nextSprite.SetActive(true);

            //Stop the particle system
            bubbles.Stop();

            //Mark that the masking is complete
            stateManager.finishedMask = true;

            //Mark that the transformation is complete
            stateManager.finishedTransformation = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stateManager"></param>
    protected void MaskDefenseTransformation(StateManager stateManager)
    {
        ApplyMask(stateManager, stateManager.normal, stateManager.defense);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stateManager"></param>
    protected void MaskNormalTransformation(StateManager stateManager)
    {
        ApplyMask(stateManager, stateManager.defense, stateManager.normal);
    }
}
