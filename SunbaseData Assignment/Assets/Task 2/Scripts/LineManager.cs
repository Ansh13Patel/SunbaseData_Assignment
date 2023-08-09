using UnityEngine;

public class LineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool isDrawing = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
        }
        else if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;
            CheckCollisionAndDestroyCircles();
            lineRenderer.positionCount = 0;
        }
    }

    private void CheckCollisionAndDestroyCircles()
    {
        Vector2 previousPoint = lineRenderer.GetPosition(0);
        for (int count = 1; count < lineRenderer.positionCount; count++)
        {
            Vector2 currentPoint = lineRenderer.GetPosition(count);
            RaycastHit2D[] hits = Physics2D.LinecastAll(previousPoint, currentPoint);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Circle"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            previousPoint = currentPoint;
        }
    }
}
