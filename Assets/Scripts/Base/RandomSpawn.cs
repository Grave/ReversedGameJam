using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {
    [SerializeField] bool inside;

	// Use this for initialization
	void Start () {
        Transform p = transform.parent;
        if ((p is RectTransform) && (transform is RectTransform)) {
            RectTransform r = p as RectTransform;
            RectTransform o = transform as RectTransform;
            float height = r.rect.height;
            float width = r.rect.width;

            float pivotOffsetX = o.rect.width * o.pivot.x;
            float pivotOffsetY = o.rect.height * o.pivot.y;

            Vector3 pos = Vector3.zero;
            float minX = pivotOffsetX * r.localScale.x;
            float minY = pivotOffsetY * r.localScale.y;
            float maxX = (width - pivotOffsetX) * r.localScale.x;
            float maxY = (height - pivotOffsetY) * r.localScale.y;
            pos.x = Random.Range(minX, maxX);
            pos.y = Random.Range(minY, maxY);

            transform.position = pos;
        }
	}

}
