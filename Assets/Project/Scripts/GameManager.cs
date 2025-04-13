using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public float timeRemaining = 60f;
    public UIController uiController;
    public GameObject gameOverPanel;

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
                timeRemaining += 10f;
        }
        else if (type == BallType.Red)
        {
            score = Mathf.Max(0, score - 1);
            timeRemaining = Mathf.Max(0, timeRemaining - 2f);
        }

        uiController.UpdateScore(score);
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); // show UI
        Time.timeScale = 0f;           // pause game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
