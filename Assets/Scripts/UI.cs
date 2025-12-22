using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWonUI;
    [Space]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;

    private float levelStartTime;
    private int killCount;
    private bool isGameOver;
    private bool isGameWon;

    private void Awake()
    {
        // Singleton safety
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        levelStartTime = Time.realtimeSinceStartup;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!isGameOver && !isGameWon)
        {
            float elapsedTime = Time.realtimeSinceStartup - levelStartTime;

            if (timerText != null)
                timerText.text = elapsedTime.ToString("F2") + "s";
        }
    }

    public void HandleWinCondition()
    {
        if (isGameWon) return; // prevents double triggering

        isGameWon = true;

        if (gameWonUI != null)
            gameWonUI.SetActive(true);

        Time.timeScale = 1f;

        Debug.Log(" HandleWinCondition() triggered, notifying GameManager...");
        GameManager.Instance?.WinGame();
    }

    public void EnableGameOverUI()
    {
        if (gameOverUI == null)
        {
            Debug.LogWarning("gameOverUI not assigned!");
            return;
        }

        Time.timeScale = 0.5f;
        gameOverUI.SetActive(true);
        isGameOver = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount()
    {
        killCount++;
        if (killCountText != null)
            killCountText.text = killCount.ToString();
    }
}

