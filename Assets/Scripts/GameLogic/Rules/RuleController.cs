using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleController : IVariationContainer
{
	public int currentDayCache;
	public int maxRulesAllowed;
	private List<string> briefingText;

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

		briefingText = new List<string> ();

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
		briefingText.Clear ();

		switch (dayNumber) {
		case 1:
			{
				briefingText.Add ("Welcome to your first day on the job! Don't fuck it up..\n");

				buttonColors.Add (ButtonColorAdj.WHITE);
				oneShotButtonNames.Add ("Boss");

				briefingText.Add (" * White buttons are the best! You can use those now");
				briefingText.Add (" * New message Source: Boss");

				break;
			}
		case 2:
			{
				briefingText.Add ("Hey there! Don't worry, your boss wont be sending you any more messages. Let's keep the world safe together!\n");

				buttonColors.Add (ButtonColorAdj.RED);
				oneShotButtonNames.Clear ();
				oneShotButtonNames.Add ("Phillip");
				oneShotButtonNames.Add ("Guilherme");

				briefingText.Add (" * Message source removed: Boss");
				briefingText.Add (" * New message Source: Phillip");
				briefingText.Add (" * New message Source: Guilherme");

				briefingText.Add (" * Scientists have found Red colored buttons to improve efficiency. We have taken the liberty to add those to your repertoire");
				briefingText.Add (" * New Possible Day Rule: IT reports system faults that might result in wrong call for actions, thus certain messages might need to be ignored.");

				++maxRulesAllowed;
				availableRules.Add (new IgnoreMessageFrom ());

				break;
			}
		case 3:
			{
				briefingText.Add ("You are doing great! Keep this up and promotion is guaranteed :D\n");

				persistantButtonNames.Add ("Cooling reactor #NaN");

				briefingText.Add (" * You are now responsible for a new group of systems: Cooling reactors");

				break;
			}
		case 4:
			{
				buttonColors.Add (ButtonColorAdj.BLUE);
				persistantButtonNames.Add ("Heating reactor #NaN");

				briefingText.Add ("Did you see that paper on button colors?\n");
				briefingText.Add (" * According to important sources, blue buttons should be an integral part of every company! We added those.");

				briefingText.Add (" * You are now responsible for a new group of systems: Heating reactors");
				briefingText.Add (" * New Possible Day Rule: Depending on Zodiac we might need to ignore clicking certain button colors.");

				availableRules.Add (new DontPressColor ());

				break;
			}
		case 5:
			{
				briefingText.Add ("You are doing so great, surely you can handle more?\n");

				oneShotButtonNames.Add ("Radu");
				persistantButtonNames.Add ("Coffee machine #NaN");

				++maxRulesAllowed;

				briefingText.Add (" * New message Source: Radu");
				briefingText.Add (" * You are now responsible for a new group of systems: Coffee machines");

				briefingText.Add (" * Henceforth 2 daily rules shall be in place to improve efficiency");

				break;
			}
		case 6:
			{
				briefingText.Add ("That was great coffee yesterday, life is good. Check out this report on efficient button clicking times!\n");

				pressTimes.Add (PressedTimeAdj.MORNING);
				pressTimes.Add (PressedTimeAdj.AFTERNOON);

				availableRules.Add (new PressColorAtTime ());

				briefingText.Add (" * New Possible Day Rule: Certain button colors should only be pressed at certain times");
				briefingText.Add (" * New supported efficient times: Morning, Afternoon");

				break;
			}
		case 7:
			{
				++maxRulesAllowed;

				oneShotButtonNames.Add ("Mike");
				persistantButtonNames.Add ("Toilet #NaN");

				radialOptions.Add ("Blame Phillip");
				radialOptions.Add ("Ask mom");

				briefingText.Add ("Round round round we go\n");

				briefingText.Add (" * New message Source: Mike");
				briefingText.Add (" * You are now responsible for a new group of systems: Toilets");
				briefingText.Add (" * New System control type: Radial Buttons");
				briefingText.Add (" * New Radial option: Blame Phillip");
				briefingText.Add (" * New Radial option: Ask Mom");
				briefingText.Add (" * Henceforth 3 daily rules shall be in place to improve efficiency");

				break;
			}
		case 8:
			{
				radialOptions.Add ("Cool it");
				radialOptions.Add ("Bop it");

				briefingText.Add ("More rounds!\n");

				briefingText.Add (" * New Radial option: Cool it");
				briefingText.Add (" * New Radial option: Bop it");

				break;
			}
		case 9:
			{
				radialOptions.Add ("Ignore");
				radialOptions.Add ("Consult magic 8 ball");

				briefingText.Add ("Things are starting to heat up\n");

				briefingText.Add (" * New Radial option: Ignore");
				briefingText.Add (" * New Radial option: Consult magic 8 ball");
				briefingText.Add (" * New Radial control: use Y axis for increase efficiency");

				radialYaxisUnlocked = true;
				break;
			}
		case 10:
			{
				maxRulesAllowed +=2 ;

				buttonColors.Add (ButtonColorAdj.GREEN);

				pressTimes.Add (PressedTimeAdj.MORNING);
				pressTimes.Add (PressedTimeAdj.AFTERNOON);

				radialNormalUnlocked = true;

				briefingText.Add ("Oh boy, we have new management and they expect much of us :(\n");

				briefingText.Add (" * Management likes green... new button colors!");
				briefingText.Add (" * We now get radial systems from a different branch that uses inverted controls...");
				briefingText.Add (" * R&D has found odd/even hour combinations for increased efficiency, these might pop up form now on");

				briefingText.Add (" * Henceforth 5 daily rules shall be in place to improve efficiency");
				break;
			}
		default:
			{
				
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

		if (maxRulesAllowed > 0)
			briefingText.Add ("\nRules for this day:");

		for (int i = 0; i < maxRulesAllowed; ++i) 
		{
			int rngIndex = Random.Range (0, usableRules.Count);
			var rule = usableRules[rngIndex];
			usableRules.RemoveAt (rngIndex);

			rule.RandomizeFrom (variationPool);
			briefingText.Add("* " + rule.GetDescription ());
			selectedRules.Add (rule);
		}
	}

	public string GetCreatedRuleDescriptions()
	{
		string description = "";

		foreach(string variation in briefingText)
		{
			description += variation + "\n";
		}

		return description;
	}
}
