using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariationPoolForRules
{
	private List<ButtonColorAdj> colorAdj;

	public VariationPoolForRules(IVariationContainer copyFrom)
	{
		colorAdj = new List<ButtonColorAdj> (copyFrom.GetButtonColors ());
	}

	public ButtonColorAdj PopRandomColor()
	{
		return PopRandomFrom (colorAdj);
	}

	private T PopRandomFrom<T>(List<T> list)
	{
		int rngIndex = Random.Range (0, list.Count);
		var elm = list [rngIndex];
		list.RemoveAt (rngIndex);

		return elm;
	}
}
