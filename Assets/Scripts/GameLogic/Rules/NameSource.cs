using UnityEngine;
using UnityEngine.UI;

public class NameSource : MonoBehaviour 
{
	[SerializeField] private Text textField;
	[SerializeField] private string fluffTextAfterName;

	private string nameSource;

	public void SetNameSource(string newName)
	{
		nameSource = newName;
		textField.text = newName + " " + fluffTextAfterName;
	}

	public string GetNameSource()
	{
		return nameSource;
	}
}