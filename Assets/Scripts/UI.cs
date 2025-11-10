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

    [Header("Win Settings")]
    [SerializeField] private float winTimeSeconds = 30f;

    private float levelStartTime;
    private int killCount;
    private bool isGameOver;
    private bool isGameWon;

    private void Awake()
    {
        // simple singleton safety
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Use realtime so changing Time.timeScale doesn't affect the recorded start time
        levelStartTime = Time.realtimeSinceStartup;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Only update timer while the game is running (not game over or won)
        if (!isGameOver && !isGameWon)
        {
            float elapsedTime = Time.realtimeSinceStartup - levelStartTime;

            if (timerText != null)
                timerText.text = elapsedTime.ToString("F2") + "s";

            // Win condition: trigger when elapsed meets or exceeds winTimeSeconds
            if (elapsedTime >= winTimeSeconds)
            {
                // show exact win time on the timer to avoid a slight overshoot display
                if (timerText != null)
                    timerText.text = winTimeSeconds.ToString("F2") + "s";

                EnableGameWonUI();
            }
        }
    }

    public void EnableGameWonUI()
    {
        Debug.Log("EnableGameWonUI called on UI.Instance");
        if (gameWonUI == null)
        {
            Debug.LogWarning("gameWonUI is not assigned on UI (check Inspector)");
            return;
        }

        // usually you want to pause the game on win (use 0). 15 would speed the game up.
        Time.timeScale = 1f;
        gameWonUI.SetActive(true);
        isGameWon = true;
    }

    public void EnableGameOverUI()
    {
        Debug.Log("EnableGameOverUI called on UI.Instance");
        if (gameOverUI == null)
        {
            Debug.LogWarning("gameOverUI is not assigned on UI (check Inspector)");
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
