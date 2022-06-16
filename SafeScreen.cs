using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeScreen : MonoBehaviour
{
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Rect area = Screen.safeArea;

        Vector2 anchorMin = area.position;
        Vector2 anchorMax = area.position + area.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;

        Destroy(this);
    }
}
