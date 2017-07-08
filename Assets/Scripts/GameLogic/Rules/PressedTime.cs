using System.Collections;
using System;
using UnityEngine;

public enum PressedTimeAdj
{
	ODD,
	EVEN,
	MORNING,
	AFTERNOON
}

public class PressedTimeTranslator
{
	private static string[] description = new string[]{ "odd times", "even times", "the morning", "the afternoon" };

	public static string GetDescriptionFor(PressedTimeAdj time)
	{
		return description [(int)time];
	}

	public static bool TimeSatisfies(DateTime time, PressedTimeAdj expectedTime)
	{
		switch (expectedTime) {
		case PressedTimeAdj.ODD: return time.Hour % 2 == 1;
		case PressedTimeAdj.EVEN: return time.Hour % 2 == 0;
		case PressedTimeAdj.MORNING: return time.Hour < 12;
		case PressedTimeAdj.AFTERNOON: return time.Hour >= 12;
		}

		return false;
	}
}