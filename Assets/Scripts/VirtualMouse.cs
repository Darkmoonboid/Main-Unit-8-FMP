using UnityEngine;
using System.Collections;

public class VirtualMouse : MonoBehaviour

{

    private Vector3 mousePosition;
    public float moveSpeed = 100f;

    void Update()

    {

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
}
