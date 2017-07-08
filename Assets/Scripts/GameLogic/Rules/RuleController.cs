using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleController : IVariationContainer
{
	public int currentDayCache;
	public int maxRulesAllowed;
	private List<string> variationsAdded;

	//Variation pools
	private List<ButtonColorAdj> buttonColors;

	private List<IRule> availableRules;
	private List<IRule> selectedRules;

	public RuleController()
	{
		currentDayCache = 0;
		maxRulesAllowed = 0;

		variationsAdded = new List<string> ();

		buttonColors = new List<ButtonColorAdj> ();

		availableRules = new List<IRule> ();
		selectedRules = new List<IRule> ();
	}

	#region IVariationContainer implementation

	public List<ButtonColorAdj> GetButtonColors ()
	{
		return buttonColors;
	}

	#endregion

	public void OnDay(int dayNumber)
	{
		currentDayCache = dayNumber;
		variationsAdded.Clear ();

		switch (dayNumber) {
		case 1:
			{
				buttonColors.Add (ButtonColorAdj.WHITE);
				buttonColors.Add (ButtonColorAdj.RED);

				variationsAdded.Add ("New button color: White\n");
				variationsAdded.Add ("New button color: Red\n");
				break;
			}
		case 2:
			{
				++maxRulesAllowed;
				buttonColors.Add (ButtonColorAdj.GREEN);
				variationsAdded.Add ("New button color: Green\n");

				availableRules.Add (new DontPressColor ());

				break;
			}
		}

		CreateRules();
	}

	private T GetRandomFrom<T>(List<T> list)
	{
		int rngIndex = Random.Range (0, list.Count);
		return list [rngIndex];
	}

	public void RandomizeObject(GameObject obj)
	{
		ButtonColor btnColor = obj.GetComponent<ButtonColor> ();
		if (btnColor) {
			btnColor.SetColorAdj (GetRandomFrom (buttonColors));
		}
	}

	void CreateRules()
	{
		selectedRules.Clear ();

		var variationPool = new VariationPoolForRules (this);
		var usableRules = new List<IRule> (availableRules);

		for (int i = 0; i < maxRulesAllowed; ++i) 
		{
			int rngIndex = Random.Range (0, usableRules.Count);
			var rule = usableRules[rngIndex];
			usableRules.RemoveAt (rngIndex);

			rule.RandomizeFrom (variationPool);
			variationsAdded.Add(rule.GetDescription ());
			selectedRules.Add (rule);
		}
	}

	public string GetCreatedRuleDescriptions()
	{
		string description = "";

		foreach(string variation in variationsAdded)
		{
			description += variation;
		}

		return description;
	}
}
