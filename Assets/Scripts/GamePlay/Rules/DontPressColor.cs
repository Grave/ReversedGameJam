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

	public int Score (GameObject obj)
	{
		ButtonColor color = obj.GetComponent<ButtonColor>();
		if (color) 
		{
			return color.GetAdj() == colorAdj ? -1 : 0;
		}

		return 0;
	}

	public string GetDescription ()
	{
		return ButtonColorTranslator.GetDescriptionFor(colorAdj) + " buttons should not be pressed!";
	}
	#endregion
	
}
