using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragPanel : MonoBehaviour, IBeginDragHandler, IDragHandler 
{
	private RectTransform rectTransform;
	private Vector2 pointerOffset;

	void Start()
	{
		Window window = GetComponentInParent<Window> ();
		rectTransform = window.transform as RectTransform;
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData data)
	{
		pointerOffset =  data.position - rectTransform.anchoredPosition;
	}
	#endregion

	#region IBeginDragHandler implementation
	public void OnDrag (PointerEventData data) 
	{
		rectTransform.anchoredPosition = data.position - pointerOffset;
	}
	#endregion
}