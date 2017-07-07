using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : JamUtilities.MonoSingleton<GameController> {

    [SerializeField] private GameObject[] spawnableUIPrefabs;
    [SerializeField] private GameObject briefingPanel;
    [SerializeField] private Image fadeIn;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeInClickableCutOff = 0.5f;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private float rocketExplosionDelay = 3.0f;
    [SerializeField] private float delayBetweenSpawns = 2.0f;
    [SerializeField] private GameObject canvas;

    private List<GameObject> currentlySpawnablePrefabs = new List<GameObject>();
    private int currentLevel = 1;
    public int CurrentLevel {
        get {
            return currentLevel;
        }
    }
    private GameStates currentState = GameStates.INIT_GAME;
    private float delayUntilNextSpawn = 2.0f;
    private float currentFadeInTime;

    private void StartCurrentLevel() {
        InitSpawnableUIsForLevel();
        BroadcastMessage("PlaySound", "MorningCrow", SendMessageOptions.DontRequireReceiver);
        currentState = GameStates.BRIEFING;
        briefingPanel.SetActive(true);
        delayUntilNextSpawn = GetCurrentDelayBetweenSpanws();

        ShowFadeIn();
    }

    private void ShowFadeIn() {
        SetFadeInAlpha(1.0f);
        currentFadeInTime = fadeInTime;
    }

    private void SetFadeInAlpha(float alpha) {
        Vector4 color = fadeIn.color;
        color.w = alpha;
        fadeIn.color = color;
        if (alpha > fadeInClickableCutOff) {
            fadeIn.raycastTarget = true;
        } else {
            fadeIn.raycastTarget = false;
        }
    }

    private float GetCurrentDelayBetweenSpanws() {
        return delayBetweenSpawns;
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
            case GameStates.BRIEFING:
                Briefing();
                break;
            case GameStates.WORKING:
                Working();
                break;
        }
	}

    private void Briefing() {
        currentFadeInTime -= Time.deltaTime;
        if (currentFadeInTime > 0.0f) {
            SetFadeInAlpha(currentFadeInTime / fadeInTime);
        } else {
            SetFadeInAlpha(0.0f);
        }
    }

    public void StartWorking() {
        briefingPanel.SetActive(false);
        currentState = GameStates.WORKING;
        SetFadeInAlpha(0.0f);
    }

    private void Working() {
        delayUntilNextSpawn -= Time.deltaTime;
        if (delayUntilNextSpawn <= 0.0f) {
            delayUntilNextSpawn = GetCurrentDelayBetweenSpanws();
            SpawnUI();
        }
    }

    private void SpawnUI() {
        if (currentlySpawnablePrefabs.Count == 0) {
            return;
        }

        int spawnIndex = Random.Range(0, currentlySpawnablePrefabs.Count);
        Instantiate<GameObject>(currentlySpawnablePrefabs[spawnIndex],canvas.transform);
    }

    public void NukeButtonPressed() {
        StartRockets();
    }

    public void StartRockets() {
        if (currentState != GameStates.WORKING) {
            return;
        }

        currentState = GameStates.ROCKET_STARTING;
        BroadcastMessage("PlaySound", "Alarm", SendMessageOptions.DontRequireReceiver);

        IEnumerator coroutine = Explode(rocketExplosionDelay);
        StartCoroutine(coroutine);
    }

    private IEnumerator Explode(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        BroadcastMessage("PlaySound", "Boom", SendMessageOptions.DontRequireReceiver);
        endPanel.SetActive(true);
    }

}
