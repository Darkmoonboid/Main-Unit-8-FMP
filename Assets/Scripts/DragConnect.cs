using UnityEngine;

public class DragConnect : MonoBehaviour
{
    public LineRenderer linePrefab;
    public float snapDistance = 15f;

    private Node startNode;
    private LineRenderer currentLine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartConnection();

        if (currentLine != null)
            UpdateLinePosition();

        if (Input.GetMouseButtonUp(0))
            TryFinishConnection();
    }
    
    void TryStartConnection()
    {
        Node node = GetNodeUnderMouse();
        if (node == null) return;

        startNode = node;
        currentLine = Instantiate(linePrefab);
        currentLine.SetPosition(0, startNode.transform.position);
        currentLine.SetPosition(1, startNode.transform.position);
    }
    
    void UpdateLinePosition()
    {
        currentLine.SetPosition(1, GetMouseWorldPosition());
    }
    
    void TryFinishConnection()
    {
        if (startNode == null) return;

        Node endNode = GetNodeUnderMouse();

        if (endNode != null &&
            endNode != startNode &&
            Vector2.Distance(startNode.transform.position, endNode.transform.position) <= snapDistance)
        {
            // Snap to node
            currentLine.SetPosition(1, endNode.transform.position);
            startNode.connected = true;
            endNode.connected = true;
        }
        else
        {
            Destroy(currentLine.gameObject);
        }

        startNode = null;
        currentLine = null;
    }
    
    Node GetNodeUnderMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            GetMouseWorldPosition(),
            Vector2.zero
        );

        if (hit.collider && hit.collider.TryGetComponent(out Node node))
            return node;

        return null;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }
}