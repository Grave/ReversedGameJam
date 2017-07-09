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

	public void Test(GameObject obj, RuleVeredict veredict)
	{
		DateTime time = GameController.Instance.GetCurrentTime ();

		ButtonColor color = obj.GetComponent<ButtonColor>();
		if (color && color.GetAdj () == colorAdj) 
		{
			if (PressedTimeTranslator.TimeSatisfies (time, timeAdj)) {
				veredict.AddScore (1);
			} else {
				veredict.AddScore (-1);
				veredict.AddFailureReason ("Pressed button at wrong time of day");
			}
		}
	}

	public string GetDescription ()
	{
		var colorDescription = ButtonColorTranslator.GetDescriptionFor(colorAdj);
		var timeDescription = PressedTimeTranslator.GetDescriptionFor(timeAdj);

		return colorDescription + " buttons should pressed only in " + timeDescription + "!";
	}
	#endregion

}
