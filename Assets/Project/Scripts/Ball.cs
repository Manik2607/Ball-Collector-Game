using UnityEngine;

public enum BallType
{
    Purple,
    Red
}


public class Ball : MonoBehaviour
{
    public BallType ballType;           // Assign in prefab: Purple or Red
    public float fallSpeed = 3f;        // Set per prefab for variation

    void Update()
    {
        // Move downward every frame
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
