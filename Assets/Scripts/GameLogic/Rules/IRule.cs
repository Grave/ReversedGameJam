using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRule
{
	void RandomizeFrom (VariationPoolForRules availableVariations);
	void Test (GameObject obj, RuleVeredict veredict);

	string GetDescription();
}
