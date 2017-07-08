using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVariationContainer
{
	List<ButtonColorAdj> GetButtonColors ();
	List<PressedTimeAdj> GetPressedTimes ();
}
