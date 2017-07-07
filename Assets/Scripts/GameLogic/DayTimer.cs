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
	private DateTime currentTimestamp;
	private float intervalTime;

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

	public void StartCounter(float roundTime)
	{
		TimeSpan daySpan = endTime - startTime;
		var totalSteps = daySpan.TotalMinutes / step.TotalMinutes;

		intervalTime = roundTime / (float)totalSteps;

		StartCoroutine ("CountTime");
	}

	void UpdateTextFieldWith(DateTime timeStamp)
	{
		textField.text = timeStamp.ToString("hh:mm tt");
	}

	IEnumerator CountTime()
	{
		currentTimestamp = startTime;

		do 
		{
			UpdateTextFieldWith(currentTimestamp);

			yield return new WaitForSeconds(intervalTime);
			currentTimestamp = currentTimestamp.Add(step);

		} while (currentTimestamp < endTime);
	}
}
