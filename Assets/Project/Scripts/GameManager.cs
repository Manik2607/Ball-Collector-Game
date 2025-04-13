using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public float timeRemaining = 60f;
    public UIController uiController;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        uiController.UpdateTimer(timeRemaining);

        if (timeRemaining <= 0)
            EndGame();
    }

    public void OnBallCollected(BallType type)
    {
        if (type == BallType.Purple)
        {
            score++;
            if (score % 10 == 0)
                timeRemaining += 10f;
        }
        else if (type == BallType.Red)
        {
            score = Mathf.Max(0, score - 1);
            timeRemaining = Mathf.Max(0, timeRemaining - 5f);
        }

        uiController.UpdateScore(score);
    }

    void EndGame()
    {
        // Show game over screen, stop spawn, etc.
        Debug.Log("Game Over!");
    }
}
