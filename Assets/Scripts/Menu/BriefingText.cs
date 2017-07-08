using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BriefingText : MonoBehaviour 
{
	[SerializeField] private Text briefingText;
	
	public void SetBriefingTextTo(string text)
	{
		briefingText.text = text;
	}
}
