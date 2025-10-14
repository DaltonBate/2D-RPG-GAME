using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] private GameObject gameOverUI;
    [Space]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;

    private float levelStartTime;
    private int killCount;
    private bool isGameOver;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        levelStartTime = Time.time; // Record level start time
    }

    private void Update()
    {
        if (!isGameOver) 
        {
         float elapsedTime = Time.time - levelStartTime;
         timerText.text = elapsedTime.ToString("F2") + "s";
        }
    }

    public void EnableGameOverUI() 
    {
        Time.timeScale = 0.5f;
        gameOverUI.SetActive(true);     
        isGameOver = true;
    }  

    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);  
    }

    public void AddKillCount() 
    {
        killCount++;
        killCountText.text = killCount.ToString();
    }
}
