using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonColorAdj
{
	GREEN,
	RED,
	WHITE,
	BLUE
}

public class ButtonColorTranslator
{
	private static string[] description = new string[]{ "gree", "red", "white", "blue" };

	public static string GetDescriptionFor(ButtonColorAdj color)
	{
		return description [(int)color];
	}

	public static Color GetColorFrom(ButtonColorAdj color)
	{
		switch (color) {
		case ButtonColorAdj.GREEN: return Color.green;
		case ButtonColorAdj.RED: return Color.red;
		case ButtonColorAdj.WHITE: return Color.white;
		case ButtonColorAdj.BLUE: return Color.blue;
		}

		return Color.black;
	}
}

public class ButtonColor : MonoBehaviour
{
	[SerializeField] private Image toOverride;
	private ButtonColorAdj adj;

	public void SetColorAdj(ButtonColorAdj adj)
	{
		toOverride.color = ButtonColorTranslator.GetColorFrom (adj);
		this.adj = adj;
	}

	public ButtonColorAdj GetAdj()
	{
		return adj;
	}
}