using System.Collections;
using System;
using UnityEngine;

public enum PressedTime
{
	ODD,
	EVEN,
	MORNING,
	AFTERNOON
}

public class PressedTimeTranslator
{
	private static string[] description = new string[]{ "odd", "even", "morning", "afternoon" };

	public static string GetDescriptionFor(PressedTime time)
	{
		return description [(int)time];
	}

	public static bool TimeSatisfies(DateTime time, PressedTime expectedTime)
	{
		switch (expectedTime) {
		case PressedTime.ODD: return time.Hour % 2 == 1;
		case PressedTime.EVEN: return time.Hour % 2 == 0;
		case PressedTime.MORNING: return time.Hour < 12;
		case PressedTime.AFTERNOON: return time.Hour >= 12;
		}

		return false;
	}
}