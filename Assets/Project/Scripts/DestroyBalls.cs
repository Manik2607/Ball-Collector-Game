using UnityEngine;

public class DestroyBalls : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball")) // Assuming all balls have the "Ball" tag
        {
            Destroy(other.gameObject);
        }
    }


}
