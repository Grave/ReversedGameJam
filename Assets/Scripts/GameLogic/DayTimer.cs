using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour 
{
	public DateTime startTime;
	public DateTime endTime;
	public TimeSpan step;

	private Text textField;
	private int totalSteps;
	private DateTime currentTime;

	void Start()
	{
		startTime = new DateTime (1, 1, 1, 9, 0, 0);
		endTime = new DateTime (1, 1, 1, 17, 0, 0);
		step = new TimeSpan (0, 1, 0);

		textField = GetComponent<Text>();
	}

	public void ResetTime()
	{
		StopAllCoroutines();

	}

    public void SetTimerToStartTime() {
        UpdateTextFieldWith(startTime);
    }

	public void StartCounter(float roundTime)
	{
		TimeSpan daySpan = endTime - startTime;
		totalSteps = Mathf.RoundToInt((float)daySpan.TotalMinutes / (float)step.TotalMinutes);

		StartCoroutine ("CountTime");
	}

	void UpdateTextFieldWith(DateTime timeStamp)
	{
		textField.text = timeStamp.ToString("hh:mm tt");
	}

	public DateTime GetCurrentTime()
	{
		return currentTime;
	}

	IEnumerator CountTime()
	{
		GameController controller = GameController.Instance;
		float roundTimeNormalized = 0f;

		do 
		{
			roundTimeNormalized = controller.GetRoundTimeNormalized();
			var steps = Mathf.RoundToInt(Mathf.Lerp(0, totalSteps, roundTimeNormalized));
			currentTime = startTime.Add(TimeSpan.FromTicks(step.Ticks * steps));
			UpdateTextFieldWith(currentTime);

			yield return null;

		} while (roundTimeNormalized < 1);
	}
}
