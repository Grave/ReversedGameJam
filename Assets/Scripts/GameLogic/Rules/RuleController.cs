using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleController
{
	public int currentDayCache;
	private List<string> variationsAdded;

	//Variation pools
	private List<ButtonColorAdj> buttonColors;

	//private List<Rule> rules;

	public RuleController()
	{
		buttonColors = new List<ButtonColorAdj> ();
		variationsAdded = new List<string> ();
	}

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
				buttonColors.Add (ButtonColorAdj.GREEN);
				variationsAdded.Add ("New button color: Green\n");
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
