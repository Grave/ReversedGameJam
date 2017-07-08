using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText : MonoBehaviour {
    const string PREFIX = "You ended\nhumanity after\n";
    const string POSTFIX = " day \n at the job!";

    [SerializeField] private Text endText;
    [SerializeField] private float letterTime = 1.0f;
    private string text;
    private int currentIndex = 1;

	void Start () {
        text = PREFIX + GameController.Instance.CurrentLevel + POSTFIX;
		var failureReasons = GameController.Instance.GetFailureReasons ();

		text += "\n";
		foreach (string reason in failureReasons) {
			text += reason;
		}

        IEnumerator coroutine = TypeEndText();
        StartCoroutine(coroutine);
    }

    private IEnumerator TypeEndText() {
        while (currentIndex <= text.Length) {
            yield return new WaitForSeconds(letterTime);
            endText.text = text.Substring(0, currentIndex);
            ++currentIndex;
        }
    }

// Update is called once per frame
void Update () {
		
	}
}
