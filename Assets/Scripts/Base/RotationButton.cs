using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationButton : MonoBehaviour {
    [SerializeField] private GameObject[] options;

    [SerializeField] bool xAxis = true;
    [SerializeField] bool invert = true;

    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] string[] testText;

    private bool mouseButtonDown = false;
    private Vector2 oldPosition;

    void Start() {
        SetOptions(testText);
    }

    public void SetOptions(string[] optionTexts) { 
        for (int i = 0; i < 4; ++i) {
            if (i < options.Length) {
                if (i < optionTexts.Length) {
                    options[i].GetComponent<Text>().text = optionTexts[i];
                } else {
                    options[i].SetActive(false);
                }
            }
        }
    }

    public void OnMouseButtonDown() {
        mouseButtonDown = true;
        oldPosition = Input.mousePosition;
    }

    void Update() {
        if (!mouseButtonDown) {
            return;
        }    

        if (!Input.GetMouseButton(0)) {
            mouseButtonDown = false;
            return;
        }

        Vector2 newPos = Input.mousePosition;

        float delta = 0.0f;
        if (xAxis) {
            delta = newPos.x - oldPosition.x;
        } else {
            delta = newPos.y - oldPosition.y;
        }

        if (invert) {
            delta *= -1;
        }

        float change = delta * rotationSpeed;
        transform.Rotate(Vector3.forward, change);
        oldPosition = newPos;

        Debug.Log(GetSelected());

     
    }

    public string GetSelected() {
        float rotationAngle = transform.localEulerAngles.z;
        while (rotationAngle < 0.0f) {
            rotationAngle += 360.0f;
        }

        int selection = (Mathf.FloorToInt(rotationAngle / (360.0f / options.Length)) + 1) % options.Length;
        if ((selection < options.Length) && (options[selection].activeSelf)) {
            return options[selection].GetComponent<Text>().text;
        } else {
            return string.Empty;
        }

    }
}
