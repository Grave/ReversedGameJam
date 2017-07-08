using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    private string[] introText = {
        "Eva Ross graduated high school with honors. She was voted most likely to succeed and science genius. She was already holding three patents before even entering college and graduated theoretical physics, aerospace engineering and social science in record time with record grades. To follow her patriotic duty she applied for a job in government, any job in government.",
        "Today all her dreams are finally coming true and she starts her job as a supervising defense expert.",
        "In a secret location for a secret project she is our countries last line of defense.",
        "She is a nuclear rocket button pusher. In case of an enemy attack she has to press the RED BUTTON." };

    private Text textField;
    private int currentText = 0;
    private int currentIndex = 0;
    private bool bottomsUp = false;

    [SerializeField] private float letterTime = 0.1f;
    [SerializeField] private float dramaticPauseTime = 2.0f;

    private void Awake() {
        textField = GetComponent<Text>();
    }

    void OnDestroy() {
        StopAllCoroutines();    
    }

    // Use this for initialization
    void Start () {
        IEnumerator coroutine = TypeEndText();
        StartCoroutine(coroutine);
    }

    private IEnumerator TypeEndText() {
        while (currentText < introText.Length) {
            while (currentIndex < introText[currentText].Length) {
                yield return new WaitForSeconds(letterTime);
                textField.text += introText[currentText][currentIndex];
                
                if (!bottomsUp && (textField.preferredHeight > textField.rectTransform.rect.height)) {
                    bottomsUp = true;
                    textField.alignment = TextAnchor.LowerCenter;
                }

                ++currentIndex;
            }
            yield return new WaitForSeconds(dramaticPauseTime);
            ++currentText;
            currentIndex = 0;
        }
    }
}
