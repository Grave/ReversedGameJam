using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject[] spawnableUIPrefabs;
    private List<GameObject> currentlySpawnablePrefabs = new List<GameObject>();
    private int currentLevel = 1;

    private void StartCurrentLevel() {
        InitSpawnableUIsForLevel();
        BroadcastMessage("PlaySound", "MorningCrow", SendMessageOptions.DontRequireReceiver);
    }

    private void InitSpawnableUIsForLevel() {
        currentlySpawnablePrefabs.Clear();
        foreach (var spawnableUIPrefab in spawnableUIPrefabs)
        {
            if (spawnableUIPrefab.GetComponent<LevelRequirement>().IsValidForLevel(currentLevel))
            {
                currentlySpawnablePrefabs.Add(spawnableUIPrefab);
            }
        }
    }
    
	void Start () {
        StartCurrentLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
