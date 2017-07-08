using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariationPoolForRules
{
	private List<ButtonColorAdj> colorAdj;
	private List<PressedTimeAdj> timeAdj;
	private List<string> allNames;

	public VariationPoolForRules(IVariationContainer copyFrom)
	{
		colorAdj = new List<ButtonColorAdj> (copyFrom.GetButtonColors ());
		timeAdj = new List<PressedTimeAdj> (copyFrom.GetPressedTimes ());
		allNames = new List<string> (copyFrom.GetAllNames ());
	}

	public ButtonColorAdj PopRandomColor()
	{
		return PopRandomFrom (colorAdj);
	}

	public PressedTimeAdj PopRandomPressTime()
	{
		return PopRandomFrom (timeAdj);
	}

	public string PopRandomName()
	{
		return PopRandomFrom (allNames);
	}

	private T PopRandomFrom<T>(List<T> list)
	{
		int rngIndex = Random.Range (0, list.Count);
		var elm = list [rngIndex];
		list.RemoveAt (rngIndex);

		return elm;
	}
}
