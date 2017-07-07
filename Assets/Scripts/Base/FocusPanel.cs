using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FocusPanel : MonoBehaviour, IPointerDownHandler {

	private RectTransform panel;

	void Awake () 
	{
		Window window = GetComponentInParent<Window> ();
		panel = window.transform as RectTransform;
	}

	public void OnPointerDown (PointerEventData data) 
	{
		panel.SetAsLastSibling ();
	}

}