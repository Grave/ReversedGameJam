using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontPressColor : IRule 
{
	private ButtonColorAdj colorAdj;

	#region IRule implementation
	public void RandomizeFrom (VariationPoolForRules variationsToUse)
	{
		colorAdj = variationsToUse.PopRandomColor ();
	}

	public void Test(GameObject obj, RuleVeredict veredict)
	{
		ButtonColor color = obj.GetComponent<ButtonColor>();
		if (color) 
		{
			if (color.GetAdj () == colorAdj) {
				veredict.AddScore (-1);
				veredict.AddFailureReason ("Did not ignore button that should be ignored");
			}
		}
	}

	public string GetDescription ()
	{
		return ButtonColorTranslator.GetDescriptionFor(colorAdj) + " buttons should not be pressed!";
	}
	#endregion
	
}
