using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleVeredict
{
	private int score;
	private List<string> failureReasons;

	public RuleVeredict()
	{
		score = 0;
		failureReasons = new List<string>();
	}

	public void AddScore(int toAdd)
	{
		score += toAdd;
	}

	public bool Pass()
	{
		return score >= 0;
	}

	public void AddFailureReason(string reason)
	{
		failureReasons.Add (reason);
	}

	public List<string> GetFailureReasons()
	{
		return failureReasons;
	}
}
