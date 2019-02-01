using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for AI attacks
/// </summary>
public abstract class AttackAction : Action {

    public int numBullets;
    public float bulletSpeed;

    /// <summary>
    /// Handles attacking the player
    /// </summary>
    /// <param name="stateManager">Script attached to AI that manages switching between states</param>
    protected abstract void FireBullet(StateManager stateManager);
}
