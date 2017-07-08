using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressColorAtTime : IRule 
{
	private ButtonColorAdj colorAdj;
	private PressedTimeAdj timeAdj;

	#region IRule implementation
	public void RandomizeFrom (VariationPoolForRules variationsToUse)
	{
		colorAdj = variationsToUse.PopRandomColor ();
		timeAdj = variationsToUse.PopRandomPressTime ();
	}

	public int Score (GameObject obj)
	{
		DateTime time = GameController.Instance.GetCurrentTime ();

		ButtonColor color = obj.GetComponent<ButtonColor>();
		if (color && color.GetAdj () == colorAdj) 
		{
			return PressedTimeTranslator.TimeSatisfies (time, timeAdj) ? 1 : -1;
		}

		return 0;
	}

	public string GetDescription ()
	{
		var colorDescription = ButtonColorTranslator.GetDescriptionFor(colorAdj);
		var timeDescription = PressedTimeTranslator.GetDescriptionFor(timeAdj);

		return colorDescription + " buttons should pressed only in " + timeDescription + "!\n";
	}
	#endregion

}
