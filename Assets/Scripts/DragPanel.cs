using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragPanel : MonoBehaviour, IBeginDragHandler, IDragHandler {

	private Vector2 pointerOffset;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData data)
	{
		var position = new Vector2 (transform.position.x, transform.position.y);
		pointerOffset =  data.position - position;
	}
	#endregion

	public void OnDrag (PointerEventData data) {
		transform.position = data.position - pointerOffset;// ClampToWindow(data);
	}
}