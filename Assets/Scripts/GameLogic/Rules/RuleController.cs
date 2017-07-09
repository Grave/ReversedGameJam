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
	private List<PressedTimeAdj> pressTimes;
	private List<string> persistantButtonNames;
	private List<string> oneShotButtonNames;
	private List<string> radialOptions;

	bool radialYaxisUnlocked;
	bool radialNormalUnlocked;

	private List<IRule> availableRules;
	private List<IRule> selectedRules;

	public RuleController()
	{
		currentDayCache = 0;
		maxRulesAllowed = 0;

		variationsAdded = new List<string> ();

		buttonColors = new List<ButtonColorAdj> ();
		pressTimes = new List<PressedTimeAdj> ();
		persistantButtonNames = new List<string> ();
		oneShotButtonNames = new List<string> ();
		radialOptions = new List<string> ();

		radialYaxisUnlocked = false;
		radialNormalUnlocked = false;

		availableRules = new List<IRule> ();
		selectedRules = new List<IRule> ();
	}

	#region IVariationContainer implementation

	public List<ButtonColorAdj> GetButtonColors ()
	{
		return buttonColors;
	}

	public List<PressedTimeAdj> GetPressedTimes ()
	{
		return pressTimes;
	}

	public List<string> GetAllNames ()
	{
		List<string> allNames = new List<string> ();

		allNames.AddRange(persistantButtonNames);
		allNames.AddRange(oneShotButtonNames);

		return allNames;
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
				oneShotButtonNames.Add ("Guilherme");
				persistantButtonNames.Add ("Cooling reactor");
				radialOptions.Add ("Cooling reactor #3");
				radialOptions.Add ("Blame Phillip");
				radialNormalUnlocked = true;
				radialYaxisUnlocked = true;

				variationsAdded.Add ("New button color: White\n");
				variationsAdded.Add ("New button color: Red\n");
				variationsAdded.Add ("New message Source: Guilherme\n");
				variationsAdded.Add ("New systems: Cooling reactors\n");

				pressTimes.Add (PressedTimeAdj.MORNING);

				availableRules.Add (new IgnoreMessageFrom ());
				++maxRulesAllowed;

				break;
			}
		case 2:
			{
				++maxRulesAllowed;
				buttonColors.Add (ButtonColorAdj.GREEN);
				oneShotButtonNames.Add ("Phillip");
				persistantButtonNames.Add ("Heating reactor");
				radialOptions.Add ("Cooling reactor #2");
				radialOptions.Add ("Blame Guilherme");
				radialOptions.Add ("Ask Mom");

				variationsAdded.Add ("New button color: Green\n");
				variationsAdded.Add ("New message Source: Phillip\n");
				variationsAdded.Add ("New systems: Heating reactors\n");

				pressTimes.Add (PressedTimeAdj.EVEN);

				availableRules.Add (new DontPressColor ());
				availableRules.Add (new PressColorAtTime ());

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

		NameSource nameSrc = obj.GetComponent<NameSource> ();
		ElementAttributes elmAttr = obj.GetComponent<ElementAttributes> ();

		if (nameSrc && elmAttr) {
			nameSrc.SetNameSource (GetRandomFrom (elmAttr.IsPersistent ? persistantButtonNames : oneShotButtonNames)); 
		}

		RadialOptionsOverride radial = obj.GetComponent<RadialOptionsOverride> ();
		if (radial) {
			radial.InitRadialOptions (radialOptions, radialYaxisUnlocked, radialNormalUnlocked);
		}
	}

	public RuleVeredict TestAgaisntRules(GameObject obj)
	{
		RuleVeredict veredict = new RuleVeredict ();

		foreach (var rule in selectedRules) 
		{
			rule.Test (obj, veredict);
		}

		return veredict;
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
