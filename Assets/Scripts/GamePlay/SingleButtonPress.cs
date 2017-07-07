using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleButtonPress : MonoBehaviour 
{
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
				textToUpdate.text = "PRESS THE BUTTON! " + timer.ToString("F2");

				yield return null;

			} while(timer > 0 && !buttonPressed);

			if (!buttonPressed)
			{
				GameOver();
			}

			var extraTime = Mathf.Max(0, timer);
			timer = 0;

			textToUpdate.text = "";
			yield return new WaitForSeconds(activationInterval + extraTime);
		}
	}

	bool IsActive()
	{
		return timer > 0;
	}

	public void OnButtonClicked()
	{
		if (!IsActive()) 
		{
			GameOver ();
		}
		else 
		{
			buttonPressed = true;
		}
	}

	void GameOver()
	{
		Destroy(window.gameObject);
	}
}
