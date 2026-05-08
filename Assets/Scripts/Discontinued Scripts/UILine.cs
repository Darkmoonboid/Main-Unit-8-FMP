using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class UILine : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        // IMPORTANT: pivot must be left-centered
        rect.pivot = new Vector2(0f, 0.5f);
    }

    public void SetLine(Vector2 startAnchoredPos, Vector2 endAnchoredPos)
    {
        Vector2 direction = endAnchoredPos - startAnchoredPos;
        float length = direction.magnitude;

        rect.anchoredPosition = startAnchoredPos;
        rect.sizeDelta = new Vector2(length, rect.sizeDelta.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}