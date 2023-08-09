using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab of the circle
    public LayerMask circleLayer; // Layer mask for circle detection

    public float minX = -1.5f;
    public float maxX = 1.5f;
    public float minY = -5f;
    public float maxY = 5f;

    private List<GameObject> circles = new List<GameObject>();
    private float circleRadius;

    private void Start()
    {
        circleRadius = circlePrefab.GetComponent<CircleCollider2D>().radius;
        SpawnRandomCircles(Random.Range(5, 10));
    }

    public void SpawnRandomCircles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = GetRandomPositionWithoutOverlap();
            GameObject circle = Instantiate(circlePrefab, randomPos, Quaternion.identity);
            circles.Add(circle);
        }
    }

    private Vector3 GetRandomPositionWithoutOverlap()
    {
        float maxAttempts = 10;
        int attempt = 0;
        Vector3 randomPos;

        do
        {
            randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            attempt++;
        } while (CheckOverlap(randomPos) && attempt < maxAttempts);

        return randomPos;
    }

    private bool CheckOverlap(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, circleRadius, circleLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider != null)
            {
                return true; // Overlap detected
            }
        }

        return false; // No overlap detected
    }

}
