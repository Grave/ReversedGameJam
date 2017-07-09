using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedFlash : MonoBehaviour {

    [SerializeField] Image redShade;
    [SerializeField] private float flashTime = 1.0f;
    [SerializeField] private float maxFlash = 0.8f;

    private bool flash = false;
    private float currentFlashTime = 0.0f;
	
	// Update is called once per frame
	void Update () {
		if (flash) {
            Flashing();
        }
	}

    public void StartFlashing() {
        flash = true;
        currentFlashTime = 0.0f;
    }

    public void StopFlashing() {
        flash = false;
        SetShadeAlpha(0.0f);
    }

    private void Flashing() {
        currentFlashTime += Time.deltaTime;
        while (currentFlashTime > flashTime) {
            currentFlashTime -= flashTime;
        }

        SetShadeAlpha(maxFlash * currentFlashTime / flashTime);
    }

    private void SetShadeAlpha(float alpha) {
        Color col = redShade.color;
        col.a = alpha;
        redShade.color = col;
    }
}
