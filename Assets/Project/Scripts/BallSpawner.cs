using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject purpleBallPrefab;
    public GameObject redBallPrefab;

    public float redBallProbability = 0.3f; // Probability of spawning a red ball
    public float spawnInterval = 1f; // Time interval between spawns

    private BoxCollider2D spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        InvokeRepeating(nameof(SpawnBall), 1f, spawnInterval); // Start spawning balls at regular intervals
    }

    void SpawnBall()
    {
        Vector2 spawnPosition = GetRandomPositionInArea(); 
        GameObject prefab = Random.value > redBallProbability ? purpleBallPrefab : redBallPrefab; // Choose ball based on probability
        Instantiate(prefab, spawnPosition, Quaternion.identity); // Instantiate the chosen ball prefab
    }

    Vector2 GetRandomPositionInArea()
    {
        Vector2 center = spawnArea.bounds.center;
        Vector2 size = spawnArea.bounds.size;

        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f); // Random x position within bounds
        float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f); // Random y position within bounds

        return new Vector2(x, y);
    }

    // for debugging purposes only
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
