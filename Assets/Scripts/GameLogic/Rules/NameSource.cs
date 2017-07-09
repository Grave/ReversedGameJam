using UnityEngine;
using UnityEngine.UI;

public class NameSource : MonoBehaviour 
{
	[SerializeField] private Text textField;
	[SerializeField] private string[] fluffTextAfterName;

	private string nameSource;

	private string GetRandomFluffText()
	{
		return fluffTextAfterName [Random.Range (0, fluffTextAfterName.Length)];
	}

	public void SetNameSource(string newName)
	{
		nameSource = newName;
		textField.text = newName + " " + GetRandomFluffText();
	}

	public string GetNameSource()
	{
		return nameSource;
	}
}