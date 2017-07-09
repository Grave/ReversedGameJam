using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialOptionsOverride : MonoBehaviour 
{
	public void InitRadialOptions(List<string> options, bool canUseYAxis, bool canInvert)
	{
		bool useXAxis = canUseYAxis ? Random.value >= 0.5f : true;
		bool invert = canInvert ? Random.value >= 0.5f : true;

		RadialSelectButtonPress radialController = GetComponentInChildren<RadialSelectButtonPress> ();
		radialController.Init (options, useXAxis, invert);
	}
} 