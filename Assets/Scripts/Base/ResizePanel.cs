using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IBeginDragHandler, IDragHandler 
{
	private RectTransform rectTransform;
	private Vector2 firstClickLocation;
	private Vector2 originalSizeDelta;

	private bool swapX = false;
	private bool swapY = false;

	void Start()
	{
		Window window = GetComponentInParent<Window> ();
		rectTransform = window.transform as RectTransform;
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData data)
	{
		originalSizeDelta = rectTransform.sizeDelta;
		firstClickLocation =  data.position;

		var windowGlobalPosition = new Vector2 (rectTransform.position.x, rectTransform.position.y);
		var pivotToMouse = data.position - windowGlobalPosition;

		swapY = (pivotToMouse.y < 0);
		swapX = (pivotToMouse.x < 0);
	}
	#endregion

	#region IBeginDragHandler implementation
	public void OnDrag (PointerEventData data) 
	{
		var mouseDelta = 2 * (data.position - firstClickLocation);

		if (swapY)
			mouseDelta.y = -mouseDelta.y;

		if (swapX)
			mouseDelta.x = -mouseDelta.x;

		rectTransform.sizeDelta = originalSizeDelta + mouseDelta;
	}
	#endregion
}