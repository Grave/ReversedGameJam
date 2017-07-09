﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSelectButtonPress : MonoBehaviour {

    [SerializeField] private bool pressOnlyOnce = false;
    [SerializeField] private string buttonInteractionText = "PRESS THE BUTTON: ";
    [SerializeField] private RotationButton rotationButton;

    [SerializeField] private string[] options = { "North", "West", "East", "South"};
    private string currentNeededSelection;

    public float activationInterval;
    public float actionTime;
    public Text textToUpdate;

    private float timer;
    private bool buttonPressed;
    private Window window;

    // Use this for initialization
    void Start() {
        rotationButton.SetOptions(options);
        currentNeededSelection = options[Random.Range(0, options.Length)];
        window = GetComponentInParent<Window>();
        StartCoroutine("StartLoop");
    }

    void OnDestroy() {
        StopAllCoroutines();
    }

    IEnumerator StartLoop() {
        while (true) {
            buttonPressed = false;
            timer = actionTime + Time.deltaTime;

            do {
                timer -= Time.deltaTime;
                textToUpdate.text = "Select : " + currentNeededSelection + ", " + buttonInteractionText + timer.ToString("F2");

                yield return null;

            } while (timer > 0 && !buttonPressed);

            var veredict = GetRuleVeredict();
            if (!buttonPressed && veredict.Pass()) {
                GameOver(CompileFailureReasonFor("Button was not pressed in time"));
            }

            if (pressOnlyOnce) {
                Destroy(window.gameObject);
            } else {
                var extraTime = Mathf.Max(0, timer);
                timer = 0;

                textToUpdate.text = "";

                ShuffleSelection();
                yield return new WaitForSeconds(activationInterval + extraTime);
            }
        }
    }

    private void ShuffleSelection() {
        rotationButton.SetOptions(options);
        currentNeededSelection = options[Random.Range(0, options.Length)];
    }

    bool IsActive() {
        return timer > 0;
    }

    public void OnButtonClicked() {
        var veredict = GetRuleVeredict();

        if (!veredict.Pass()) {
            GameOver(veredict.GetFailureReasons());
            return;
        }

        if (!IsActive()) {
            GameOver(CompileFailureReasonFor("Button pressed too soon"));
        } else if (rotationButton.GetSelected() == currentNeededSelection) { 
            buttonPressed = true;
        } else {
            GameOver(CompileFailureReasonFor("Button pressed with wrong Selection"));
        }
    }

    private List<string> CompileFailureReasonFor(string reason) {
        var failureReason = new List<string>();
        failureReason.Add(reason);
        return failureReason;
    }

    RuleVeredict GetRuleVeredict() {
        return GameController.Instance.ScoreAccordingToRules(window.gameObject);
    }

    void GameOver(List<string> failureReason) {
        GameController.Instance.StartRockets(failureReason);
        Destroy(window.gameObject);
    }
}
