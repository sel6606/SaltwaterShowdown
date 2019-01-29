using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to generate the AI at the start of a level.
/// </summary>
public class AIGenerator : MonoBehaviour {

    public GameObject spawnPos;
    public GameObject AI;

	// Use this for initialization
	void Start () {
        SpawnAI();
	}

    /// <summary>
    /// Spawns the AI
    /// </summary>
    private void SpawnAI()
    {
        Instantiate(AI, spawnPos.transform.position, Quaternion.identity);
    }
}
