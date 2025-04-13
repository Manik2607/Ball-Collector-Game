using UnityEngine;

public class CollectorController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Camera mainCamera;
    private float halfWidth;
    void Start()
    {
        mainCamera = Camera.main;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        halfWidth = collider.bounds.extents.x / 2.0f; // Calculate half the width of the collector
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal"); // Get horizontal input
        transform.Translate(Vector3.right * move * moveSpeed * Time.deltaTime); // Move collector indepentent of the frame rate

        // Clamp the position to stay within the camera bounds
        Vector3 position = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);

        // Adjust clamping to account for the collector's width
        float screenHalfWidth = halfWidth / mainCamera.orthographicSize / mainCamera.aspect;
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, screenHalfWidth, 1f - screenHalfWidth);

        position = mainCamera.ViewportToWorldPoint(viewportPosition);
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball")) // Check if the object has the "Ball" tag
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null)
            {
                GameManager.Instance.OnBallCollected(ball.ballType); // Notify GameManager of ball collection
                Destroy(other.gameObject); // Destroy the ball object
            }
        }
    }
}