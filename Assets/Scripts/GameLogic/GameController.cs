using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject[] spawnableUIPrefabs;
    [SerializeField] private GameObject briefingPanel;
    private List<GameObject> currentlySpawnablePrefabs = new List<GameObject>();
    private int currentLevel = 1;
    private GameStates currentState = GameStates.INIT_GAME;

    private void StartCurrentLevel() {
        InitSpawnableUIsForLevel();
        BroadcastMessage("PlaySound", "MorningCrow", SendMessageOptions.DontRequireReceiver);
        currentState = GameStates.BRIEFING;
        briefingPanel.SetActive(true);
    }

    private void InitSpawnableUIsForLevel() {
        currentlySpawnablePrefabs.Clear();
        foreach (var spawnableUIPrefab in spawnableUIPrefabs) {
            if (spawnableUIPrefab.GetComponent<LevelRequirement>().IsValidForLevel(currentLevel)) {
                currentlySpawnablePrefabs.Add(spawnableUIPrefab);
            }
        }
    }
    
	void Start () {
        StartCurrentLevel();
    }
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
            case GameStates.WORKING:
                Working();
                break;
        }
	}

    public void StartWorking() {
        briefingPanel.SetActive(false);
        currentState = GameStates.WORKING;
    }

    private void Working() {

    }
}
