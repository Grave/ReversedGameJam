using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRule
{
	void RandomizeFrom (VariationPoolForRules availableVariations);
	int Score (GameObject obj);

	string GetDescription();
}
