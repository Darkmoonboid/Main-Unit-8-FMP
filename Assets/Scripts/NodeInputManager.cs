
using UnityEngine;

public class NodeInputManager : MonoBehaviour
{
    public LineRenderer linePrefab;

    private UINode startNode;
    private LineRenderer activeLine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TrySelectNode();

        if (startNode != null && activeLine != null)
            UpdateLine();

        if (Input.GetMouseButtonUp(0))
            TryReleaseNode();
    }

    void TrySelectNode()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            GetMouseWorldPosition(),
            Vector2.zero
        );

        if (hit.collider != null &&
            hit.collider.TryGetComponent(out UINode node))
        {
            startNode = node;
            CreateLine();
        }
    }

    void CreateLine()
    {
        activeLine = Instantiate(linePrefab);
        activeLine.positionCount = 2;

        Vector3 startPos = startNode.transform.position;
        activeLine.SetPosition(0, startPos);
        activeLine.SetPosition(1, startPos);
    }

    void UpdateLine()
    {
        activeLine.SetPosition(0, startNode.transform.position);
        activeLine.SetPosition(1, GetMouseWorldPosition());
    }

    void TryReleaseNode()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            GetMouseWorldPosition(),
            Vector2.zero
        );

        if (hit.collider != null &&
            hit.collider.TryGetComponent(out UINode endNode))
        {
            if (IsValidConnection(startNode, endNode))
            {
                CompleteConnection(endNode);
                return;
            }
        }

        Destroy(activeLine.gameObject);
        startNode = null;
    }

    bool IsValidConnection(UINode a, UINode b)
    {
        float maxDistance = 1.5f; // world units
        return Vector2.Distance(
            a.transform.position,
            b.transform.position
        ) <= maxDistance && !b.isConnected;
    }

    void CompleteConnection(UINode endNode)
    {
        activeLine.SetPosition(1, endNode.transform.position);

        startNode.isConnected = true;
        endNode.isConnected = true;

        startNode = endNode;
        activeLine = null;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }
}
