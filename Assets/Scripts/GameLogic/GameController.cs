﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : JamUtilities.MonoSingleton<GameController> {

    [SerializeField] private GameObject[] spawnableUIPrefabs;
	[SerializeField] private GameObject briefingPanel;
    [SerializeField] private GameObject endOfDayPanel;
    [SerializeField] private DayTimer timer;
    [SerializeField] private Image fadeIn;
    [SerializeField] private RedFlash redFlash;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime = 1.0f;
    [SerializeField] private float fadeInClickableCutOff = 0.5f;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private float rocketExplosionDelay = 3.0f;

    [SerializeField] private float[] startDelay;
    [SerializeField] private float[] delayBetweenSpawns;
    [SerializeField] private int[] maxPersistentElement;
    
    [SerializeField] private float roundTime = 60f;
    [SerializeField] private GameObject canvas;

	private float currentRoundTime;
    private int currentPresistentElements = 0;

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
    private float currentFadeOutTime;
	private RuleController ruleController = new RuleController();
	private List<string> failureReasons;

    public float GetRoundTimeNormalized()
	{
		return currentRoundTime / roundTime;
	}

	public List<string> GetFailureReasons()
	{
		return failureReasons;
	}

    private void StartCurrentLevel() {
        InitSpawnableUIsForLevel();
        BroadcastMessage("PlaySound", "MorningCrow", SendMessageOptions.DontRequireReceiver);
        currentState = GameStates.BRIEFING;
        briefingPanel.SetActive(true);
        delayUntilNextSpawn = GetLevelArrayValue(startDelay);

		ruleController.OnDay(currentLevel);
		var briefingText = briefingPanel.GetComponent<BriefingText> ();
		briefingText.SetBriefingTextTo (ruleController.GetCreatedRuleDescriptions ());

        ShowFadeIn();
    }

    private void ShowFadeIn() {
        SetFadeInAlpha(1.0f);
        currentFadeInTime = fadeInTime;
    }

	public System.DateTime GetCurrentTime()
	{
		return timer.GetCurrentTime ();
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
        return GetLevelArrayValue(delayBetweenSpawns);
    }

    private T GetLevelArrayValue<T>(T[] values) {
        int index = currentLevel - 1;
        index = Mathf.Clamp(index, 0, values.Length - 1);
        return values[index];
    }

    private void InitSpawnableUIsForLevel() {
        currentlySpawnablePrefabs.Clear();
        foreach (var spawnableUIPrefab in spawnableUIPrefabs) {
            if (spawnableUIPrefab.GetComponent<ElementAttributes>().IsValidForLevel(currentLevel)) {
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
				Working ();
				UpdateRoundTimer ();
	            break;
            case GameStates.DAY_ENDING:
                DayEnding();
                break;
        }
	}

    public void StartNextDay() {
        timer.SetTimerToStartTime();
        endOfDayPanel.SetActive(false);
        currentPresistentElements = 0;
        ++currentLevel;
        StartCurrentLevel();
    }

    private void Briefing() {
        currentFadeInTime -= Time.deltaTime;
        if (currentFadeInTime > 0.0f) {
            SetFadeInAlpha(currentFadeInTime / fadeInTime);
        } else {
            SetFadeInAlpha(0.0f);
        }
    }

    private void DayEnding() {
        currentFadeOutTime -= Time.deltaTime;
        if (currentFadeOutTime > 0.0f) {
            SetFadeInAlpha(1.0f - currentFadeOutTime / fadeOutTime);
        } else {
            SetFadeInAlpha(1.0f);
            currentState = GameStates.DAY_END;
        }
    }

    public void StartWorking() {
        briefingPanel.SetActive(false);
        currentState = GameStates.WORKING;
        SetFadeInAlpha(0.0f);

		currentRoundTime = 0;
		timer.StartCounter(roundTime);
    }

	void UpdateRoundTimer()
	{
		currentRoundTime += Time.deltaTime;

		if (currentRoundTime >= roundTime) {
            DestroyAllActiveWindows();
			currentState = GameStates.DAY_ENDING;
            currentFadeOutTime = fadeOutTime;
            endOfDayPanel.SetActive(true);
        }
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
        GameObject currentSpawnablePrefab = currentlySpawnablePrefabs[spawnIndex];
        var spawned = Instantiate<GameObject>(currentSpawnablePrefab, canvas.transform);

		ruleController.RandomizeObject (spawned);

        ElementAttributes eA = currentSpawnablePrefab.GetComponent<ElementAttributes>();
        if (eA.IsUnique) {
            currentlySpawnablePrefabs.RemoveAt(spawnIndex);
        }

        if (eA.IsPersistent) {
            ++currentPresistentElements;
            if (currentPresistentElements >= GetLevelArrayValue(maxPersistentElement)) {
                PurgetPersistentElements();
            }
        }
    }

    public void PurgetPersistentElements() {
        for (int i = 0; i <currentlySpawnablePrefabs.Count;) {
            GameObject currentSpawnablePrefab = currentlySpawnablePrefabs[i];
            ElementAttributes eA = currentSpawnablePrefab.GetComponent<ElementAttributes>();
            if (eA.IsPersistent) {
                currentlySpawnablePrefabs.RemoveAt(i);
            } else {
                ++i;
            }
        }
    }

    public void NukeButtonPressed() {
		var failureReason = new List<string> ();
		failureReason.Add ("You decided to end it yourself..");

		StartRockets(failureReason);
    }

	public void StartRockets(List<string> looseReasons) {
        if (currentState != GameStates.WORKING) {
            return;
        }

		failureReasons = looseReasons;
        DestroyAllActiveWindows();

        redFlash.StartFlashing();
        currentState = GameStates.ROCKET_STARTING;
        BroadcastMessage("PlaySound", "Alarm", SendMessageOptions.DontRequireReceiver);

        IEnumerator coroutine = Explode(rocketExplosionDelay);
        StartCoroutine(coroutine);
    }

    private void DestroyAllActiveWindows() {
        var windows = GameObject.FindObjectsOfType<Window>();
        foreach (var window in windows) {
            Destroy(window.gameObject);
        }
    }

    private IEnumerator Explode(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        redFlash.StopFlashing();
        BroadcastMessage("PlaySound", "Boom", SendMessageOptions.DontRequireReceiver);
        endPanel.SetActive(true);
    }

	public RuleVeredict ScoreAccordingToRules(GameObject obj)
	{
		return ruleController.TestAgaisntRules (obj);
	}
}
