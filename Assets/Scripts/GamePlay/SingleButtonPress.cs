using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleButtonPress : MonoBehaviour 
{
    [SerializeField] private bool pressOnlyOnce = false;
    [SerializeField] private string buttonInteractionText = "PRESS THE BUTTON: ";

    public float activationInterval;
	public float actionTime;
	public Text textToUpdate;

	private float timer;
	private bool buttonPressed;
	private Window window;

	// Use this for initialization
	void Start () 
	{
		window = GetComponentInParent<Window>();
		StartCoroutine("StartLoop");
	}

	void OnDestroy()
	{
		StopAllCoroutines();
	}

	IEnumerator StartLoop()
	{
		while (true) 
		{
			buttonPressed = false;
			timer = actionTime + Time.deltaTime;

			do {
				timer -= Time.deltaTime;
				textToUpdate.text = buttonInteractionText + timer.ToString("F2");

				yield return null;

			} while(timer > 0 && !buttonPressed);

			if (!buttonPressed && ShouldBePressed())
			{
				GameOver();
			}

            if (pressOnlyOnce) {
                Destroy(window.gameObject);
            } else {
                var extraTime = Mathf.Max(0, timer);
                timer = 0;

                textToUpdate.text = "";
                yield return new WaitForSeconds(activationInterval + extraTime);
            }
		}
	}

	bool IsActive()
	{
		return timer > 0;
	}

	public void OnButtonClicked()
	{
		if (!ShouldBePressed()) 
		{
			GameOver ();
			return;
		}

		if (!IsActive())
		{
			GameOver ();
		}
		else 
		{
			buttonPressed = true;
		}
	}

	bool ShouldBePressed()
	{
		int score = GameController.Instance.ScoreAccordingToRules (window.gameObject);
		return score >= 0;
	}

	void GameOver()
	{
        GameController.Instance.StartRockets();
		Destroy(window.gameObject);
	}
}
