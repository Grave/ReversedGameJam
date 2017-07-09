using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialOptionsOverride : MonoBehaviour 
{
	public void InitRadialOptions(List<string> options)
	{
		RadialSelectButtonPress radialController = GetComponentInChildren<RadialSelectButtonPress> ();
		radialController.Init (options);
	}
} 