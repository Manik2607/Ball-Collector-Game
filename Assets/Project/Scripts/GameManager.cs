using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public float timeRemaining = 60f;
    public UIController uiController;
    public GameObject gameOverPanel;

    public float PurpleBallTimeBonus = 10f; // Time bonus for every 10 purple balls
    public float RedBallTimePenalty = 2f;   // Time penalty for red ball
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;
        uiController.UpdateTimer(timeRemaining);

        if (timeRemaining <= 0)
        {
            GameOver();
        }
    }

    public void OnBallCollected(BallType type)
    {
        if (isGameOver) return;

        if (type == BallType.Purple)
        {
            score++;
            if (score % 10 == 0)
                timeRemaining += PurpleBallTimeBonus;
            AudioManager.Instance.Play("purple");
        }
        else if (type == BallType.Red)
        {
            score = Mathf.Max(0, score - 1); // Decrease score by 1 on red ball collection
            timeRemaining = Mathf.Max(0, timeRemaining - RedBallTimePenalty);
            AudioManager.Instance.Play("red");
        }

        uiController.UpdateScore(score);
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); // Show game over UI
        Time.timeScale = 0f;           // Pause the game
    }
    // restart the game by reloading the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
