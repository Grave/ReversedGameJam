using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreMessageFrom : IRule 
{
	private string nameToIgnore;

	#region IRule implementation
	public void RandomizeFrom (VariationPoolForRules variationsToUse)
	{
		nameToIgnore = variationsToUse.PopRandomName ();
	}

	public void Test(GameObject obj, RuleVeredict veredict)
	{
		NameSource nameSrc = obj.GetComponent<NameSource>();
		if (nameSrc) 
		{
			if (nameSrc.GetNameSource() == nameToIgnore) {
				veredict.AddScore (-1);
				veredict.AddFailureReason ("You did not ignore message from " + nameToIgnore);
			}
		}
	}

	public string GetDescription ()
	{
		return "Messages from " + nameToIgnore + " should be ignored\n";
	}
	#endregion

}