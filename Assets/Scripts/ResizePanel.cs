using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IBeginDragHandler, IDragHandler 
{
	private RectTransform rectTransform;
	private Vector2 firstClickLocation;
	private Vector2 originalSizeDelta;

	void Start()
	{
		rectTransform = transform as RectTransform;
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData data)
	{
		originalSizeDelta = rectTransform.sizeDelta;
		firstClickLocation =  data.position;
	}
	#endregion

	#region IBeginDragHandler implementation
	public void OnDrag (PointerEventData data) 
	{
		var mouseDelta = 2 * (data.position - firstClickLocation);
		mouseDelta.y = -mouseDelta.y;

		rectTransform.sizeDelta = originalSizeDelta + mouseDelta;
	}
	#endregion
}