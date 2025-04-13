using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject purpleBallPrefab;
    public GameObject redBallPrefab;

    public float RedBallProbability = 0.3f;
    public float spawnInterval = 1f;

    private BoxCollider2D spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        InvokeRepeating(nameof(SpawnBall), 1f, spawnInterval);
    }

    void SpawnBall()
    {
        Vector2 spawnPosition = GetRandomPositionInArea();
        GameObject prefab = Random.value > RedBallProbability ? purpleBallPrefab : redBallPrefab; // Choose ball based on probability
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomPositionInArea()
    {
        Vector2 center = spawnArea.bounds.center;
        Vector2 size = spawnArea.bounds.size;

        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(x, y);
    }

    void OnDrawGizmos()
    {
        var box = GetComponent<BoxCollider2D>();
        if (box)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
        }
    }
}
