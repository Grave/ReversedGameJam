using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBlackboard : MonoBehaviour 
{
	private Dictionary<string, float> numerical;
	private Dictionary<string, string> categorical;

	void Awake()
	{
		numerical = new Dictionary<string, float> ();
		categorical = new Dictionary<string, string> ();
	}

	public void Store(string key, float value)
	{
		numerical[key] = value;
	}

	public void Store(string key, string category)
	{
		categorical[key] = category;
	}

	public float GetNumeric(string key)
	{
		return numerical [key];
	}

	public string GetCategoric(string key)
	{
		return categorical [key];
	}
}
